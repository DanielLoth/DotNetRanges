using DotNetRanges.Experimental;
using Xunit;

namespace DotNetRanges.Tests.Experimental
{
    public class ClosedRangeTests
    {
        [Fact]
        public void Blah()
        {
            var a = Range<int>.Closed(1, 5);
            var b = Range<int>.Closed(1, 5);

            Assert.True(a.Equals(b));
            Assert.True(b.Equals(a));
        }
    }
}
