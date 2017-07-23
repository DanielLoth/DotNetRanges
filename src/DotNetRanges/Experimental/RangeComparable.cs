using System;

namespace DotNetRanges.Experimental
{
    public partial struct Range<T>
    {
        public int LowerBoundCompareTo(Range<T> other)
        {
            // TODO: Haven't worked through the logic here properly yet. Finish me.

            var flags = _bitmask;
            var otherFlags = other._bitmask;

            if ((flags & RangeFlag.LowerBelowValue) != 0)
            {
                if ((otherFlags & RangeFlag.LowerBelowValue) != 0)
                {
                    var blah = _lowerBound.CompareTo(other.LowerBound);
                    return blah;
                }
                else if ((otherFlags & RangeFlag.LowerAboveValue) != 0)
                {
                    var blah = _lowerBound.CompareTo(other.LowerBound);
                    return blah;
                }
                else if ((otherFlags & RangeFlag.LowerBelowAll) != 0)
                {
                    return 1;
                }
            }
            else if ((flags & RangeFlag.LowerAboveValue) != 0)
            {
                if ((otherFlags & RangeFlag.LowerBelowValue) != 0)
                {
                    var blah = _lowerBound.CompareTo(other.LowerBound);
                    return blah;
                }
                else if ((otherFlags & RangeFlag.LowerAboveValue) != 0)
                {
                    var blah = _lowerBound.CompareTo(other.LowerBound);
                    return blah;
                }
                else if ((otherFlags & RangeFlag.LowerBelowAll) != 0)
                {
                    return 1;
                }
            }
            else if ((flags & RangeFlag.LowerBelowAll) != 0)
            {
                return (otherFlags & RangeFlag.LowerBelowAll) != 0 ? 0 : -1;
            }

            throw new InvalidOperationException();
        }

        public int UpperBoundCompareTo(Range<T> other)
        {
            // TODO: Haven't worked through the logic here properly yet. Finish me.

            var flags = _bitmask;
            var otherFlags = other._bitmask;

            if ((flags & RangeFlag.UpperBelowValue) != 0)
            {
                if ((otherFlags & RangeFlag.UpperBelowValue) != 0)
                {

                }
                else if ((otherFlags & RangeFlag.UpperAboveValue) != 0)
                {

                }
                else if ((otherFlags & RangeFlag.UpperAboveAll) != 0)
                {
                    return -1;
                }
            }
            else if ((flags & RangeFlag.UpperAboveValue) != 0)
            {
                if ((otherFlags & RangeFlag.UpperBelowValue) != 0)
                {

                }
                else if ((otherFlags & RangeFlag.UpperAboveValue) != 0)
                {

                }
                else if ((otherFlags & RangeFlag.UpperAboveAll) != 0)
                {
                    return -1;
                }
            }
            else if ((flags & RangeFlag.UpperAboveAll) != 0)
            {
                return (otherFlags & RangeFlag.UpperAboveAll) != 0 ? 0 : 1;
            }

            throw new InvalidOperationException();
        }
    }
}
