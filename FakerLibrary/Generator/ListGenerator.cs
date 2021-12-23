using System;
using System.Collections;

namespace FakerLibrary.Generator
{
    public class ListGenerator : IGenerator
    {
        public object GenerateValue(GeneratorContext generatorContext)
        {
            IList list = (IList)Activator.CreateInstance(generatorContext.TargetType);

            for (int i = 0; i <= generatorContext.RandomGenerator.Next(1, 10); i++)
            {
                list.Add(generatorContext.Faker.Create(generatorContext.TargetType.GetGenericArguments()[0]));
            }

            return list;
        }
    }
}
