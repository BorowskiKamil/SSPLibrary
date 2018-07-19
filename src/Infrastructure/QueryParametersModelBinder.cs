using System;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SSPLibrary.Models;

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
			var paramsInstance = (QueryParameters)Activator.CreateInstance(genericType);

			var queryParams = HttpUtility.ParseQueryString(bindingContext.HttpContext.Request.QueryString.Value);

			// foreach (var queryParamKey in queryParams.Keys)
			// {
			// 	Console.WriteLine($"Key: {queryParamKey.ToString()}, Value: {queryParams[queryParamKey.ToString()]}");
			// }

			var action = bindingContext.ActionContext.ActionDescriptor as ControllerActionDescriptor;

			paramsInstance.ActionName = action.ActionName;

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

			bindingContext.Result = ModelBindingResult.Success(paramsInstance);

			return Task.CompletedTask;
		}
	}
}
