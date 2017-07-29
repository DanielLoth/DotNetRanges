using DotNetRanges.Experimental;
using Xunit;

namespace DotNetRanges.Tests.Experimental
{
    public class IntegerPrecedesTests
    {
        [Theory]
        // Open-open range tests
        [InlineData(0, 6, BoundType.OPEN, 9, 21, BoundType.OPEN, true, "(0..6) before (9..21) returns true")]
        [InlineData(0, 11, BoundType.OPEN, 9, 21, BoundType.OPEN, false, "(0..11) before (9..21) returns false (touching)")]
        [InlineData(0, 16, BoundType.OPEN, 9, 21, BoundType.OPEN, false, "(0..16) before (9..21) returns false (overlapping)")]
        [InlineData(int.MinValue, 6, BoundType.OPEN, 9, 21, BoundType.OPEN, true, "(min..6) before (9..21) returns true")]
        [InlineData(int.MinValue, 11, BoundType.OPEN, 9, 21, BoundType.OPEN, false, "(min..11) before (9..21) returns false (touching)")]
        [InlineData(int.MinValue, 16, BoundType.OPEN, 9, 21, BoundType.OPEN, false, "(min..16) before (9..21) returns false (overlapping)")]
        [InlineData(int.MinValue, 6, BoundType.OPEN, 9, int.MaxValue, BoundType.OPEN, true, "(min..6) before (9..max) returns true")]
        [InlineData(int.MinValue, 11, BoundType.OPEN, 9, int.MaxValue, BoundType.OPEN, false, "(min..11) before (9..max) returns false (touching)")]
        [InlineData(int.MinValue, 16, BoundType.OPEN, 9, int.MaxValue, BoundType.OPEN, false, "(min..16) before (9..max) returns false (overlapping)")]
        // Closed-closed range tests
        [InlineData(1, 5, BoundType.CLOSED, 10, 20, BoundType.CLOSED, true, "[1..5] before [10..20] returns true")]
        [InlineData(1, 5, BoundType.CLOSED, 5, 10, BoundType.CLOSED, false, "[1..5] before [5..10] return false (touching)")]
        [InlineData(1, 6, BoundType.CLOSED, 5, 10, BoundType.CLOSED, false, "[1..6] before [5..10] return false (overlapping)")]
        [InlineData(int.MinValue, 5, BoundType.CLOSED, 10, 20, BoundType.CLOSED, true, "[min..5] before [10..20] returns true")]
        [InlineData(int.MinValue, 5, BoundType.CLOSED, 5, 10, BoundType.CLOSED, false, "[min..5] before [5..10] return false (touching)")]
        [InlineData(int.MinValue, 6, BoundType.CLOSED, 5, 10, BoundType.CLOSED, false, "[min..6] before [5..10] return false (overlapping)")]
        [InlineData(int.MinValue, 5, BoundType.CLOSED, 10, int.MaxValue, BoundType.CLOSED, true, "[min..5] before [10..max] returns true")]
        [InlineData(int.MinValue, 5, BoundType.CLOSED, 5, int.MaxValue, BoundType.CLOSED, false, "[min..5] before [5..max] return false (touching)")]
        [InlineData(int.MinValue, 6, BoundType.CLOSED, 5, int.MaxValue, BoundType.CLOSED, false, "[min..6] before [5..max] return false (overlapping)")]
        // Open-closed range tests
        [InlineData(1, 6, BoundType.OPEN, 10, 20, BoundType.CLOSED, true, "(1..6) before [10..20] returns true")]
        [InlineData(1, 6, BoundType.OPEN, 5, 20, BoundType.CLOSED, false, "(1..6) before [5..20] returns false (touching)")]
        [InlineData(1, 6, BoundType.OPEN, 3, 20, BoundType.CLOSED, false, "(1..6) before [3..20] returns false (overlapping)")]
        [InlineData(int.MinValue, 6, BoundType.OPEN, 10, 20, BoundType.CLOSED, true, "(min..6) before [10..20] returns true")]
        [InlineData(int.MinValue, 6, BoundType.OPEN, 5, 20, BoundType.CLOSED, false, "(min..6) before [5..20] returns false (touching)")]
        [InlineData(int.MinValue, 6, BoundType.OPEN, 3, 20, BoundType.CLOSED, false, "(min..6) before [3..20] returns false (overlapping)")]
        [InlineData(int.MinValue, 6, BoundType.OPEN, 10, int.MaxValue, BoundType.CLOSED, true, "(min..6) before [10..max] returns true")]
        [InlineData(int.MinValue, 6, BoundType.OPEN, 5, int.MaxValue, BoundType.CLOSED, false, "(min..6) before [5..max] returns false (touching)")]
        [InlineData(int.MinValue, 6, BoundType.OPEN, 3, int.MaxValue, BoundType.CLOSED, false, "(min..6) before [3..max] returns false (overlapping)")]
        // Closed-open range tests
        [InlineData(0, 20, BoundType.CLOSED, 30, 50, BoundType.OPEN, true, "[0..20] before (30..50) returns true")]
        [InlineData(0, 20, BoundType.CLOSED, 19, 50, BoundType.OPEN, false, "[0..20] before (19..50) returns false (touching)")]
        [InlineData(0, 20, BoundType.CLOSED, 15, 50, BoundType.OPEN, false, "[0..20] before (15..50) returns false (overlapping)")]
        [InlineData(int.MinValue, 20, BoundType.CLOSED, 30, 50, BoundType.OPEN, true, "[min..20] before (30..50) returns true")]
        [InlineData(int.MinValue, 20, BoundType.CLOSED, 19, 50, BoundType.OPEN, false, "[min..20] before (19..50) returns false (touching)")]
        [InlineData(int.MinValue, 20, BoundType.CLOSED, 15, 50, BoundType.OPEN, false, "[min..20] before (15..50) returns false (overlapping)")]
        [InlineData(int.MinValue, 20, BoundType.CLOSED, 30, int.MaxValue, BoundType.OPEN, true, "[min..20] before (30..max) returns true")]
        [InlineData(int.MinValue, 20, BoundType.CLOSED, 19, int.MaxValue, BoundType.OPEN, false, "[min..20] before (19..max) returns false (touching)")]
        [InlineData(int.MinValue, 20, BoundType.CLOSED, 15, int.MaxValue, BoundType.OPEN, false, "[min..20] before (15..max) returns false (overlapping)")]
        public void ReturnsExpectedRangePrecedesResult(
            int rangeLower, int rangeUpper, BoundType rangeType,
            int otherLower, int otherUpper, BoundType otherType,
            bool expectedResult, string description)
        {
            var range = Helper.Create(rangeLower, rangeUpper, rangeType);
            var other = Helper.Create(otherLower, otherUpper, otherType);

            Assert.Equal(expectedResult, range.Precedes(other));
        }
    }
}
