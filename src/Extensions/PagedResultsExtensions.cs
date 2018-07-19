using System;
using System.Collections.Generic;
using System.Linq;
using SSPLibrary.Models;

namespace SSPLibrary
{
	public static class PagedResultsExtensions
	{
        public static PagedCollection<T> ToPagedCollection<T>(
				this PagedResults<T> source, 
				QueryParameters<T> queryParams)
			{

				var collectionLink = Link.ToCollection(queryParams.ActionName);

				var collection = PagedCollection<T>.Create<PagedCollection<T>>(collectionLink,
					source.Items.ToArray(),
					source.TotalSize,
					queryParams.PagingParameters
				);

				return collection;
			}
	}
}