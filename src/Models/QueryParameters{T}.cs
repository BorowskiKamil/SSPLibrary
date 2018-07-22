using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SSPLibrary.Models
{
	public class QueryParameters<T> : IQueryParameters
	{

		public SortOptions<T> SortOptions { get; set; } = new SortOptions<T>();

		public PagingParameters PagingParameters { get; set; } = new PagingParameters();

		public SearchOptions<T> SearchOptions { get; set; } = new SearchOptions<T>();

		public string ActionName { get; set; }

		public void ApplyQueryParameters(string sortParams, string searchOptions)
		{
			SortOptions.ParseQuery(sortParams);
			SearchOptions.ParseQuery(searchOptions);
		}
	}
}