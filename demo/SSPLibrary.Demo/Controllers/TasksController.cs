using Microsoft.AspNetCore.Mvc;
using SSPLibrary.Demo.Models;
using SSPLibrary.Demo.Repositories;
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

		[HttpGet]
        public IActionResult GetAll(QueryParameters<TodoTask> queryParameters)
        {
            var result = _repository.GetTasks(queryParameters);

			return Ok(result);
        }
	}
}