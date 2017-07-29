using DotNetRanges.Experimental;
using System;
using Xunit;

namespace DotNetRanges.Tests.Experimental
{
    public class RangeConstructorTests
    {
        [Theory]
        [InlineData(1)]
        [InlineData("a")]
        [InlineData('a')]
        [InlineData(5.9f)]
        public void ThrowOnInvalidOpenRange<T>(T endpoint)
            where T : IComparable<T>
        {
            Assert.Throws<InvalidOperationException>(() => Range<T>.Open(endpoint, endpoint));
        }

        [Theory]
        [InlineData(1)]
        [InlineData("a")]
        [InlineData('a')]
        [InlineData(5.9f)]
        public void AllowConstructionOfSingletonRange<T>(T endpoint)
            where T : IComparable<T>
        {
            var range = Range<T>.Closed(endpoint, endpoint);
        }

        [Theory]
        [InlineData(1)]
        [InlineData("a")]
        [InlineData('a')]
        [InlineData(5.9f)]
        public void AllowConstructionOfEmptyClosedOpenRange<T>(T endpoint)
            where T : IComparable<T>
        {
            var range = Range<T>.ClosedOpen(endpoint, endpoint);
        }

        [Theory]
        [InlineData(1)]
        [InlineData("a")]
        [InlineData('a')]
        [InlineData(5.9f)]
        public void AllowConstructionOfEmptyOpenClosedRange<T>(T endpoint)
            where T : IComparable<T>
        {
            var range = Range<T>.OpenClosed(endpoint, endpoint);
        }
    }
}
