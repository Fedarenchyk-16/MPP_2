using System;
using System.Text;
using FakerLibrary.Generator;

namespace StringGenerator
{
    public class MyStringGenerator : TypeGenerator<string>
    {
        protected override string Generation(Random random)
        {
            char[] chars = new char[random.Next(32) + 1];
            for (var i = 0; i < chars.Length; i++)
            {
                chars[i] = (char)random.Next(255);
            }

            return new string(chars);
        }
    }
}
