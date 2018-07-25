using System;
using SSPLibrary.Infrastructure;

namespace SSPLibrary.Tests.Models
{
	public class TodoTask
	{
		[Sortable]
		public int Id { get; set; }

		[Searchable]
		public string Name { get; set; }

		[Searchable]
		[Sortable]
		public bool IsDone { get; set; }

		[Searchable]
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
	}
}
