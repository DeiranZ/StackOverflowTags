using Xunit;
using FluentAssertions;

namespace StackOverflowTags.Domain.Models.Tests
{
    public class TagParametersTests
    {
        [Fact()]
        public void PageSize_RequestedAboveMax_Clamps()
        {
            // arrange

            var tagParameters = new TagParameters();

            // act

            tagParameters.PageSize = 70;

            // assert

            tagParameters.PageSize.Should().Be(50);
        }

        [Fact()]
        public void PageSize_RequestedInRange()
        {
            // arrange

            var tagParameters = new TagParameters();

            // act

            tagParameters.PageSize = 35;

            // assert

            tagParameters.PageSize.Should().Be(35);
        }
    }
}