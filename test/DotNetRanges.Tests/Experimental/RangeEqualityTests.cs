using DotNetRanges.Experimental;
using System;
using Xunit;

namespace DotNetRanges.Tests.Experimental
{
    public class RangeTwoParameterEqualsTests
    {
        [Fact]
        public void ReturnFalseOnNullLeft()
        {
            Range<int> left = null;
            var right = Range<int>.All();

            Assert.False(Range<int>.Equals(left, right));
        }

        [Fact]
        public void ReturnFalseOnNullRight()
        {
            Range<int> right = null;
            var left = Range<int>.All();

            Assert.False(Range<int>.Equals(left, right));
        }

        [Fact]
        public void ReturnTrueOnSameReference()
        {
            var range = Range<int>.Closed(1, 2);

            Assert.True(Range<int>.Equals(range, range));
        }

        [Theory]
        [InlineData(5, 10, 15, 20)]
        [InlineData("a", "z", "aa", "zz")]
        public void ReturnFalseOnDifferentObjects<T>(T leftLower, T leftUpper, T rightLower, T rightUpper)
            where T : IComparable<T>
        {
            var left = Range<T>.Closed(leftLower, leftUpper);
            var right = Range<T>.Closed(rightLower, rightUpper);

            Assert.False(Range<T>.Equals(left, right));
        }

        [Theory]
        [InlineData(5, 10, 5, 10)]
        [InlineData(5f, 10f, 5f, 10f)]
        [InlineData("a", "z", "a", "z")]
        public void ReturnTrueOnEqualObjects<T>(T leftLower, T leftUpper, T rightLower, T rightUpper)
            where T : IComparable<T>
        {
            var left = Range<T>.Closed(leftLower, leftUpper);
            var right = Range<T>.Closed(rightLower, rightUpper);

            Assert.True(Range<T>.Equals(left, right));
        }
    }
}
