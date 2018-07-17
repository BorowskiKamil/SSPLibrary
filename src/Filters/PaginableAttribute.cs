using System.Text;
using System.Threading.Tasks;
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
            var someString = "Elllo";

            byte[] bytes = Encoding.ASCII.GetBytes(someString);

            context.HttpContext.Response.Body.WriteAsync(bytes, 0, bytes.Length);

            base.OnResultExecuting(context);
        }
    }
}