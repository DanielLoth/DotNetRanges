using System;
using System.Text;

namespace DotNetRanges.Experimental
{
    public class Range<T> : IEquatable<Range<T>>
        where T : IComparable<T>
    {
        private ICut<T> _lowerBound;
        private ICut<T> _upperBound;

        public BoundType LowerBoundType => _lowerBound.TypeAsLowerBound;
        public BoundType UpperBoundType => _upperBound.TypeAsUpperBound;

        public T LowerEndpoint => _lowerBound.Endpoint;
        public T UpperEndpoint => _upperBound.Endpoint;

        public bool HasLowerEndpoint => _lowerBound.HasEndpoint;
        public bool HasUpperEndpoint => _upperBound.HasEndpoint;

        #region Constructors

        private Range(ICut<T> lowerBound, ICut<T> upperBound)
        {
            _lowerBound = lowerBound ?? throw new ArgumentNullException(nameof(lowerBound));
            _upperBound = upperBound ?? throw new ArgumentNullException(nameof(upperBound));
        }

        #endregion

        #region Factory methods

        internal static Range<T> Create(ICut<T> lowerBound, ICut<T> upperBound)
        {
            return new Range<T>(lowerBound, upperBound);
        }

        public static Range<T> Open(T lower, T upper)
        {
            return Create(Cut<T>.LowerOpenBound(lower), Cut<T>.UpperOpenBound(upper));
        }

        public static Range<T> Closed(T lower, T upper)
        {
            return Create(Cut<T>.LowerClosedBound(lower), Cut<T>.UpperClosedBound(upper));
        }

        public static Range<T> ClosedOpen(T lower, T upper)
        {
            return Create(Cut<T>.LowerClosedBound(lower), Cut<T>.UpperOpenBound(upper));
        }

        public static Range<T> OpenClosed(T lower, T upper)
        {
            return Create(Cut<T>.LowerOpenBound(lower), Cut<T>.UpperClosedBound(upper));
        }

        public static Range<T> GreaterThan(T lower)
        {
            return Create(Cut<T>.LowerOpenBound(lower), Cut<T>.AboveAll());
        }

        public static Range<T> AtLeast(T lower)
        {
            return Create(Cut<T>.LowerClosedBound(lower), Cut<T>.AboveAll());
        }

        public static Range<T> LessThan(T upper)
        {
            return Create(Cut<T>.BelowAll(), Cut<T>.UpperOpenBound(upper));
        }

        public static Range<T> AtMost(T upper)
        {
            return Create(Cut<T>.BelowAll(), Cut<T>.UpperClosedBound(upper));
        }

        public static Range<T> All()
        {
            return Create(Cut<T>.BelowAll(), Cut<T>.AboveAll());
        }

        #endregion

        #region IEquatable<T>

        public static bool Equals(Range<T> left, Range<T> right)
        {
            if (left == null || right == null) return false;
            if (ReferenceEquals(left, right)) return true;

            return left._lowerBound.CompareTo(right._lowerBound) == 0 &&
                left._upperBound.CompareTo(right._upperBound) == 0;
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
            // TODO: Write this in terms of the upper and lower bounds.
            return base.GetHashCode();
        }

        public override string ToString()
        {
            var builder = new StringBuilder(32);

            _lowerBound.DescribeAsLowerBound(builder);
            builder.Append("..");
            _upperBound.DescribeAsUpperBound(builder);

            return builder.ToString();
        }

        #endregion
    }
}
