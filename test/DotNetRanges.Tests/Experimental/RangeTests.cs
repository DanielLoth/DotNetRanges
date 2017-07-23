using DotNetRanges.Experimental;
using Xunit;

namespace DotNetRanges.Tests.Experimental
{
    public class RangeTests
    {
        [Fact]
        public void FirstUnitTest()
        {
            var range = new Range<int>();

            Assert.NotNull(range);
            Assert.Equal(1, 1);
        }
    }
}
