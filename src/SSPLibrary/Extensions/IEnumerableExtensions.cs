using System.Collections.Generic;
using System.Linq;
using SSPLibrary.Models;

namespace SSPLibrary
{
	public static class IEnumerableExtensions
	{
		// public static PagedCollection<T> ToPagedCollection<T>(
		// 		this IEnumerable<T> source, 
		// 		QueryParameters<T> queryParams)
		// 		where T : PagedResults<T>, new()
		// 	{
		// 		var collectionLink = Link.ToCollection(queryParams.ActionName);

		// 		var collection = PagedCollection<T>.Create<PagedCollection<T>>(collectionLink,
		// 			source.ToArray(),
		// 			queryParams.CollectionSize,
		// 			queryParams.PagingParameters
		// 		);

		// 		return collection;
		// 	}		
	}
}