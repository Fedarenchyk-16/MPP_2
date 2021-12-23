using System;
using FakerLibrary.Generator;

namespace DecimalGenerator
{
    public class MyDecimalGenerator : TypeGenerator<decimal>
    {
        protected override decimal Generation(Random random)
        {
            return new decimal((int)(uint.MaxValue * random.NextDouble()),
                               (int)(uint.MaxValue * random.NextDouble()),
                               (int)(uint.MaxValue * random.NextDouble()),
                               random.NextDouble() >= 0.5, (byte)random.Next(29));
        }
    }
}
