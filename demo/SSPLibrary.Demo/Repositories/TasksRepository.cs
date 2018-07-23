using System;
using System.Collections.Generic;
using SSPLibrary.Demo.Models;
using SSPLibrary.Models;
using System.Linq;

namespace SSPLibrary.Demo.Repositories
{
	public class TasksRepository : ITasksRepository
	{

		public PagedResults<TodoTask> GetTasks(QueryParameters<TodoTask> queryParameters)
		{
			var entities = GenerateFakeTasks()
								.AsQueryable()
								.ApplySearching(queryParameters)
								.ApplySorting(queryParameters)
								.ApplyPaging(queryParameters);

			return entities;
		}

		private IEnumerable<TodoTask> GenerateFakeTasks()
		{
			Random rnd = new Random();

			for (int i = 1; i < 50; i++)
			{
				yield return new TodoTask
				{
					Id = i,
					Name = $"Task {i}",
					IsDone = rnd.Next() % 2 == 0,
					Value = Convert.ToInt16(rnd.Next(1, 500))
				};
			}
		}

	}
}