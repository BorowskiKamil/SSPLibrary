using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SSPLibrary.Models;
using System.Linq;
using System.Globalization;
using System.Text.RegularExpressions;

namespace SSPLibrary.Infrastructure
{
	public class QueryParametersModelBinder : IModelBinder
	{
		
		public Task BindModelAsync(ModelBindingContext bindingContext)
		{
			if (bindingContext == null)
			{
				throw new ArgumentNullException(nameof(bindingContext));
			}

			var elementTypes = bindingContext.ModelType.GetGenericArguments();

            var genericType = typeof(QueryParameters<>).MakeGenericType(elementTypes[0]);
			var paramsInstance = (IQueryParameters)Activator.CreateInstance(genericType);

			var queryParams = HttpUtility.ParseQueryString(bindingContext.HttpContext.Request.QueryString.Value);

			var action = bindingContext.ActionContext.ActionDescriptor as ControllerActionDescriptor;

			paramsInstance.ActionName = action.ActionName;

			var validationContext = new ValidationContext(paramsInstance);

			if (int.TryParse(queryParams[nameof(paramsInstance.PagingParameters.Limit).ToCamelCase()], out int limitResult))
			{
				paramsInstance.PagingParameters.Limit = Math.Min(limitResult, SSPOptions.Instance.PagingOptions.MaxLimit);
			}
			else 
			{
				paramsInstance.PagingParameters.Limit = SSPOptions.Instance.PagingOptions.DefaultLimit;
			}

			if (int.TryParse(queryParams[nameof(paramsInstance.PagingParameters.Offset).ToCamelCase()], out int offsetResult))
			{
				paramsInstance.PagingParameters.Offset = offsetResult;
			}
			else
			{
				paramsInstance.PagingParameters.Offset = 0;
			}

			var orderByKey = queryParams.AllKeys.Where(x => Regex.IsMatch(x, "OrderBy", RegexOptions.IgnoreCase)).FirstOrDefault();
			if (orderByKey != null)
			{
				var errors = paramsInstance.ApplyQueryParameters(queryParams[orderByKey], validationContext);
				
				if (errors?.Count() > 0)
				{
					foreach (var error in errors)
					{
						bindingContext.ModelState.AddModelError(error.ErrorMessage, String.Join(",", error.MemberNames));
					}

					bindingContext.Result = ModelBindingResult.Failed();
					return Task.CompletedTask;
				}
			}

			bindingContext.Result = ModelBindingResult.Success(paramsInstance);

			return Task.CompletedTask;
		}
	}
}
