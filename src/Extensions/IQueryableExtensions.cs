using System.Linq;
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
		
		
	}
}