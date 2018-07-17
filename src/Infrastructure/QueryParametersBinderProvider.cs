using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using SSPLibrary.Models;
using System;

namespace SSPLibrary.Infrastructure
{
    public class QueryParametersBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {

            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            Console.WriteLine($"Name: {context.Metadata.ModelType.FullName}");

            if (context.Metadata.ModelType.GetGenericTypeDefinition() == typeof(QueryParameters<>))
            {
				return new BinderTypeModelBinder(typeof(QueryParametersModelBinder));
            }

            return null;
        }
    }
}
