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
			throw new NotImplementedException();
		}
    }
}
