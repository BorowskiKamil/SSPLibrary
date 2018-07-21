using System;
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
        public IActionResult GetAllTasks(QueryParameters<TodoTask> queryParameters)
        {
			if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = _repository.GetTasks(queryParameters);
			return Ok(result.ToPagedCollection(queryParameters));
        }
	}
}