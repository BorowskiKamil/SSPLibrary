using System;
using System.Linq;
using System.Threading.Tasks;
using SSPLibrary.Infrastructure;
using SSPLibrary.Models;

namespace SSPLibrary
{
	public static class IQueryableExtensions
	{
		public static PagedResults<TEntity> ToPagedResults<TModel, TEntity>(
			this IQueryable<TEntity> source, 
            QueryParameters<TModel> queryParams)
		{
			var totalSize = source.Count();

			var result = source
				.Skip(queryParams.PagingParameters.Offset.Value)
				.Take(queryParams.PagingParameters.Limit.Value)
				.ToArray();

			return new PagedResults<TEntity>
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

		public static IQueryable<TEntity> ApplySorting<TModel, TEntity>(
			this IQueryable<TEntity> query,
			QueryParameters<TModel> queryParams
		)
        {
			var processor = new SortOptionsProcessor<TModel>(queryParams.SortOptions.SortTerms);
			return processor.Apply<TEntity>(query);
        }

		public static IQueryable<TEntity> ApplySearching<TModel, TEntity>(
			this IQueryable<TEntity> query,
			QueryParameters<TModel> queryParams
		)
        {
			var processor = new SearchOptionsProcessor<TModel>(queryParams.SearchOptions.SearchTerms);
			return processor.Apply<TEntity>(query);
        }


	}

}