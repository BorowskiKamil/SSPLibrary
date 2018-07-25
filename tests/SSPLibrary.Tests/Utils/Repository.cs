using System;
using System.Linq;
using System.Collections.Generic;
using SSPLibrary.Tests.Models;

namespace SSPLibrary.Tests
{
	public class Repository
	{

		private readonly IEnumerable<TodoTask> _tasks;

		public Repository()
		{
			_tasks = GenerateTasks();
		}


		public IQueryable<TodoTask> GetTasks()
		{
			return _tasks.AsQueryable();
		}

		private IEnumerable<TodoTask> GenerateTasks()
		{
			Random rnd = new Random();

			for (int i = 1; i < 50; i++)
			{
				yield return new TodoTask
				{
					Id = i,
					Name = $"Task {i}",
					IsDone = rnd.Next() % 2 == 0,
				};
			}
		}
	}
}