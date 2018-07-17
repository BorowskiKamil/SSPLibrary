using System.Collections.Generic;
using SSPLibrary.Demo.Models;
using SSPLibrary.Models;

namespace SSPLibrary.Demo.Repositories
{
	public interface ITasksRepository
	{
		IEnumerable<TodoTask> GetTasks(QueryParameters<TodoTask> queryParameters);
	}
}