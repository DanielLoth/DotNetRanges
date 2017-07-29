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

            return leftUpper.CompareTo(rightLower) == 0;
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

        #endregion

        #region Starts / Started by

        #endregion

        #region During / Contains

        #endregion

        #region Finishes / Finished by

        #endregion

        #region Equals

        #endregion
    }
}
