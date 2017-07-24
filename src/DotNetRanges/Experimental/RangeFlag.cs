using System;

namespace DotNetRanges.Experimental
{
    [Flags]
    enum RangeFlag : byte
    {
        LowerClosedBound = 1, /* LowerBelowValue in Guava */
        LowerOpenBound = 2, /* LowerAboveValue in Guava */
        LowerInfiniteBound = 4, /* LowerBelowAll in Guava */

        UpperOpenBound = 8, /* UpperBelowValue in Guava */
        UpperClosedBound = 16, /* UpperAboveValue in Guava */
        UpperInfiniteBound = 32, /* UpperAboveAll in Guava */

        HasLowerBound = LowerClosedBound | LowerOpenBound,
        HasUpperBound = UpperOpenBound | UpperClosedBound,

        AllLowerBits = LowerClosedBound | LowerOpenBound | LowerInfiniteBound,
        AllUpperBits = UpperOpenBound | UpperClosedBound | UpperInfiniteBound
    }
}
