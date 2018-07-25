using System;
using SSPLibrary.Models;
using SSPLibrary.Tests.Models;

namespace SSPLibrary.Tests
{
	public class ExpressionBuilderFixture : IDisposable
	{

		public readonly QueryParameters<TodoTask> QueryParameters;

		public readonly Repository Repository;

		public ExpressionBuilderFixture()
		{
			QueryParameters = BuildQueryParameters<TodoTask>();
			Repository = new Repository();
		}

		private QueryParameters<T> BuildQueryParameters<T>()
		{
			return new QueryParameters<T>
			{
				PagingParameters = new PagingParameters
				{
					Offset = 5,
					Limit = 20
				},
			};
		}

		public void Dispose()
		{
		}
	}
}