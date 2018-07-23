using System.Linq;
using SSPLibrary.Infrastructure;
using SSPLibrary.Models;

namespace SSPLibrary
{
	public static class IQueryableExtensions
	{
		public static PagedResults<T> ApplyPaging<T>(
			this IQueryable<T> source, 
            QueryParameters<T> queryParams)
		{
			var totalSize = source.Count();

			var result = source
				.Skip(queryParams.PagingParameters.Offset.Value)
				.Take(queryParams.PagingParameters.Limit.Value)
				.ToArray();

			return new PagedResults<T>
			{
				TotalSize = totalSize,
				Items = result
			};
		}

		public static IQueryable<T> ApplySorting<T>(
			this IQueryable<T> query,
			QueryParameters<T> queryParams)
		{
			var processor = new SortOptionsProcessor<T>(queryParams.SortOptions.OrderBy);
			return processor.Apply(query);
		}

		public static IQueryable<T> ApplySearching<T>(
			this IQueryable<T> query,
			QueryParameters<T> queryParams
		)
        {
			var processor = new SearchOptionsProcessor<T>(queryParams.SearchOptions.SearchTerms);
			return processor.Apply(query);
        }

	}

}