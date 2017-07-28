using System;

namespace DotNetRanges.Experimental
{
    public partial struct Range<T>
    {
        private int LowerInfiniteBoundCompareTo(Range<T> other)
        {
            return (other._bitmask & RangeFlag.LowerInfiniteBound) != 0 ? 0 : -1;
        }

        private int UpperInfiniteBoundCompareTo(Range<T> other)
        {
            return (other._bitmask & RangeFlag.UpperInfiniteBound) != 0 ? 0 : 1;
        }

        private int FiniteBoundCompareTo(RangeFlag thisFlags, RangeFlag otherFlags, T thisEndpoint, T otherEndpoint)
        {
            if ((otherFlags & RangeFlag.LowerInfiniteBound) != 0) return 1;
            else if ((otherFlags & RangeFlag.UpperInfiniteBound) != 0) return -1;

            var result = thisEndpoint.CompareTo(otherEndpoint);
            if (result != 0) return result;

            return thisFlags.CompareTo(otherFlags);
        }

        public int LowerBoundCompareTo(Range<T> other)
        {
#if DEBUG
            _bitmask.AssertHasLowerBound();
            other._bitmask.AssertHasLowerBound();
#endif

            var thisFlags = _bitmask & RangeFlag.AllLowerBits;
            var otherFlags = other._bitmask & RangeFlag.AllLowerBits;

            if ((thisFlags & RangeFlag.LowerInfiniteBound) != 0) return LowerInfiniteBoundCompareTo(other);
            else return FiniteBoundCompareTo(thisFlags, otherFlags, _lowerEndpoint, other._lowerEndpoint);
        }

        public int UpperBoundCompareTo(Range<T> other)
        {
#if DEBUG
            _bitmask.AssertHasUpperBound();
            other._bitmask.AssertHasUpperBound();
#endif

            var thisFlags = _bitmask & RangeFlag.AllUpperBits;
            var otherFlags = other._bitmask & RangeFlag.AllUpperBits;

            if ((thisFlags & RangeFlag.UpperInfiniteBound) != 0) return UpperInfiniteBoundCompareTo(other);
            else return FiniteBoundCompareTo(thisFlags, otherFlags, _upperEndpoint, other._upperEndpoint);
        }
    }
}
