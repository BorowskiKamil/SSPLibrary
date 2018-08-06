using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SSPLibrary.Demo.Models;
using SSPLibrary.Models;

namespace SSPLibrary.Demo.Repositories
{
	public interface ITasksRepository
	{
		Task<PagedResults<TodoTask>> GetTasks(QueryParameters<TodoTask> queryParameters, CancellationToken ct);
	}
}