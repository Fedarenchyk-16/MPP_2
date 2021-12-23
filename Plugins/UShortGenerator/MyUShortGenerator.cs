using System;
using FakerLibrary.Generator;

namespace UShortGenerator
{
    public class MyUShortGenerator : TypeGenerator<ushort>
    {
        protected override ushort Generation(Random random)
        {
            return (ushort)random.Next(ushort.MaxValue + 1);
        }
    }
}
