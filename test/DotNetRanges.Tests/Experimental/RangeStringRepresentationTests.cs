using DotNetRanges.Experimental;
using Xunit;

namespace DotNetRanges.Tests.Experimental
{
    public class RangeStringRepresentationTests
    {
        [Fact]
        public void ToStringHandlesOpenRange()
        {
            var range = Range<int>.Open(5, 10);

            var expected = "(5..10)";
            var actual = range.ToString();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ToStringHandlesClosedRange()
        {
            var range = Range<int>.Closed(5, 10);

            var expected = "[5..10]";
            var actual = range.ToString();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ToStringHandlesClosedOpenRange()
        {
            var range = Range<int>.ClosedOpen(5, 10);

            var expected = "[5..10)";
            var actual = range.ToString();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ToStringHandlesOpenClosedRange()
        {
            var range = Range<int>.OpenClosed(5, 10);

            var expected = "(5..10]";
            var actual = range.ToString();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ToStringHandlesGreaterThanRange()
        {
            var range = Range<int>.GreaterThan(5);

            var expected = "(5..+∞)";
            var actual = range.ToString();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ToStringHandlesAtLeastRange()
        {
            var range = Range<int>.AtLeast(5);

            var expected = "[5..+∞)";
            var actual = range.ToString();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ToStringHandlesLessThanRange()
        {
            var range = Range<int>.LessThan(10);

            var expected = "(-∞..10)";
            var actual = range.ToString();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ToStringHandlesAtMostRange()
        {
            var range = Range<int>.AtMost(10);

            var expected = "(-∞..10]";
            var actual = range.ToString();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ToStringHandlesAllRange()
        {
            var range = Range<int>.All();

            var expected = "(-∞..+∞)";
            var actual = range.ToString();

            Assert.Equal(expected, actual);
        }
    }
}
