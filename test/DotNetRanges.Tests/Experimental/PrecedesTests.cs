using DotNetRanges.Experimental;
using System;
using Xunit;

namespace DotNetRanges.Tests.Experimental
{
    public class PrecedesTests
    {
        private Range<T> Create<T>(T lower, T upper, BoundType type)
            where T : IComparable<T>
        {
            switch (type)
            {
                case BoundType.OPEN: return Range<T>.Open(lower, upper);
                case BoundType.CLOSED: return Range<T>.Closed(lower, upper);
                default: throw new InvalidOperationException("This method only supports OPEN and CLOSED ranges");
            }
        }

        #region Theories

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
        public void ReturnsExpectedValueIntegerRangePrecedes(
            int rangeLower, int rangeUpper, BoundType rangeType,
            int otherLower, int otherUpper, BoundType otherType,
            bool expectedResult, string description)
        {
            var range = Create(rangeLower, rangeUpper, rangeType);
            var other = Create(otherLower, otherUpper, otherType);

            Assert.Equal(expectedResult, range.Precedes(other));
        }

        [Theory]
        // Open-open range tests
        [InlineData(0, 6, BoundType.OPEN, 9, 21, BoundType.OPEN, true, "(0..6) before (9..21) returns true")]
        [InlineData(0, 11, BoundType.OPEN, 9, 21, BoundType.OPEN, false, "(0..11) before (9..21) returns false (touching)")]
        [InlineData(0, 16, BoundType.OPEN, 9, 21, BoundType.OPEN, false, "(0..16) before (9..21) returns false (overlapping)")]
        [InlineData(long.MinValue, 6, BoundType.OPEN, 9, 21, BoundType.OPEN, true, "(min..6) before (9..21) returns true")]
        [InlineData(long.MinValue, 11, BoundType.OPEN, 9, 21, BoundType.OPEN, false, "(min..11) before (9..21) returns false (touching)")]
        [InlineData(long.MinValue, 16, BoundType.OPEN, 9, 21, BoundType.OPEN, false, "(min..16) before (9..21) returns false (overlapping)")]
        [InlineData(long.MinValue, 6, BoundType.OPEN, 9, long.MaxValue, BoundType.OPEN, true, "(min..6) before (9..max) returns true")]
        [InlineData(long.MinValue, 11, BoundType.OPEN, 9, long.MaxValue, BoundType.OPEN, false, "(min..11) before (9..max) returns false (touching)")]
        [InlineData(long.MinValue, 16, BoundType.OPEN, 9, long.MaxValue, BoundType.OPEN, false, "(min..16) before (9..max) returns false (overlapping)")]
        // Closed-closed range tests
        [InlineData(1, 5, BoundType.CLOSED, 10, 20, BoundType.CLOSED, true, "[1..5] before [10..20] returns true")]
        [InlineData(1, 5, BoundType.CLOSED, 5, 10, BoundType.CLOSED, false, "[1..5] before [5..10] return false (touching)")]
        [InlineData(1, 6, BoundType.CLOSED, 5, 10, BoundType.CLOSED, false, "[1..6] before [5..10] return false (overlapping)")]
        [InlineData(long.MinValue, 5, BoundType.CLOSED, 10, 20, BoundType.CLOSED, true, "[min..5] before [10..20] returns true")]
        [InlineData(long.MinValue, 5, BoundType.CLOSED, 5, 10, BoundType.CLOSED, false, "[min..5] before [5..10] return false (touching)")]
        [InlineData(long.MinValue, 6, BoundType.CLOSED, 5, 10, BoundType.CLOSED, false, "[min..6] before [5..10] return false (overlapping)")]
        [InlineData(long.MinValue, 5, BoundType.CLOSED, 10, long.MaxValue, BoundType.CLOSED, true, "[min..5] before [10..max] returns true")]
        [InlineData(long.MinValue, 5, BoundType.CLOSED, 5, long.MaxValue, BoundType.CLOSED, false, "[min..5] before [5..max] return false (touching)")]
        [InlineData(long.MinValue, 6, BoundType.CLOSED, 5, long.MaxValue, BoundType.CLOSED, false, "[min..6] before [5..max] return false (overlapping)")]
        // Open-closed range tests
        [InlineData(1, 6, BoundType.OPEN, 10, 20, BoundType.CLOSED, true, "(1..6) before [10..20] returns true")]
        [InlineData(1, 6, BoundType.OPEN, 5, 20, BoundType.CLOSED, false, "(1..6) before [5..20] returns false (touching)")]
        [InlineData(1, 6, BoundType.OPEN, 3, 20, BoundType.CLOSED, false, "(1..6) before [3..20] returns false (overlapping)")]
        [InlineData(long.MinValue, 6, BoundType.OPEN, 10, 20, BoundType.CLOSED, true, "(min..6) before [10..20] returns true")]
        [InlineData(long.MinValue, 6, BoundType.OPEN, 5, 20, BoundType.CLOSED, false, "(min..6) before [5..20] returns false (touching)")]
        [InlineData(long.MinValue, 6, BoundType.OPEN, 3, 20, BoundType.CLOSED, false, "(min..6) before [3..20] returns false (overlapping)")]
        [InlineData(long.MinValue, 6, BoundType.OPEN, 10, long.MaxValue, BoundType.CLOSED, true, "(min..6) before [10..max] returns true")]
        [InlineData(long.MinValue, 6, BoundType.OPEN, 5, long.MaxValue, BoundType.CLOSED, false, "(min..6) before [5..max] returns false (touching)")]
        [InlineData(long.MinValue, 6, BoundType.OPEN, 3, long.MaxValue, BoundType.CLOSED, false, "(min..6) before [3..max] returns false (overlapping)")]
        // Closed-open range tests
        [InlineData(0, 20, BoundType.CLOSED, 30, 50, BoundType.OPEN, true, "[0..20] before (30..50) returns true")]
        [InlineData(0, 20, BoundType.CLOSED, 19, 50, BoundType.OPEN, false, "[0..20] before (19..50) returns false (touching)")]
        [InlineData(0, 20, BoundType.CLOSED, 15, 50, BoundType.OPEN, false, "[0..20] before (15..50) returns false (overlapping)")]
        [InlineData(long.MinValue, 20, BoundType.CLOSED, 30, 50, BoundType.OPEN, true, "[min..20] before (30..50) returns true")]
        [InlineData(long.MinValue, 20, BoundType.CLOSED, 19, 50, BoundType.OPEN, false, "[min..20] before (19..50) returns false (touching)")]
        [InlineData(long.MinValue, 20, BoundType.CLOSED, 15, 50, BoundType.OPEN, false, "[min..20] before (15..50) returns false (overlapping)")]
        [InlineData(long.MinValue, 20, BoundType.CLOSED, 30, long.MaxValue, BoundType.OPEN, true, "[min..20] before (30..max) returns true")]
        [InlineData(long.MinValue, 20, BoundType.CLOSED, 19, long.MaxValue, BoundType.OPEN, false, "[min..20] before (19..max) returns false (touching)")]
        [InlineData(long.MinValue, 20, BoundType.CLOSED, 15, long.MaxValue, BoundType.OPEN, false, "[min..20] before (15..max) returns false (overlapping)")]
        public void ReturnsExpectedValueLongRangePrecedes(
            long rangeLower, long rangeUpper, BoundType rangeType,
            long otherLower, long otherUpper, BoundType otherType,
            bool expectedResult, string description)
        {
            var range = Create(rangeLower, rangeUpper, rangeType);
            var other = Create(otherLower, otherUpper, otherType);

            Assert.Equal(expectedResult, range.Precedes(other));
        }

        #endregion

        #region Integer range tests

        [Fact]
        public void ReturnsTrueWhenMinimumIntOpenRangeIsBeforeIntOpenRange()
        {
            var min = Range<int>.Open(int.MinValue, 5);
            var other = Range<int>.Open(10, 20);

            Assert.True(min.Precedes(other));
        }

        [Fact]
        public void ReturnsTrueWhenMinimumIntOpenRangeIsBeforeMaximumIntOpenRange()
        {
            var min = Range<int>.Open(int.MinValue, 100);
            var max = Range<int>.Open(200, int.MaxValue);

            Assert.True(min.Precedes(max));
        }

        [Fact]
        public void ReturnsFalseWhenOpenFullRangeBeforeOpenFullRange()
        {
            var a = Range<int>.Open(int.MinValue, int.MaxValue);
            var b = Range<int>.Open(int.MinValue, int.MaxValue);

            Assert.False(a.Precedes(b));
        }

        [Fact]
        public void ReturnsFalseWhenRangeIsBeforeItself()
        {
            var range = Range<int>.Open(1, 100);
            Assert.False(range.Precedes(range));
        }

        [Fact]
        public void ReturnsTrueWhenInfiniteRangeBeforeOpenRange()
        {
            var infinite = Range<int>.LessThan(10);
            var other = Range<int>.Open(20, 40);

            Assert.True(infinite.Precedes(other));
        }

        [Fact]
        public void ReturnsFalseWhenInfiniteRangeTouchesOpenRange()
        {
            var infinite = Range<int>.LessThan(10); // [inf..9]
            var other = Range<int>.Open(8, 40); // [9..39]

            Assert.False(infinite.Precedes(other));
        }

        [Fact]
        public void ReturnsTrueWhenInfiniteOpenRangeBeforeInfiniteOpenRange()
        {
            var infLower = Range<int>.LessThan(10);
            var infUpper = Range<int>.GreaterThan(20);

            Assert.True(infLower.Precedes(infUpper));
        }

        [Fact]
        public void ReturnsTrueWhenIntOpenRangeIsBeforeIntOpenRange()
        {
            var a = Range<int>.Open(1, 5); // [2..4]
            var b = Range<int>.Open(10, 20); // [11..19]

            Assert.True(a.Precedes(b));
        }

        [Fact]
        public void ReturnsFalseWhenIntOpenRangeTouchesIntOpenRange()
        {
            var a = Range<int>.Open(1, 5); // [2..4]
            var b = Range<int>.Open(3, 10); // [4..9]

            Assert.False(a.Precedes(b));
        }

        [Fact]
        public void ReturnsFalseWhenIntOpenRangeOverlapsIntOpenRange()
        {
            var a = Range<int>.Open(1, 5); // [2..4]
            var b = Range<int>.Open(2, 10); // [3..9]

            Assert.False(a.Precedes(b));
        }

        [Fact]
        public void ReturnsTrueWhenIntClosedRangeIsBeforeIntClosedRange()
        {
            var a = Range<int>.Closed(1, 5);
            var b = Range<int>.Closed(6, 10);

            Assert.True(a.Precedes(b));
        }

        [Fact]
        public void ReturnsFalseWhenIntClosedRangeTouchesIntClosedRange()
        {
            var a = Range<int>.Closed(1, 6);
            var b = Range<int>.Closed(6, 10);

            Assert.False(a.Precedes(b));
        }

        [Fact]
        public void ReturnsFalseWhenIntClosedRangeOverlapsIntClosedRange()
        {
            var a = Range<int>.Closed(1, 7);
            var b = Range<int>.Closed(6, 10);

            Assert.False(a.Precedes(b));
        }

        [Fact]
        public void ReturnsTrueWhenIntOpenRangeIsBeforeIntClosedRange()
        {
            var a = Range<int>.Open(0, 6); // [1..5]
            var b = Range<int>.Closed(10, 15);

            Assert.True(a.Precedes(b));
        }

        [Fact]
        public void ReturnsTrueWhenIntClosedRangeIsBeforeIntOpenRange()
        {
            var a = Range<int>.Closed(1, 10);
            var b = Range<int>.Open(15, 20); // [16..19]

            Assert.True(a.Precedes(b));
        }

        #endregion
    }
}
