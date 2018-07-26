using System;
using System.Linq;
using System.Threading.Tasks;
using SSPLibrary.Infrastructure;
using SSPLibrary.Models;

namespace SSPLibrary
{
	public static class IQueryableExtensions
	{
		public static PagedResults<T> ToPagedResults<T>(
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

		public static async Task<PagedResults<T>> ToPagedResultsAsync<T>(
			this IQueryable<T> source,
			Func<IQueryable<T>, Task<int>> countAsync,
            Func<IQueryable<T>, Task<T[]>> toArrayAsync, 
            QueryParameters<T> queryParams)
		{
			var totalSize = await countAsync(source);

			source = source
				.Skip(queryParams.PagingParameters.Offset.Value)
				.Take(queryParams.PagingParameters.Limit.Value);
			
			var result = await toArrayAsync(source);

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
			var processor = new SortOptionsProcessor<T>(queryParams.SortOptions.SortTerms);
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