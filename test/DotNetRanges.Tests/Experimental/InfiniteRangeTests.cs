using DotNetRanges.Experimental;
using System;
using Xunit;

namespace DotNetRanges.Tests.Experimental
{
    public class InfiniteRangeTests
    {
        [Fact]
        public void InfiniteRangesEqual()
        {
            var a = Range<int>.All();
            var b = Range<int>.All();

            Assert.True(a.Equals(b));
            Assert.True(b.Equals(a));
        }

        [Fact]
        public void ThrowsWhenAccessingLowerBound()
        {
            var infinite = Range<int>.All();

            Assert.Throws<InvalidOperationException>(() => infinite.LowerEndpoint);
        }

        [Fact]
        public void ThrowsWhenAccessingUpperBound()
        {
            var infinite = Range<int>.All();

            Assert.Throws<InvalidOperationException>(() => infinite.UpperEndpoint);
        }

        [Fact]
        public void HasLowerBoundReturnsFalse()
        {
            var infinite = Range<int>.All();

            Assert.False(infinite.HasLowerEndpoint);
        }

        [Fact]
        public void HasUpperBoundReturnsFalse()
        {
            var infinite = Range<int>.All();

            Assert.False(infinite.HasUpperEndpoint);
        }
    }
}
