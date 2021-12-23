using System;
using FakerLibrary.Generator;

namespace UIntGenerator
{
    public class MyUIntGenerator : TypeGenerator<uint>
    {
        protected override uint Generation(Random random)
        {
            byte[] buffer = new byte[4];
            random.NextBytes(buffer);

            return BitConverter.ToUInt32(buffer, 0);
        }
    }
}
