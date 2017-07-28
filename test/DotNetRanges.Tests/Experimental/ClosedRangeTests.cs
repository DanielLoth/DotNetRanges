using DotNetRanges.Experimental;
using Xunit;

namespace DotNetRanges.Tests.Experimental
{
    public class ClosedRangeTests
    {
        [Fact]
        public void IdenticalClosedRangesAreEqual()
        {
            var a = Range<int>.Closed(1, 5);
            var b = Range<int>.Closed(1, 5);

            Assert.True(a.Equals(b));
            Assert.True(b.Equals(a));
        }

        [Fact]
        public void DifferentClosedRangesNotEqual()
        {
            var a = Range<int>.Closed(1, 5);
            var b = Range<int>.Closed(0, 5);

            Assert.False(a.Equals(b));
            Assert.False(b.Equals(a));
        }

        //[Fact]
        public void ClosedRangeEqualsEquivalentOpenRange()
        {
            var closed = Range<int>.Closed(10, 20); // 10 to 20
            var open = Range<int>.Open(9, 21); // 9.001 to 20.999

            // TODO: There needs to be an 'Equivalent' method to handle integer intervals.
            //Assert.True(closed.Equals(open));
            //Assert.True(open.Equals(closed));
        }

        [Fact]
        public void IntCompareTo()
        {
            var a = 5;
            var b = 10;

            var ab = a.CompareTo(b); // Returns -1
            var ba = b.CompareTo(a); // Returns 1

            // Equality returns 0
        }
    }
}
