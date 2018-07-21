using SSPLibrary.Infrastructure;

namespace SSPLibrary.Demo.Models
{
	public class TodoTask
	{
		[Sortable(Default = true, WhenDefaultIsDescending = true)]
		public int Id { get; set; }

		public string Name { get; set; }

		public bool IsDone { get; set; }
	}
}
