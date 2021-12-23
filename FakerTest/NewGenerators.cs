using System;
using FakerLibrary.Generator;

namespace FakerTest
{
    public class NewIntGenerator : TypeGenerator<int>
    {
        protected override int Generation(Random random)
        {
            return 21;
        }
    }

    public class NewShortGenerator : TypeGenerator<short>
    {
        protected override short Generation(Random random)
        {
            return 2021;
        }
    }

    public class NewStringGenerator : TypeGenerator<string>
    {
        private readonly string[] names = { "Daffy Duck", "Porky Pig", "Bugs Bunny", "Pepé Le Pew", "Tweety", "Sylvester" };

        protected override string Generation(Random random)
        {
            return names[random.Next(names.Length)];
        }
    }
}
