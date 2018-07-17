using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SSPLibrary.Demo.Models;
using SSPLibrary.Filters;
using SSPLibrary.Models;

namespace SSPLibrary.Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> GetAll(QueryParameters<TodoTask> queryParameters)
        {
            Console.WriteLine($"LimitVal: {queryParameters.PagingParameters.Limit.Value}");
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        // [HttpGet("{id}")]
        // public ActionResult<string> Get(int id)
        // {
        //     return "value";
        // }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
