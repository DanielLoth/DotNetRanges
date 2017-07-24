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

        bool HasInfiniteLowerBound => (_bitmask & RangeFlag.LowerInfiniteBound) != 0;
        bool HasInfiniteUpperBound => (_bitmask & RangeFlag.UpperInfiniteBound) != 0;

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
            var flags = RangeFlag.LowerOpenBound | RangeFlag.UpperOpenBound;

            return new Range<T>(lower, upper, flags);
        }

        public static Range<T> Closed(T lower, T upper)
        {
            var flags = RangeFlag.LowerClosedBound | RangeFlag.UpperClosedBound;

            return new Range<T>(lower, upper, flags);
        }

        public static Range<T> ClosedOpen(T lower, T upper)
        {
            var flags = RangeFlag.LowerClosedBound | RangeFlag.UpperOpenBound;

            return new Range<T>(lower, upper, flags);
        }

        public static Range<T> OpenClosed(T lower, T upper)
        {
            var flags = RangeFlag.LowerOpenBound | RangeFlag.UpperClosedBound;

            return new Range<T>(lower, upper, flags);
        }

        public static Range<T> GreaterThan(T lower)
        {
            var flags = RangeFlag.LowerOpenBound | RangeFlag.UpperInfiniteBound;

            return new Range<T>(lower, default(T), flags);
        }

        public static Range<T> AtLeast(T lower)
        {
            var flags = RangeFlag.LowerClosedBound | RangeFlag.UpperInfiniteBound;

            return new Range<T>(lower, default(T), flags);
        }

        public static Range<T> LessThan(T upper)
        {
            var flags = RangeFlag.LowerInfiniteBound | RangeFlag.UpperOpenBound;

            return new Range<T>(default(T), upper, flags);
        }

        public static Range<T> AtMost(T upper)
        {
            var flags = RangeFlag.LowerInfiniteBound | RangeFlag.UpperClosedBound;

            return new Range<T>(default(T), upper, flags);
        }

        public static Range<T> All()
        {
            var flags = RangeFlag.LowerInfiniteBound | RangeFlag.UpperInfiniteBound;

            return new Range<T>(default(T), default(T), flags);
        }

        #endregion

        #region IEquatable<T>

        public static bool Equals(Range<T> left, Range<T> right)
        {
            return left.LowerBoundCompareTo(right) == 0 && left.UpperBoundCompareTo(right) == 0;
        }

        public bool Equals(Range<T> other)
        {
            return Equals(this, other);
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
