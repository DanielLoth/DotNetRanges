using DotNetRanges.Experimental;
using System;
using System.Reflection.Emit;
using Xunit;

namespace DotNetRanges.Tests.Experimental
{
    public class RangeTests
    {
#pragma warning disable 0169 // Suppress warning about non-use. This struct exists to facilitate struct size test.
        struct IntRangeStruct
        {
            int upper;
            int lower;
            byte flags;
        }
#pragma warning restore 0169

#pragma warning disable 0169 // Suppress warning about non-use. This struct exists to facilitate struct size test.
        struct LongRangeStruct
        {
            long upper;
            long lower;
            byte flags;
        }
#pragma warning restore 0169

        [Fact]
        public void CheckStructSize()
        {
            var range = Range<int>.Closed(5, 10);

            var intSz = GetSize(typeof(int));
            var longSize = GetSize(typeof(long));

            var a = GetSize(typeof(Range<byte>));
            var b = GetSize(typeof(Range<int>));
            var c = GetSize(typeof(Range<long>));

            var d = GetSize(typeof(IntRangeStruct));
            var e = GetSize(typeof(LongRangeStruct));
        }

        static int GetSize(Type t)
        {
            var dm = new DynamicMethod("SizeOfType", typeof(int), new Type[] { });
            ILGenerator il = dm.GetILGenerator();
            il.Emit(OpCodes.Sizeof, t);
            il.Emit(OpCodes.Ret);
            return (int)dm.Invoke(null, null);
        }
    }
}
