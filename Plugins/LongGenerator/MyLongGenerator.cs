using System;
using FakerLibrary.Generator;

namespace LongGenerator
{
    public class MyLongGenerator : TypeGenerator<long>
    {
        protected override long Generation(Random random)
        {
            byte[] buffer = new byte[8];
            random.NextBytes(buffer);

            return BitConverter.ToInt64(buffer, 0);
        }
    }
}
