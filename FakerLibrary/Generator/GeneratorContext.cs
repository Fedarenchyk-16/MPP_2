using System;

namespace FakerLibrary.Generator
{
    public class GeneratorContext
    {
        public Random RandomGenerator;
        public Type TargetType;
        public Faker.IFaker Faker;

        public GeneratorContext(Random random, Type type, Faker.IFaker faker)
        {
            RandomGenerator = random;
            TargetType = type;
            Faker = faker;
        }
    }
}
