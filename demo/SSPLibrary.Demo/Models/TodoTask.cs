using System;
using SSPLibrary.Infrastructure;

namespace SSPLibrary.Demo.Models
{
	public class TodoTask
	{
		[Sortable(Default = true, WhenDefaultIsDescending = true)]
		public int Id { get; set; }

		[Searchable]
		public string Name { get; set; }

		[Searchable]
		public bool IsDone { get; set; }

		[Searchable]
		public int Value { get; set; }

		[Searchable]
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
	}
}
