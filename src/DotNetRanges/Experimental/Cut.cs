using System;
using System.Text;

namespace DotNetRanges.Experimental
{
    internal interface ICut<T> : IComparable<ICut<T>>, IEquatable<ICut<T>>
        where T : IComparable<T>
    {
        T Endpoint { get; }
        bool HasEndpoint { get; }
        BoundType TypeAsLowerBound { get; }
        BoundType TypeAsUpperBound { get; }

        void DescribeAsLowerBound(StringBuilder builder);
        void DescribeAsUpperBound(StringBuilder builder);
    }

    internal static class Cut<T> where T : IComparable<T>
    {
        public static ICut<T> BelowValue(T endpoint)
        {
            return new BelowValue<T>(endpoint);
        }

        public static ICut<T> AboveValue(T endpoint)
        {
            return new AboveValue<T>(endpoint);
        }

        public static ICut<T> BelowAll()
        {
            return BelowAll<T>.INSTANCE;
        }

        public static ICut<T> AboveAll()
        {
            return AboveAll<T>.INSTANCE;
        }

        public static ICut<T> LowerOpenBound(T endpoint)
        {
            return AboveValue(endpoint);
        }

        public static ICut<T> LowerClosedBound(T endpoint)
        {
            return BelowValue(endpoint);
        }

        public static ICut<T> UpperOpenBound(T endpoint)
        {
            return BelowValue(endpoint);
        }

        public static ICut<T> UpperClosedBound(T endpoint)
        {
            return AboveValue(endpoint);
        }
    }

    internal abstract class AbstractCut<T> : ICut<T>
        where T : IComparable<T>
    {
        public abstract T Endpoint { get; }
        public abstract bool HasEndpoint { get; }
        public abstract BoundType TypeAsLowerBound { get; }
        public abstract BoundType TypeAsUpperBound { get; }

        public abstract void DescribeAsLowerBound(StringBuilder builder);
        public abstract void DescribeAsUpperBound(StringBuilder builder);

        public virtual int CompareTo(ICut<T> other)
        {
            if (other == BelowAll<T>.INSTANCE) return 1;
            if (other == AboveAll<T>.INSTANCE) return -1;

            var result = Endpoint.CompareTo(other.Endpoint);
            if (result != 0)
            {
                return result;
            }

            var thisIsAboveValue = this as AboveValue<T> != null;
            var otherIsAboveValue = other as AboveValue<T> != null;

            return thisIsAboveValue.CompareTo(otherIsAboveValue);
        }

        public bool Equals(ICut<T> other)
        {
            return CompareTo(other) == 0;
        }
    }

    internal sealed class AboveAll<T> : AbstractCut<T>
        where T : IComparable<T>
    {
        internal static readonly AboveAll<T> INSTANCE = new AboveAll<T>();

        public override BoundType TypeAsLowerBound => throw new InvalidOperationException();
        public override BoundType TypeAsUpperBound => throw new InvalidOperationException();
        public override T Endpoint => throw new InvalidOperationException();
        public override bool HasEndpoint => false;

        public override int CompareTo(ICut<T> other)
        {
            return other == this ? 0 : 1;
        }

        public override void DescribeAsLowerBound(StringBuilder builder)
        {
            throw new InvalidOperationException();
        }

        public override void DescribeAsUpperBound(StringBuilder builder)
        {
            builder.Append("+∞)");
        }
    }

    internal sealed class AboveValue<T> : AbstractCut<T>
        where T : IComparable<T>
    {
        private readonly T _endpoint;

        public override BoundType TypeAsLowerBound => BoundType.OPEN;
        public override BoundType TypeAsUpperBound => BoundType.CLOSED;
        public override T Endpoint => _endpoint;
        public override bool HasEndpoint => true;

        public AboveValue(T endpoint)
        {
            _endpoint = endpoint;
        }

        public override void DescribeAsLowerBound(StringBuilder builder)
        {
            builder.Append('(').Append(_endpoint);
        }

        public override void DescribeAsUpperBound(StringBuilder builder)
        {
            builder.Append(_endpoint).Append(']');
        }
    }

    internal sealed class BelowValue<T> : AbstractCut<T>
        where T : IComparable<T>
    {
        private readonly T _endpoint;

        public override BoundType TypeAsLowerBound => BoundType.CLOSED;
        public override BoundType TypeAsUpperBound => BoundType.OPEN;
        public override T Endpoint => _endpoint;
        public override bool HasEndpoint => true;

        public BelowValue(T endpoint)
        {
            _endpoint = endpoint;
        }

        public override void DescribeAsLowerBound(StringBuilder builder)
        {
            builder.Append('[').Append(_endpoint);
        }

        public override void DescribeAsUpperBound(StringBuilder builder)
        {
            builder.Append(_endpoint).Append(")");
        }
    }

    internal sealed class BelowAll<T> : AbstractCut<T>
        where T : IComparable<T>
    {
        internal static readonly BelowAll<T> INSTANCE = new BelowAll<T>();

        public override BoundType TypeAsLowerBound => throw new InvalidOperationException();
        public override BoundType TypeAsUpperBound => throw new InvalidOperationException();
        public override T Endpoint => throw new InvalidOperationException();
        public override bool HasEndpoint => false;

        public override int CompareTo(ICut<T> other)
        {
            return other == this ? 0 : -1;
        }

        public override void DescribeAsLowerBound(StringBuilder builder)
        {
            builder.Append("(-∞");
        }

        public override void DescribeAsUpperBound(StringBuilder builder)
        {
            throw new InvalidOperationException();
        }
    }
}
