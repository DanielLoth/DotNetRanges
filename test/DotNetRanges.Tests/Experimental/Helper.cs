using DotNetRanges.Experimental;
using System;

namespace DotNetRanges.Tests.Experimental
{
    public static class Helper
    {
        public static Range<T> Create<T>(T lower, T upper, BoundType type)
            where T : IComparable<T>
        {
            switch (type)
            {
                case BoundType.OPEN: return Range<T>.Open(lower, upper);
                case BoundType.CLOSED: return Range<T>.Closed(lower, upper);
                default: throw new InvalidOperationException("This method only supports OPEN and CLOSED ranges");
            }
        }
    }
}
