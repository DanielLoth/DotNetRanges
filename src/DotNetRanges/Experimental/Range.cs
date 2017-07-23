using System;

namespace DotNetRanges.Experimental
{
    public partial struct Range<T> : IEquatable<Range<T>>
        where T : IComparable<T>
    {
        private readonly T _lowerBound;
        private readonly T _upperBound;
        private readonly RangeFlag _bitmask;

        public T LowerBound => HasLowerBound ? _lowerBound : throw new InvalidOperationException("Unbounded - no lower bound");
        public T UpperBound => HasUpperBound ? _upperBound : throw new InvalidOperationException("Unbounded - no upper bound");

        public bool HasLowerBound => (_bitmask & RangeFlag.HasLowerBound) != 0;
        public bool HasUpperBound => (_bitmask & RangeFlag.HasUpperBound) != 0;

        bool HasInfiniteLowerBound => (_bitmask & RangeFlag.LowerBelowAll) != 0;
        bool HasInfiniteUpperBound => (_bitmask & RangeFlag.UpperAboveAll) != 0;

        #region Constructors

        private Range(T lowerBound, T upperBound, RangeFlag bitmask)
        {
#if DEBUG
            bitmask.AssertMutualExclusion();
#endif

            _lowerBound = lowerBound;
            _upperBound = upperBound;
            _bitmask = bitmask;
        }

        #endregion

        #region Factory methods

        public static Range<T> Open(T lower, T upper)
        {
            var flags = RangeFlag.LowerAboveValue | RangeFlag.UpperBelowValue;

            return new Range<T>(lower, upper, flags);
        }

        public static Range<T> Closed(T lower, T upper)
        {
            var flags = RangeFlag.LowerBelowValue | RangeFlag.UpperAboveValue;

            return new Range<T>(lower, upper, flags);
        }

        public static Range<T> ClosedOpen(T lower, T upper)
        {
            var flags = RangeFlag.LowerBelowValue | RangeFlag.UpperBelowValue;

            return new Range<T>(lower, upper, flags);
        }

        public static Range<T> OpenClosed(T lower, T upper)
        {
            var flags = RangeFlag.LowerAboveValue | RangeFlag.UpperAboveValue;

            return new Range<T>(lower, upper, flags);
        }

        public static Range<T> GreaterThan(T lower)
        {
            var flags = RangeFlag.LowerAboveValue | RangeFlag.UpperAboveAll;

            return new Range<T>(lower, default(T), flags);
        }

        public static Range<T> AtLeast(T lower)
        {
            var flags = RangeFlag.LowerBelowValue | RangeFlag.UpperAboveAll;

            return new Range<T>(lower, default(T), flags);
        }

        public static Range<T> LessThan(T upper)
        {
            var flags = RangeFlag.LowerBelowAll | RangeFlag.UpperBelowValue;

            return new Range<T>(default(T), upper, flags);
        }

        public static Range<T> AtMost(T upper)
        {
            var flags = RangeFlag.LowerBelowAll | RangeFlag.UpperAboveValue;

            return new Range<T>(default(T), upper, flags);
        }

        public static Range<T> All()
        {
            var flags = RangeFlag.LowerBelowAll | RangeFlag.UpperAboveAll;

            return new Range<T>(default(T), default(T), flags);
        }

        #endregion

        #region IEquatable<T>

        public bool Equals(Range<T> other)
        {
            return LowerBoundCompareTo(other) == 0 && UpperBoundCompareTo(other) == 0;
        }

        #endregion

        #region Object overrides

        public override bool Equals(object obj)
        {
            var isValidType = obj is Range<T>;

            if (!isValidType)
            {
                return false;
            }

            var other = (Range<T>)obj;
            return Equals(other);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return _bitmask.RangeToString(this);
        }

        #endregion
    }
}
