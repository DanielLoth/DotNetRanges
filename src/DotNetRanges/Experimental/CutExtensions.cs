using System;

namespace DotNetRanges.Experimental
{
    internal static class CutExtensions
    {
        public static int EndpointAsLowerClosed(this ICut<int> lower)
        {
            if (lower == BelowAll<int>.INSTANCE) return int.MinValue;

            switch (lower.TypeAsLowerBound)
            {
                case BoundType.CLOSED: return lower.Endpoint;
                case BoundType.OPEN: return lower.Endpoint + 1;
                default: throw new InvalidOperationException();
            }
        }

        public static long EndpointAsLowerClosed(this ICut<long> lower)
        {
            if (lower == BelowAll<long>.INSTANCE) return long.MinValue;

            switch (lower.TypeAsLowerBound)
            {
                case BoundType.CLOSED: return lower.Endpoint;
                case BoundType.OPEN: return lower.Endpoint + 1;
                default: throw new InvalidOperationException();
            }
        }

        public static int EndpointAsUpperClosed(this ICut<int> upper)
        {
            if (upper == AboveAll<int>.INSTANCE) return int.MaxValue;

            switch (upper.TypeAsUpperBound)
            {
                case BoundType.CLOSED: return upper.Endpoint;
                case BoundType.OPEN: return upper.Endpoint - 1;
                default: throw new InvalidOperationException();
            }
        }

        public static long EndpointAsUpperClosed(this ICut<long> upper)
        {
            if (upper == AboveAll<long>.INSTANCE) return long.MaxValue;

            switch (upper.TypeAsUpperBound)
            {
                case BoundType.CLOSED: return upper.Endpoint;
                case BoundType.OPEN: return upper.Endpoint - 1;
                default: throw new InvalidOperationException();
            }
        }
    }
}
