using System;
using FakerLibrary.Generator;

namespace CharGenerator
{
    public class MyCharGenerator : TypeGenerator<char>
    {
        protected override char Generation(Random random)
        {
            return (char)random.Next(char.MaxValue + 1);
        }
    }
}
