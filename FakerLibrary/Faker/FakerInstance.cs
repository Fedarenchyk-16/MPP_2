using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FakerLibrary.Faker
{
    public class FakerInstance : IFaker
    {
        private readonly Dictionary<Type, Generator.IGenerator> _typeGenerators;
        private readonly Stack<Type> _circleDependency;
        private readonly Configuration.FakerConfiguration _fakerConfiguration;

        public FakerInstance(Configuration.FakerConfiguration fakerConfiguration)
        {
            _typeGenerators = new Dictionary<Type, Generator.IGenerator>();
            _circleDependency = new Stack<Type>();

            _typeGenerators.Add(typeof(List<>), new Generator.ListGenerator());

            LoadPlugins();
            _fakerConfiguration = fakerConfiguration;
        }

        public T Create<T>()
        {
            return (T)Create(typeof(T));
        }

        public object Create(Type type)
        {
            if (_circleDependency.Count(CircleType => CircleType == type) >= 5)
            {
                Console.WriteLine("Circular Dependency");
                return GetDefaultValue(type);
            }

            _circleDependency.Push(type);
            FakerInstance faker = new FakerInstance(_fakerConfiguration);

            int seed = (int)DateTime.Now.Ticks & 0x0000FFFF; 
            Generator.GeneratorContext context = new Generator.GeneratorContext(new Random(seed), type, faker);

            Generator.IGenerator generator = FindGenerator(type);
            if (generator != null)
            {
                _circleDependency.Pop();
                return generator.GenerateValue(context);
            }

            object obj = CreateObject(type);
            obj = FillObject(obj);
            _circleDependency.Pop();

            return obj;
        }

        private object FillObject(object obj)
        {
            if (obj != null)
            {
                FieldInfo[] fields = obj.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public);
                Console.WriteLine(fields.Length);
                PropertyInfo[] properties = obj.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
                foreach (FieldInfo field in fields)
                {
                    if (IsNotValueSet(field, obj))
                    {
                        Configuration.Configuration configuration = null;
                        if (_fakerConfiguration != null)
                        {
                            foreach (Configuration.Configuration config in _fakerConfiguration.Configurations)
                            {
                                if (config.Name == field.Name && config.FieldType == field.FieldType)
                                {
                                    configuration = config;
                                }
                            }
                        }

                        if (configuration == null)
                        {
                            field.SetValue(obj, Create(field.FieldType));
                        }
                        else
                        {
                            FakerInstance faker = new FakerInstance(_fakerConfiguration);
                            int seed = (int)DateTime.Now.Ticks & 0x0000FFFF;

                            Generator.GeneratorContext context = new Generator.GeneratorContext(new Random(seed), field.FieldType, faker);
                            field.SetValue(obj, ((Generator.IGenerator)Activator.CreateInstance(configuration.GeneratorType)).GenerateValue(context));
                        }
                    }
                }

                foreach (PropertyInfo property in properties)
                {
                    if (property.CanWrite && IsNotValueSet(property, obj))
                    {
                        Configuration.Configuration configuration = null;
                        if (_fakerConfiguration != null)
                        {
                            foreach (Configuration.Configuration config in _fakerConfiguration.Configurations)
                            {
                                if (config.Name == property.Name && config.FieldType == property.PropertyType)
                                {
                                    configuration = config;
                                }
                            }
                        }

                        if (configuration == null)
                        {
                            property.SetValue(obj, Create(property.PropertyType));
                        }
                        else
                        {
                            FakerInstance faker = new FakerInstance(_fakerConfiguration);
                            int seed = (int)DateTime.Now.Ticks & 0x0000FFFF;

                            Generator.GeneratorContext context = new Generator.GeneratorContext(new Random(seed), property.PropertyType, faker);
                            property.SetValue(obj, ((Generator.IGenerator)Activator.CreateInstance(configuration.GeneratorType)).GenerateValue(context));
                        }
                    }
                }
            }

            return obj;
        }

        private object CreateObject(Type type)
        {
            ConstructorInfo[] bufferConstructors = type.GetConstructors(BindingFlags.Instance | BindingFlags.Public);
            IEnumerable<ConstructorInfo> constructors = bufferConstructors.OrderByDescending(Constructor => Constructor.GetParameters().Length);

            object obj = null;
            foreach (ConstructorInfo constructor in constructors)
            {
                ParameterInfo[] parametersInfo = constructor.GetParameters(); 
                
                object[] parameters = new object[parametersInfo.Length]; 

                for (int i = 0; i < parameters.Length; i++)
                {
                    Configuration.Configuration configuration = null;
                    if (_fakerConfiguration != null)
                    {
                        foreach (Configuration.Configuration config in _fakerConfiguration.Configurations)
                        {
                            //Console.WriteLine(config.FieldType);
                            if (config.Name == parametersInfo[i].Name && config.FieldType == parametersInfo[i].ParameterType)
                            {
                                configuration = config;
                            }
                        }
                    }

                    if (configuration == null)
                    {
                        parameters[i] = Create(parametersInfo[i].ParameterType); //create param
                    }
                    else
                    {
                        FakerInstance faker = new FakerInstance(_fakerConfiguration);
                        int seed = (int)DateTime.Now.Ticks & 0x0000FFFF;

                        Generator.GeneratorContext context = new Generator.GeneratorContext(new Random(seed), type, faker);
                        parameters[i] = ((Generator.IGenerator)Activator.CreateInstance(configuration.GeneratorType)).GenerateValue(context);
                    }
                }

                try
                {
                    obj = constructor.Invoke(parameters);//invoke - try to call construct
                    break;
                }
                catch
                {
                }
            }

            if (obj == null && type.IsValueType)
            {
                obj = Activator.CreateInstance(type);
            }

            return obj;
        }


        private object GetDefaultValue(Type type)
        {
            return type.IsValueType ? Activator.CreateInstance(type) : null;
        }

        private Generator.IGenerator FindGenerator(Type type)
        {
            if (type.IsGenericType)
            {
                type = type.GetGenericTypeDefinition();
            }

            return _typeGenerators.ContainsKey(type) ? _typeGenerators[type] : null;
        }

        private bool IsNotValueSet(MemberInfo member, object obj)
        {
            if (member is FieldInfo field)
            {
                if (GetDefaultValue(field.FieldType) == null)
                {
                    return true;
                }
                if (field.GetValue(obj).Equals(GetDefaultValue(field.FieldType)))
                {
                    return true;
                }
            }

            if (member is PropertyInfo property)
            {
                if (GetDefaultValue(property.PropertyType) == null)
                {
                    return true;
                }
                if (property.GetValue(obj).Equals(GetDefaultValue(property.PropertyType)))
                {
                    return true;
                }
            }

            return false;
        }

        private void LoadPlugins()
        {
            Plugin plugin = new Plugin();
            string[] typeNames = new string[15] {
                "Boolean", "Byte",   "Char",  "DateTime",
                "Decimal", "Double", "Float", "Int",
                "Long",    "SByte",  "Short", "String",
                "UInt",    "ULong",  "UShort"
            };

            foreach (string typeName in typeNames)
            {
                Tuple<Type, Generator.IGenerator> generator = plugin.LoadPlugin(typeName);

                if (generator != null)
                {
                    _typeGenerators.Add(generator.Item1, generator.Item2);
                }
            }
        }
    }
}
