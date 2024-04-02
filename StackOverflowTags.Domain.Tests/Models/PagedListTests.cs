using Xunit;
using FluentAssertions;

namespace StackOverflowTags.Domain.Models.Tests
{
    public class PagedListTests
    {
        [Fact()]
        public void PagedList_PagesCorrectly()
        {
            // arrange

            var originalList = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            var queryable = Enumerable.AsEnumerable(originalList).AsQueryable();

            // act

            var pagedList = PagedList<int>.ToPagedList(queryable, 3, 2);

            // assert

            pagedList.ToArray().Should().BeEquivalentTo(new[] { 5, 6 } );
        }

        [Fact()]
        public void PagedList_PagesCorrectly2()
        {
            // arrange

            var originalList = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            var queryable = Enumerable.AsEnumerable(originalList).AsQueryable();

            // act

            var pagedList = PagedList<int>.ToPagedList(queryable, 2, 8);

            // assert

            pagedList.ToArray().Should().BeEquivalentTo(new[] { 9, 10 });
        }

        [Fact()]
        public void PagedList_HasCorrectMetadata_WhenPreviousAndNextExist()
        {
            // arrange

            var originalList = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            var queryable = Enumerable.AsEnumerable(originalList).AsQueryable();

            // act

            var pagedList = PagedList<int>.ToPagedList(queryable, 3, 2);

            // assert

            pagedList.TotalCount.Should().Be(10);
            pagedList.TotalPages.Should().Be(5);
            pagedList.PageSize.Should().Be(2);
            pagedList.CurrentPage.Should().Be(3);
            pagedList.HasNext.Should().BeTrue();
            pagedList.HasPrevious.Should().BeTrue();
        }
        
        [Fact()]
        public void PagedList_HasCorrectMetadata_WhenPreviousDoesntExist()
        {
            // arrange

            var originalList = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8 };
            var queryable = Enumerable.AsEnumerable(originalList).AsQueryable();

            // act

            var pagedList = PagedList<int>.ToPagedList(queryable, 1, 2);

            // assert

            pagedList.TotalCount.Should().Be(8);
            pagedList.TotalPages.Should().Be(4);
            pagedList.PageSize.Should().Be(2);
            pagedList.CurrentPage.Should().Be(1);
            pagedList.HasNext.Should().BeTrue();
            pagedList.HasPrevious.Should().BeFalse();
        }

        [Fact()]
        public void PagedList_HasCorrectMetadata_WhenNextDoesntExist()
        {
            // arrange

            var originalList = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            var queryable = Enumerable.AsEnumerable(originalList).AsQueryable();

            // act

            var pagedList = PagedList<int>.ToPagedList(queryable, 2, 7);

            // assert

            pagedList.TotalCount.Should().Be(10);
            pagedList.TotalPages.Should().Be(2);
            pagedList.PageSize.Should().Be(7);
            pagedList.CurrentPage.Should().Be(2);
            pagedList.HasNext.Should().BeFalse();
            pagedList.HasPrevious.Should().BeTrue();
        }
    }
}