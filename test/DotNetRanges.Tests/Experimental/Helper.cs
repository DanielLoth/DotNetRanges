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

        public static Range<int> CreateOther(Range<int> range, BoundType type, Operation operation)
        {
            if (range.LowerBoundType != BoundType.CLOSED && range.LowerBoundType != BoundType.OPEN)
            {
                throw new InvalidOperationException("Unbounded lower endpoint not supported");
            }

            if (range.UpperBoundType != BoundType.CLOSED && range.UpperBoundType != BoundType.OPEN)
            {
                throw new InvalidOperationException("Unbounded upper endpoint not supported");
            }

            var lowerClosed = range.LowerBoundType == BoundType.CLOSED ? range.LowerEndpoint : range.LowerEndpoint + 1;
            var upperClosed = range.UpperBoundType == BoundType.CLOSED ? range.UpperEndpoint : range.UpperEndpoint - 1;

            switch (type)
            {
                case BoundType.OPEN:
                    switch (operation)
                    {
                        case Operation.Precedes: return Create(lowerClosed - 20, lowerClosed - 10, type);
                        case Operation.Meets: return Create(lowerClosed - 20, lowerClosed, type);
                        case Operation.Overlaps: return Create(lowerClosed - 20, lowerClosed + 1, type);
                        case Operation.FinishedBy: return Create(lowerClosed - 20, upperClosed, type);
                        case Operation.Contains: return Create(lowerClosed - 10, upperClosed + 10, type);
                        case Operation.Starts: return Create(lowerClosed, upperClosed - 1, type);
                        case Operation.Equals: return Create(lowerClosed, upperClosed, type);
                        case Operation.StartedBy: return Create(lowerClosed, upperClosed + 1, type);
                        case Operation.During: return Create(lowerClosed + 1, upperClosed - 1, type);
                        case Operation.Finishes: return Create(lowerClosed + 1, upperClosed, type);
                        case Operation.OverlappedBy: return Create(lowerClosed + 1, upperClosed + 1, type);
                        case Operation.MetBy: return Create(upperClosed, upperClosed + 10, type);
                        case Operation.PrecededBy: return Create(upperClosed + 10, upperClosed + 20, type);
                        default: throw new InvalidOperationException();
                    }
                case BoundType.CLOSED:
                    switch (operation)
                    {
                        case Operation.Precedes: return Create(lowerClosed - 20, lowerClosed - 10, type);
                        case Operation.Meets: return Create(lowerClosed - 20, lowerClosed, type);
                        case Operation.Overlaps: return Create(lowerClosed - 20, lowerClosed + 1, type);
                        case Operation.FinishedBy: return Create(lowerClosed - 20, upperClosed, type);
                        case Operation.Contains: return Create(lowerClosed - 10, upperClosed + 10, type);
                        case Operation.Starts: return Create(lowerClosed, upperClosed - 1, type);
                        case Operation.Equals: return Create(lowerClosed, upperClosed, type);
                        case Operation.StartedBy: return Create(lowerClosed, upperClosed + 1, type);
                        case Operation.During: return Create(lowerClosed + 1, upperClosed - 1, type);
                        case Operation.Finishes: return Create(lowerClosed + 1, upperClosed, type);
                        case Operation.OverlappedBy: return Create(lowerClosed + 1, upperClosed + 1, type);
                        case Operation.MetBy: return Create(upperClosed, upperClosed + 10, type);
                        case Operation.PrecededBy: return Create(upperClosed + 10, upperClosed + 20, type);
                        default: throw new InvalidOperationException();
                    }
                default: throw new InvalidOperationException("This method only supports OPEN and CLOSED ranges");
            }
        }
    }

    public enum Operation
    {
        Precedes,
        Meets,
        Overlaps,
        FinishedBy,
        Contains,
        Starts,
        Equals,
        StartedBy,
        During,
        Finishes,
        OverlappedBy,
        MetBy,
        PrecededBy
    }
}
