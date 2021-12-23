using System;
using FakerLibrary.Generator;

namespace DoubleGenerator
{
    public class MyDoubleGenerator : TypeGenerator<double>
    {
        protected override double Generation(Random random)
        {
            byte[] buffer = new byte[8];
            random.NextBytes(buffer);

            return BitConverter.ToDouble(buffer, 0);
        }
    }
}
