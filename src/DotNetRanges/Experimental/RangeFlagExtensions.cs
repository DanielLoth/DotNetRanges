using System;
using System.Diagnostics;
using System.Text;

namespace DotNetRanges.Experimental
{
    static class RangeFlagExtensions
    {
#if DEBUG
        public static void AssertMutualExclusion(this RangeFlag flags)
        {
            var lowerBitmask = flags & RangeFlag.AllLowerBits;
            Debug.Assert(IsPowerOfTwo(lowerBitmask));

            var upperBitmask = flags & RangeFlag.AllUpperBits;
            Debug.Assert(IsPowerOfTwo(upperBitmask));
        }

        public static void AssertHasLowerBound(this RangeFlag flags)
        {
            var lowerBitmask = flags & RangeFlag.AllLowerBits;
            Debug.Assert((flags & lowerBitmask) != 0);
        }

        public static void AssertHasUpperBound(this RangeFlag flags)
        {
            var upperBitmask = flags & RangeFlag.AllUpperBits;
            Debug.Assert((flags & upperBitmask) != 0);
        }

        private static bool IsPowerOfTwo(RangeFlag flags)
        {
            return flags > 0 && (flags & (flags - 1)) == 0;
        }
#endif

        public static string RangeToString<T>(this RangeFlag flags, Range<T> range)
            where T : IComparable<T>
        {
            var builder = new StringBuilder(30);
            var hasLowerBound = (flags & RangeFlag.HasLowerBound) != 0;
            var hasUpperBound = (flags & RangeFlag.HasUpperBound) != 0;

            if (hasLowerBound)
            {
                var closedLower = (flags & RangeFlag.LowerClosedBound) != 0;

                builder.Append(closedLower ? "[" : "(");
                builder.Append(range.LowerBound);
            }
            else
            {
                builder.Append("(-∞");
            }

            builder.Append("..");

            if (hasUpperBound)
            {
                var closedUpper = (flags & RangeFlag.UpperClosedBound) != 0;

                builder.Append(range.UpperBound);
                builder.Append(closedUpper ? "]" : ")");
            }
            else
            {
                builder.Append("+∞)");
            }

            return builder.ToString();
        }
    }
}
