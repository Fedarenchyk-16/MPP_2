using System;
using FakerLibrary.Generator;

namespace ByteGenerator
{
    public class MyByteGenerator : TypeGenerator<byte>
    {
        protected override byte Generation(Random random)
        {
            return (byte)random.Next(byte.MaxValue + 1);
        }
    }
}
