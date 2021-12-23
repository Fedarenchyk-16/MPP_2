using System;
using FakerLibrary.Generator;

namespace ShortGenerator
{
    public class MyShortGenerator : TypeGenerator<short>
    {
        protected override short Generation(Random random)
        {
            return (short)random.Next(short.MinValue, short.MaxValue + 1);
        }
    }
}
