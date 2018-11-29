using System;
using SSPLibrary.Infrastructure;

namespace SSPLibrary.Tests.Models
{
	public class TodoTaskEntity
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public bool IsDone { get; set; }

		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
	}
}
