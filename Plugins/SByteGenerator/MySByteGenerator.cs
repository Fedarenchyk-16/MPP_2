using System;
using FakerLibrary.Generator;

namespace SByteGenerator
{
    public class MySByteGenerator : TypeGenerator<sbyte>
    {
        protected override sbyte Generation(Random random)
        {
            return (sbyte)random.Next(sbyte.MinValue, sbyte.MaxValue + 1);
        }
    }
}
