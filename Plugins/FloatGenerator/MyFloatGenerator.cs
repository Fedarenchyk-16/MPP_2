using System;
using FakerLibrary.Generator;

namespace FloatGenerator
{
    public class MyFloatGenerator : TypeGenerator<float>
    {
        protected override float Generation(Random random)
        {
            byte[] buffer = new byte[4];
            random.NextBytes(buffer);

            return BitConverter.ToSingle(buffer, 0);
        }
    }
}
