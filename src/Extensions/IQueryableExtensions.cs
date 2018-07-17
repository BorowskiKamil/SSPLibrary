using System.Linq;
using SSPLibrary.Models;

namespace SSPLibrary
{
	public static class IQueryableExtensions
	{
		public static IQueryable<T> ToPagedResultAsync<T>(
			this IQueryable<T> source, 
            QueryParameters<T> queryParams)
				=> source
					.Skip(queryParams.PagingParameters.Offset.Value)
					.Take(queryParams.PagingParameters.Limit.Value);
		
		
	}
}