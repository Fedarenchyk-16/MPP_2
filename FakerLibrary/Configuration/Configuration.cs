using System;

namespace FakerLibrary.Configuration
{
    public class Configuration
    {
        public string Name { get; }

        public Type FieldType { get; }
        public Type GeneratorType { get; }

        public Configuration(string name, Type type, Type generatorType)
        {
            Name = name;
            FieldType = type;
            GeneratorType = generatorType;
        }
    }
}
