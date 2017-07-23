using DotNetRanges.Experimental;
using System;
using System.Reflection.Emit;
using Xunit;

namespace DotNetRanges.Tests.Experimental
{
    public class RangeTests
    {
        struct IntRangeStruct
        {
            int upper;
            int lower;
            byte flags;
        }

        struct LongRangeStruct
        {
            long upper;
            long lower;
            byte flags;
        }

        [Fact]
        public void FirstUnitTest()
        {
            var range = new Range<int>();

            Assert.NotNull(range);
            Assert.Equal(1, 1);
        }

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
