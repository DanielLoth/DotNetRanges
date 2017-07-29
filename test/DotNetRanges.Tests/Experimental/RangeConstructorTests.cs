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
    }
}
