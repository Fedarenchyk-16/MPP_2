using System;
using FakerLibrary.Generator;

namespace ULongGenerator
{
    public class MyULongGenerator : TypeGenerator<ulong>
    {
        protected override ulong Generation(Random random)
        {
            byte[] buffer = new byte[8];
            random.NextBytes(buffer);

            return BitConverter.ToUInt64(buffer, 0);
        }
    }
}
