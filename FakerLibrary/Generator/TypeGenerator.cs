using System;

namespace FakerLibrary.Generator
{
    public abstract class TypeGenerator<T> : IGenerator
    {
        protected abstract T Generation(Random random);

        public object GenerateValue(GeneratorContext context)
        {
            return Generation(context.RandomGenerator);
        }
    }
}
