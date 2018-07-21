using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SSPLibrary.Models
{
	public class QueryParameters<TEntity> : IQueryParameters
	{

		public SortOptions<TEntity> SortOptions { get; set; } = new SortOptions<TEntity>();

		public PagingParameters PagingParameters { get; set; } = new PagingParameters();

		public string ActionName { get; set; }

		public IEnumerable<ValidationResult> ApplyQueryParameters(string sortParams, ValidationContext validationContext)
		{
			SortOptions.ParseQuery(sortParams, validationContext);
			return null;
		}
	}
}