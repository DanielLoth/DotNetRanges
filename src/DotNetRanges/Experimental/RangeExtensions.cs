using System;

namespace DotNetRanges.Experimental
{
    public static partial class RangeExtensions
    {
        #region Precedes / Preceded By

        private static bool PrecedesInternal(Range<int> left, Range<int> right)
        {
            var leftUpper = left.UpperBound.EndpointAsUpperClosed();
            var rightLower = right.LowerBound.EndpointAsLowerClosed();

            return leftUpper.CompareTo(rightLower) < 0;
        }

        private static bool PrecedesInternal(Range<long> left, Range<long> right)
        {
            var leftUpper = left.UpperBound.EndpointAsUpperClosed();
            var rightLower = right.LowerBound.EndpointAsLowerClosed();

            return leftUpper.CompareTo(rightLower) < 0;
        }

        public static bool Precedes(this Range<int> range, Range<int> other)
        {
            return PrecedesInternal(range, other);
        }

        public static bool Precedes(this Range<long> range, Range<long> other)
        {
            return PrecedesInternal(range, other);
        }

        public static bool PrecededBy(this Range<int> range, Range<int> other)
        {
            return PrecedesInternal(other, range);
        }

        public static bool PrecededBy(this Range<long> range, Range<long> other)
        {
            return PrecedesInternal(other, range);
        }

        #endregion

        #region Meets

        private static bool MeetsInternal(Range<int> left, Range<int> right)
        {
            var leftUpper = left.UpperBound.EndpointAsUpperClosed();
            var rightLower = right.LowerBound.EndpointAsLowerClosed();

            return leftUpper.CompareTo(rightLower) == 0;
        }

        private static bool MeetsInternal(Range<long> left, Range<long> right)
        {
            var leftUpper = left.UpperBound.EndpointAsUpperClosed();
            var rightLower = right.LowerBound.EndpointAsLowerClosed();

            return leftUpper.CompareTo(rightLower) == 0;
        }

        public static bool Meets(this Range<int> range, Range<int> other)
        {
            return MeetsInternal(range, other) || MeetsInternal(other, range);
        }

        public static bool Meets(this Range<long> range, Range<long> other)
        {
            return MeetsInternal(range, other) || MeetsInternal(other, range);
        }

        #endregion

        #region Overlaps

        #endregion

        #region Starts

        #endregion

        #region During

        #endregion

        #region Finishes

        #endregion

        #region Equals

        #endregion
    }
}
