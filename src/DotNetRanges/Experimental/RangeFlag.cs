using System;

namespace DotNetRanges.Experimental
{
    [Flags]
    enum RangeFlag : byte
    {
        LowerBelowValue = 1,
        LowerAboveValue = 2,
        LowerBelowAll = 4,

        UpperBelowValue = 8,
        UpperAboveValue = 16,
        UpperAboveAll = 32,

        HasLowerBound = LowerBelowValue | LowerAboveValue,
        HasUpperBound = UpperBelowValue | UpperAboveValue,

        AllLowerBits = LowerBelowValue | LowerAboveValue | LowerBelowAll,
        AllUpperBits = UpperBelowValue | UpperAboveValue | UpperAboveAll
    }
}
