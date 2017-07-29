using DotNetRanges.Experimental;
using Xunit;
using Xunit.Abstractions;

namespace DotNetRanges.Tests.Experimental
{
    public class IntegerPrecedesTests
    {
        private readonly ITestOutputHelper _output;

        public IntegerPrecedesTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Theory]
        // Open-open range tests
        [InlineData(0, 6, BoundType.OPEN, 9, 21, BoundType.OPEN, true, "precedes")]
        [InlineData(0, 11, BoundType.OPEN, 9, 21, BoundType.OPEN, false, "touches")]
        [InlineData(0, 16, BoundType.OPEN, 9, 21, BoundType.OPEN, false, "overlaps")]
        [InlineData(int.MinValue, 6, BoundType.OPEN, 9, 21, BoundType.OPEN, true, "precedes")]
        [InlineData(int.MinValue, 11, BoundType.OPEN, 9, 21, BoundType.OPEN, false, "touches")]
        [InlineData(int.MinValue, 16, BoundType.OPEN, 9, 21, BoundType.OPEN, false, "overlaps")]
        [InlineData(int.MinValue, 6, BoundType.OPEN, 9, int.MaxValue, BoundType.OPEN, true, "precedes")]
        [InlineData(int.MinValue, 11, BoundType.OPEN, 9, int.MaxValue, BoundType.OPEN, false, "touches")]
        [InlineData(int.MinValue, 16, BoundType.OPEN, 9, int.MaxValue, BoundType.OPEN, false, "overlaps")]
        // Closed-closed range tests
        [InlineData(1, 5, BoundType.CLOSED, 10, 20, BoundType.CLOSED, true, "precedes")]
        [InlineData(1, 5, BoundType.CLOSED, 5, 10, BoundType.CLOSED, false, "touches")]
        [InlineData(1, 6, BoundType.CLOSED, 5, 10, BoundType.CLOSED, false, "overlaps")]
        [InlineData(int.MinValue, 5, BoundType.CLOSED, 10, 20, BoundType.CLOSED, true, "precedes")]
        [InlineData(int.MinValue, 5, BoundType.CLOSED, 5, 10, BoundType.CLOSED, false, "touches")]
        [InlineData(int.MinValue, 6, BoundType.CLOSED, 5, 10, BoundType.CLOSED, false, "overlaps")]
        [InlineData(int.MinValue, 5, BoundType.CLOSED, 10, int.MaxValue, BoundType.CLOSED, true, "precedes")]
        [InlineData(int.MinValue, 5, BoundType.CLOSED, 5, int.MaxValue, BoundType.CLOSED, false, "touches")]
        [InlineData(int.MinValue, 6, BoundType.CLOSED, 5, int.MaxValue, BoundType.CLOSED, false, "overlaps")]
        // Open-closed range tests
        [InlineData(1, 6, BoundType.OPEN, 10, 20, BoundType.CLOSED, true, "precedes")]
        [InlineData(1, 6, BoundType.OPEN, 5, 20, BoundType.CLOSED, false, "touches")]
        [InlineData(1, 6, BoundType.OPEN, 3, 20, BoundType.CLOSED, false, "overlaps")]
        [InlineData(int.MinValue, 6, BoundType.OPEN, 10, 20, BoundType.CLOSED, true, "precedes")]
        [InlineData(int.MinValue, 6, BoundType.OPEN, 5, 20, BoundType.CLOSED, false, "touches")]
        [InlineData(int.MinValue, 6, BoundType.OPEN, 3, 20, BoundType.CLOSED, false, "overlaps")]
        [InlineData(int.MinValue, 6, BoundType.OPEN, 10, int.MaxValue, BoundType.CLOSED, true, "precedes")]
        [InlineData(int.MinValue, 6, BoundType.OPEN, 5, int.MaxValue, BoundType.CLOSED, false, "touches")]
        [InlineData(int.MinValue, 6, BoundType.OPEN, 3, int.MaxValue, BoundType.CLOSED, false, "overlaps")]
        // Closed-open range tests
        [InlineData(0, 20, BoundType.CLOSED, 30, 50, BoundType.OPEN, true, "precedes")]
        [InlineData(0, 20, BoundType.CLOSED, 19, 50, BoundType.OPEN, false, "touches")]
        [InlineData(0, 20, BoundType.CLOSED, 15, 50, BoundType.OPEN, false, "overlaps")]
        [InlineData(int.MinValue, 20, BoundType.CLOSED, 30, 50, BoundType.OPEN, true, "precedes")]
        [InlineData(int.MinValue, 20, BoundType.CLOSED, 19, 50, BoundType.OPEN, false, "touches")]
        [InlineData(int.MinValue, 20, BoundType.CLOSED, 15, 50, BoundType.OPEN, false, "overlaps")]
        [InlineData(int.MinValue, 20, BoundType.CLOSED, 30, int.MaxValue, BoundType.OPEN, true, "precedes")]
        [InlineData(int.MinValue, 20, BoundType.CLOSED, 19, int.MaxValue, BoundType.OPEN, false, "touches")]
        [InlineData(int.MinValue, 20, BoundType.CLOSED, 15, int.MaxValue, BoundType.OPEN, false, "overlaps")]
        public void ReturnsExpectedRangePrecedesResult(
            int rangeLower, int rangeUpper, BoundType rangeType,
            int otherLower, int otherUpper, BoundType otherType,
            bool expectedResult, string description)
        {
            var range = Helper.Create(rangeLower, rangeUpper, rangeType);
            var other = Helper.Create(otherLower, otherUpper, otherType);
            var output = $"{range} {description} {other}";

            _output.WriteLine(output);
            Assert.Equal(expectedResult, range.Precedes(other));
        }

        [Theory]
        // Closed-closed range tests
        [InlineData(BoundType.CLOSED, 20, 30, BoundType.CLOSED, true, "precedes")]
        [InlineData(BoundType.CLOSED, 11, 30, BoundType.CLOSED, true, "precedes")]
        [InlineData(BoundType.CLOSED, 10, 30, BoundType.CLOSED, false, "touches")]
        [InlineData(BoundType.CLOSED, 5, 30, BoundType.CLOSED, false, "overlaps")]
        // Open-open range tests
        [InlineData(BoundType.OPEN, 20, 30, BoundType.OPEN, true, "precedes")]
        [InlineData(BoundType.OPEN, 11, 30, BoundType.OPEN, true, "precedes")]
        [InlineData(BoundType.OPEN, 8, 30, BoundType.OPEN, false, "touches")]
        [InlineData(BoundType.OPEN, 5, 30, BoundType.OPEN, false, "overlaps")]
        // Closed-open range tests
        [InlineData(BoundType.CLOSED, 20, 30, BoundType.OPEN, true, "precedes")]
        [InlineData(BoundType.CLOSED, 11, 30, BoundType.OPEN, true, "precedes")]
        [InlineData(BoundType.CLOSED, 9, 30, BoundType.OPEN, false, "touches")]
        [InlineData(BoundType.CLOSED, 8, 30, BoundType.OPEN, false, "overlaps")]
        // Open-closed range tests
        [InlineData(BoundType.OPEN, 20, 30, BoundType.CLOSED, true, "precedes")]
        [InlineData(BoundType.OPEN, 11, 30, BoundType.CLOSED, true, "precedes")]
        [InlineData(BoundType.OPEN, 9, 30, BoundType.CLOSED, false, "touches")]
        [InlineData(BoundType.OPEN, 8, 30, BoundType.CLOSED, false, "overlaps")]
        public void ReturnsExpectedInfiniteRangePrecedesResult(
            BoundType rangeType,
            int otherLower, int otherUpper, BoundType otherType,
            bool expectedResult, string description)
        {
            var range = rangeType == BoundType.CLOSED ? Range<int>.AtMost(10) : Range<int>.LessThan(10);
            var other = Helper.Create(otherLower, otherUpper, otherType);
            var output = $"{range} {description} {other}";

            _output.WriteLine(output);
            Assert.Equal(expectedResult, range.Precedes(other));
        }
    }
}
