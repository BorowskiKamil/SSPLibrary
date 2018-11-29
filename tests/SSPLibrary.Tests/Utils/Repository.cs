using System;
using System.Linq;
using System.Collections.Generic;
using SSPLibrary.Tests.Models;

namespace SSPLibrary.Tests
{
	public class Repository
	{

		private readonly IEnumerable<TodoTaskEntity> _tasks;

		public Repository()
		{
			_tasks = GenerateTasks();
		}


		public IQueryable<TodoTaskEntity> GetTasks()
		{
			return _tasks.AsQueryable();
		}

		private IEnumerable<TodoTaskEntity> GenerateTasks()
		{
			Random rnd = new Random();

			for (int i = 1; i < 50; i++)
			{
				yield return new TodoTaskEntity
				{
					Id = i,
					Name = $"Task {i}",
					IsDone = rnd.Next() % 2 == 0,
				};
			}
		}
	}
}