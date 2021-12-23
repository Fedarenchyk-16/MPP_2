using System;
using FakerLibrary.Generator;

namespace IntGenerator
{
    public class MyIntGenerator : TypeGenerator<int>
    {
        protected override int Generation(Random random)
        {
            byte[] buffer = new byte[4];
            random.NextBytes(buffer);

            return BitConverter.ToInt32(buffer, 0);
        }
    }
}
