using System;
using SSPLibrary.Models;
using SSPLibrary.Tests.Models;
using Xunit;
using System.Linq;

namespace SSPLibrary.Tests
{

    public class ExpressionBuilderTests : IClassFixture<ExpressionBuilderFixture>
    {

        private readonly ExpressionBuilderFixture _fixture;

        public ExpressionBuilderTests(ExpressionBuilderFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void ApplyPaging()
        {
            var paged = _fixture.Repository.GetTasks().ApplyPaging(_fixture.QueryParameters);

            Assert.IsType<PagedResults<TodoTask>>(paged);
            Assert.True(paged.Items.Count() <= _fixture.QueryParameters.PagingParameters.Limit);
        }

        [Fact]
        public void ApplySorting()
        {
            var sortQuery = "-Id,IsDone";

            _fixture.QueryParameters.ApplyQueryParameters(sortQuery, null);

            Assert.Equal(2, _fixture.QueryParameters.SortOptions.SortTerms.Count());

            var sorted = _fixture.Repository.GetTasks().ApplySorting(_fixture.QueryParameters).ToArray();

            Assert.True(sorted[0].Id > sorted[1].Id);
        }

        [Fact]
        public void ApplySearching()
        {
            var searchQuery = "Name==Task 10|Task 21";

            _fixture.QueryParameters.ApplyQueryParameters(null, searchQuery);

            Assert.Single(_fixture.QueryParameters.SearchOptions.SearchTerms);
            Assert.Equal(2, _fixture.QueryParameters.SearchOptions.SearchTerms.FirstOrDefault()?.Value.Count());

            var searched = _fixture.Repository.GetTasks().ApplySearching(_fixture.QueryParameters).ToArray();

            Assert.Equal(2, searched.Count());
        }
    }
}
