using System;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using SSPLibrary.Infrastructure;

namespace SSPLibrary
{
    public static class IMvcBuilderExtensions
    {

        public static IMvcBuilder AddSSP(this IMvcBuilder mvcBuilder, Action<SSPOptions> optionsBuilder = null)
        {
            optionsBuilder.Invoke(SSPOptions.Instance);

            mvcBuilder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

            mvcBuilder.AddMvcOptions(opt =>
            {
                opt.ModelBinderProviders.Insert(0, new QueryParametersBinderProvider());
            });

			return mvcBuilder;
        }

    }
}
