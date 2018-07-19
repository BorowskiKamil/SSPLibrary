using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SSPLibrary.Filters
{
    public class PaginableAttribute : ResultFilterAttribute
    {
        public PaginableAttribute()
        {
        }

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            var result = context.Result;

            if (result is OkObjectResult)
            {
                var objectResult = (OkObjectResult)result;

                // typeof(objectResult.Value);

                // var newResult = new PagedCollection<>
                // {

                // };
            }

            base.OnResultExecuting(context);
        }
    }
}