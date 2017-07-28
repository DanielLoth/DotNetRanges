using DotNetRanges.Experimental;
using Xunit;

namespace DotNetRanges.Tests.Experimental
{
    public class RangeBoundTypeTests
    {
        [Fact]
        public void ReturnUnboundedForLowerInfiniteBound()
        {
            var range = Range<int>.All();

            Assert.Equal(BoundType.UNBOUNDED, range.LowerBoundType);
        }

        [Fact]
        public void ReturnUnboundedForUpperInfiniteBound()
        {
            var range = Range<int>.All();

            Assert.Equal(BoundType.UNBOUNDED, range.UpperBoundType);
        }

        [Fact]
        public void ReturnClosedForLowerClosedBound()
        {
            var range = Range<int>.Closed(10, 100);

            Assert.Equal(BoundType.CLOSED, range.LowerBoundType);
        }

        [Fact]
        public void ReturnClosedForUpperClosedBound()
        {
            var range = Range<int>.Closed(10, 100);

            Assert.Equal(BoundType.CLOSED, range.UpperBoundType);
        }

        [Fact]
        public void ReturnOpenForLowerOpenBound()
        {
            var range = Range<int>.Open(10, 100);

            Assert.Equal(BoundType.OPEN, range.LowerBoundType);
        }

        [Fact]
        public void ReturnOpenForUpperOpenBound()
        {
            var range = Range<int>.Open(10, 100);

            Assert.Equal(BoundType.OPEN, range.UpperBoundType);
        }
    }
}
