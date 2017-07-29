namespace DotNetRanges.Experimental
{
    public static class IntegerRangeExtensions
    {
        #region Precedes / Preceded by

        private static bool PrecedesInternal(Range<int> left, Range<int> right)
        {
            var leftUpper = left.UpperBound.EndpointAsUpperClosed();
            var rightLower = right.LowerBound.EndpointAsLowerClosed();

            return leftUpper.CompareTo(rightLower) < 0;
        }

        public static bool Precedes(this Range<int> range, Range<int> other)
        {
            return PrecedesInternal(range, other);
        }

        public static bool PrecededBy(this Range<int> range, Range<int> other)
        {
            return PrecedesInternal(other, range);
        }

        #endregion

        #region Meets / Met by

        private static bool MeetsInternal(Range<int> left, Range<int> right)
        {
            var leftUpper = left.UpperBound.EndpointAsUpperClosed();
            var rightLower = right.LowerBound.EndpointAsLowerClosed();

            return leftUpper == rightLower;
        }

        public static bool Meets(this Range<int> range, Range<int> other)
        {
            return MeetsInternal(range, other);
        }

        public static bool MetBy(this Range<int> range, Range<int> other)
        {
            return MeetsInternal(other, range);
        }

        #endregion

        #region Overlaps / Overlapped by

        private static bool OverlapsInternal(Range<int> left, Range<int> right)
        {
            var leftUpper = left.UpperBound.EndpointAsUpperClosed();
            var rightLower = right.LowerBound.EndpointAsLowerClosed();

            if (leftUpper <= rightLower)
            {
                return false;
            }

            var leftLower = left.LowerBound.EndpointAsLowerClosed();
            var rightUpper = right.UpperBound.EndpointAsUpperClosed();

            return leftLower < rightLower && leftUpper < rightUpper;
        }

        public static bool Overlaps(this Range<int> range, Range<int> other)
        {
            return OverlapsInternal(range, other);
        }

        public static bool OverlappedBy(this Range<int> range, Range<int> other)
        {
            return OverlapsInternal(other, range);
        }

        #endregion

        #region Starts / Started by

        private static bool StartsInternal(Range<int> left, Range<int> right)
        {
            var leftLower = left.LowerBound.EndpointAsLowerClosed();
            var rightLower = right.LowerBound.EndpointAsLowerClosed();

            return leftLower == rightLower;
        }

        public static bool Starts(this Range<int> range, Range<int> other)
        {
            return StartsInternal(range, other);
        }

        public static bool StartedBy(this Range<int> range, Range<int> other)
        {
            return StartsInternal(other, range);
        }

        #endregion

        #region During / Contains

        private static bool ContainsInternal(Range<int> left, Range<int> right)
        {
            var leftLower = left.LowerBound.EndpointAsLowerClosed();
            var rightLower = right.LowerBound.EndpointAsLowerClosed();

            if (leftLower.CompareTo(rightLower) >= 0)
            {
                return false;
            }

            var leftUpper = left.UpperBound.EndpointAsUpperClosed();
            var rightUpper = right.UpperBound.EndpointAsUpperClosed();

            return leftUpper.CompareTo(rightUpper) > 0;
        }

        public static bool Contains(this Range<int> range, Range<int> other)
        {
            return ContainsInternal(range, other);
        }

        public static bool During(this Range<int> range, Range<int> other)
        {
            return ContainsInternal(other, range);
        }

        #endregion

        #region Finishes / Finished by

        private static bool FinishesInternal(Range<int> left, Range<int> right)
        {
            var leftUpper = left.UpperBound.EndpointAsUpperClosed();
            var rightUpper = right.UpperBound.EndpointAsUpperClosed();

            return leftUpper == rightUpper;
        }

        public static bool Finishes(this Range<int> range, Range<int> other)
        {
            return FinishesInternal(range, other);
        }

        public static bool FinishedBy(this Range<int> range, Range<int> other)
        {
            return FinishesInternal(other, range);
        }

        #endregion

        #region Equals

        private static bool EquivalentInternal(Range<int> left, Range<int> right)
        {
            var leftLower = left.LowerBound.EndpointAsLowerClosed();
            var rightLower = right.LowerBound.EndpointAsLowerClosed();

            if (leftLower != rightLower)
            {
                return false;
            }

            var leftUpper = left.UpperBound.EndpointAsUpperClosed();
            var rightUpper = right.UpperBound.EndpointAsUpperClosed();

            return leftUpper == rightUpper;
        }

        public static bool Equivalent(this Range<int> range, Range<int> other)
        {
            return EquivalentInternal(range, other);
        }

        #endregion
    }
}
