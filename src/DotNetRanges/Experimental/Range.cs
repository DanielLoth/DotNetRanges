using System;

namespace DotNetRanges.Experimental
{
    public partial struct Range<T> : IEquatable<Range<T>>
        where T : IComparable<T>
    {
        private readonly T _lowerEndpoint;
        private readonly T _upperEndpoint;
        private readonly RangeFlag _bitmask;

        public T LowerEndpoint => HasLowerEndpoint ? _lowerEndpoint : throw new InvalidOperationException("Unbounded - no lower bound");
        public T UpperEndpoint => HasUpperEndpoint ? _upperEndpoint : throw new InvalidOperationException("Unbounded - no upper bound");

        public bool HasLowerEndpoint => (_bitmask & RangeFlag.HasLowerBound) != 0;
        public bool HasUpperEndpoint => (_bitmask & RangeFlag.HasUpperBound) != 0;

        bool HasInfiniteLowerEndpoint => (_bitmask & RangeFlag.LowerInfiniteBound) != 0;
        bool HasInfiniteUpperEndpoint => (_bitmask & RangeFlag.UpperInfiniteBound) != 0;

        #region Constructors

        private Range(T lowerEndpoint, T upperEndpoint, RangeFlag bitmask)
        {
#if DEBUG
            bitmask.AssertMutualExclusion();
#endif

            _lowerEndpoint = lowerEndpoint;
            _upperEndpoint = upperEndpoint;
            _bitmask = bitmask;
        }

        #endregion

        #region Factory methods

        public static Range<T> Open(T lower, T upper)
        {
            return new Range<T>(lower, upper, RangeFlag.Open);
        }

        public static Range<T> Closed(T lower, T upper)
        {
            return new Range<T>(lower, upper, RangeFlag.Closed);
        }

        public static Range<T> ClosedOpen(T lower, T upper)
        {
            return new Range<T>(lower, upper, RangeFlag.ClosedOpen);
        }

        public static Range<T> OpenClosed(T lower, T upper)
        {
            return new Range<T>(lower, upper, RangeFlag.OpenClosed);
        }

        public static Range<T> GreaterThan(T lower)
        {
            return new Range<T>(lower, default(T), RangeFlag.GreaterThan);
        }

        public static Range<T> AtLeast(T lower)
        {
            return new Range<T>(lower, default(T), RangeFlag.AtLeast);
        }

        public static Range<T> LessThan(T upper)
        {
            return new Range<T>(default(T), upper, RangeFlag.LessThan);
        }

        public static Range<T> AtMost(T upper)
        {
            return new Range<T>(default(T), upper, RangeFlag.AtMost);
        }

        public static Range<T> All()
        {
            return new Range<T>(default(T), default(T), RangeFlag.All);
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
