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
        }

        [Fact]
        public void ThrowsWhenAccessingLowerBound()
        {
            var infinite = Range<int>.All();

            Assert.Throws<InvalidOperationException>(() => infinite.LowerBound);
        }

        [Fact]
        public void ThrowsWhenAccessingUpperBound()
        {
            var infinite = Range<int>.All();

            Assert.Throws<InvalidOperationException>(() => infinite.UpperBound);
        }

        [Fact]
        public void HasLowerBoundReturnsFalse()
        {
            var infinite = Range<int>.All();

            Assert.False(infinite.HasLowerBound);
        }

        [Fact]
        public void HasUpperBoundReturnsFalse()
        {
            var infinite = Range<int>.All();

            Assert.False(infinite.HasUpperBound);
        }
    }
}
