using System;
using FakerLibrary.Generator;

namespace BooleanGenerator
{
    public class MyBooleanGenerator : TypeGenerator<bool>
    {
        protected override bool Generation(Random random)
        {
            return random.Next(2) == 0;
        }
    }
}
