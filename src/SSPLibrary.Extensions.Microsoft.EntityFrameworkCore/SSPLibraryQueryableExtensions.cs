using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SSPLibrary.Models;

namespace Microsoft.EntityFrameworkCore
{
    public static class SSPLibraryQueryableExtensions
    {
        public static async Task<PagedResults<T>> ToPagedResultsAsync<T>(
			this IQueryable<T> source,
            QueryParameters<T> queryParams,
            CancellationToken cancellationToken = default(CancellationToken))
		{
			var totalSize = await source.CountAsync(cancellationToken);

			var result = await source
				.Skip(queryParams.PagingParameters.Offset.Value)
				.Take(queryParams.PagingParameters.Limit.Value).ToArrayAsync(cancellationToken);
			
			return new PagedResults<T>
			{
				TotalSize = totalSize,
				Items = result
			};
		}
    }
}
