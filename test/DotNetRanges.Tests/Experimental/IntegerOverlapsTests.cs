using DotNetRanges.Experimental;
using Xunit;

namespace DotNetRanges.Tests.Experimental
{
    public class IntegerOverlapsTests
    {
        [Theory]
        [InlineData(0, 20, BoundType.CLOSED, 50, 100, BoundType.CLOSED, false, "[0..20] overlaps [50..100] returns false (precedes)")]
        [InlineData(0, 50, BoundType.CLOSED, 50, 100, BoundType.CLOSED, false, "[0..50] overlaps [50..100] returns false (meets)")]
        [InlineData(0, 51, BoundType.CLOSED, 50, 100, BoundType.CLOSED, true, @"[0..51] overlaps [50..100] returns true (overlaps)")]
        [InlineData(0, 100, BoundType.CLOSED, 50, 100, BoundType.CLOSED, false, "[0..100] overlaps [50..100] returns false (finished by)")]
        [InlineData(0, 40, BoundType.CLOSED, 10, 20, BoundType.CLOSED, false, "[0..40] overlaps [10..20] returns false (contains)")]
        [InlineData(0, 40, BoundType.CLOSED, 0, 100, BoundType.CLOSED, false, "[0..40] overlaps [0..100] returns false (starts)")] // BROKEN
        [InlineData(0, 40, BoundType.CLOSED, 0, 40, BoundType.CLOSED, false, "[0..40] overlaps [50..100] returns false (equals)")]
        [InlineData(0, 40, BoundType.CLOSED, 0, 20, BoundType.CLOSED, false, "[0..40] overlaps [0..20] returns false (started by)")]
        [InlineData(0, 40, BoundType.CLOSED, -50, 100, BoundType.CLOSED, false, "[0..40] overlaps [-50..100] returns false (during)")] // BROKEN
        [InlineData(0, 100, BoundType.CLOSED, -50, 100, BoundType.CLOSED, false, "[0..100] overlaps [-50..100] returns false (finishes)")]
        [InlineData(0, 40, BoundType.CLOSED, 50, 100, BoundType.CLOSED, false, "[0..40] overlaps [50..100] returns false (overlapped by)")]
        [InlineData(0, 40, BoundType.CLOSED, 50, 100, BoundType.CLOSED, false, "[0..40] overlaps [50..100] returns false (met by)")]
        [InlineData(0, 40, BoundType.CLOSED, 50, 100, BoundType.CLOSED, false, "[0..40] overlaps [50..100] returns false (preceded by)")]
        public void ReturnsExpectedRangeOverlapsResult(
            int rangeLower, int rangeUpper, BoundType rangeType,
            int otherLower, int otherUpper, BoundType otherType,
            bool expectedResult, string description)
        {
            var range = Helper.Create(rangeLower, rangeUpper, rangeType);
            var other = Helper.Create(otherLower, otherUpper, otherType);
            var result = range.Overlaps(other);

            Assert.Equal(expectedResult, range.Overlaps(other));
        }

        //[Theory]
        //[InlineData(0, 20, BoundType.CLOSED, BoundType.CLOSED, Operation.Precedes, false, "[0..20] overlaps [50..100] returns false (precedes)")]
        //[InlineData(0, 50, BoundType.CLOSED, BoundType.CLOSED, Operation.Meets, false, "[0..50] overlaps [50..100] returns false (meets)")]
        //[InlineData(0, 51, BoundType.CLOSED, BoundType.CLOSED, Operation.Overlaps, true, @"[0..51] overlaps [50..100] returns true (overlaps)")]
        //[InlineData(0, 100, BoundType.CLOSED, BoundType.CLOSED, Operation.FinishedBy, false, "[0..100] overlaps [50..100] returns false (finished by)")]
        //[InlineData(0, 40, BoundType.CLOSED, BoundType.CLOSED, Operation.Contains, false, "[0..40] overlaps [10..20] returns false (contains)")]
        //[InlineData(0, 40, BoundType.CLOSED, BoundType.CLOSED, Operation.Starts, false, "[0..40] overlaps [0..100] returns false (starts)")]
        //[InlineData(0, 40, BoundType.CLOSED, BoundType.CLOSED, Operation.Equals, false, "[0..40] overlaps [50..100] returns false (equals)")]
        //[InlineData(0, 40, BoundType.CLOSED, BoundType.CLOSED, Operation.StartedBy, false, "[0..40] overlaps [0..20] returns false (started by)")]
        //[InlineData(0, 40, BoundType.CLOSED, BoundType.CLOSED, Operation.During, false, "[0..40] overlaps [-50..100] returns false (during)")]
        //[InlineData(0, 100, BoundType.CLOSED, BoundType.CLOSED, Operation.Finishes, false, "[0..100] overlaps [-50..100] returns false (finishes)")]
        //[InlineData(0, 40, BoundType.CLOSED, BoundType.CLOSED, Operation.OverlappedBy, false, "[0..40] overlaps [50..100] returns false (overlapped by)")]
        //[InlineData(0, 40, BoundType.CLOSED, BoundType.CLOSED, Operation.MetBy, false, "[0..40] overlaps [50..100] returns false (met by)")]
        //[InlineData(0, 40, BoundType.CLOSED, BoundType.CLOSED, Operation.PrecededBy, false, "[0..40] overlaps [50..100] returns false (preceded by)")]
        //public void asdf(
        //    int rangeLower, int rangeUpper, BoundType rangeType,
        //    BoundType otherType, Operation operation,
        //    bool expectedResult, string description)
        //{
        //    var range = Helper.Create(rangeLower, rangeUpper, rangeType);
        //    var other = Helper.CreateOther(range, otherType, operation);

        //    var result = range.Overlaps(other);

        //    Assert.Equal(expectedResult, result);
        //}
    }
}
