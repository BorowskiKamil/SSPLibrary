using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SSPLibrary.Demo.Models;
using SSPLibrary.Demo.Repositories;
using SSPLibrary.Filters;
using SSPLibrary.Models;

namespace SSPLibrary.Demo.Controllers
{
    [Route("api/[controller]")]
    public class TasksController : Controller
    {

		private readonly ITasksRepository _repository;

		public TasksController(ITasksRepository repository)
		{
			_repository = repository;
		}

		[HttpGet(Name = nameof(GetAllTasks))]
        public async Task<IActionResult> GetAllTasks(QueryParameters<TodoTask> queryParameters, CancellationToken ct)
        {
			if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _repository.GetTasks(queryParameters, ct);
			return Ok(result.ToPagedCollection(queryParameters));
        }
	}
}