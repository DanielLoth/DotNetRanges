using System;

namespace DotNetRanges.Experimental
{
    public struct Range<T> : IEquatable<Range<T>>
        where T : IComparable<T>, IEquatable<T>
    {
        #region IEquatable<T>

        public bool Equals(Range<T> other)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Object overrides

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }

        #endregion
    }
}
