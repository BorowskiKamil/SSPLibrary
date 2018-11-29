using System;
using System.Collections.Generic;
using System.Linq;
using SSPLibrary.Models;

namespace SSPLibrary
{
	public static class PagedResultsExtensions
	{
        public static PagedCollection<TEntity> ToPagedCollection<TModel, TEntity>(
				this PagedResults<TEntity> source, 
				QueryParameters<TModel> queryParams)
			{

				var collectionLink = Link.ToCollection(queryParams.ActionName);

				var collection = PagedCollection<TEntity>.Create<PagedCollection<TEntity>>(collectionLink,
					source.Items.ToArray(),
					source.TotalSize,
					queryParams.PagingParameters
				);

				return collection;
			}
	}
}