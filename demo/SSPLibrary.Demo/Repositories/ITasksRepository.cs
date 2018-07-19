using System.Collections.Generic;
using SSPLibrary.Demo.Models;
using SSPLibrary.Models;

namespace SSPLibrary.Demo.Repositories
{
	public interface ITasksRepository
	{
		PagedResults<TodoTask> GetTasks(QueryParameters<TodoTask> queryParameters);
	}
}