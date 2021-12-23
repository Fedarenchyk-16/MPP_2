/*
2. Faker
Необходимо реализовать генератор объектов со случайными тестовыми данными:
var faker = new Faker();
Foo foo = faker.Create<Foo>();
Bar bar = faker.Create<Bar>();
int i = faker.Create<int>();

// Далее в примерах List носит иллюстративный характер. 
// Вместо него может быть выбрана любая коллекция из условия.

List<Foo> foos = faker.Create<List<Foo>>();
List<List<Foo>> lists = faker.Create<List<List<Foo>>>();
List<int> ints = faker.Create<List<int>>();
При создании объекта следует использовать конструктор, а также заполнять публичные поля и свойства с публичными сеттерами,
 которые не были заполнены в конструкторе. Следует учитывать сценарии, когда у класса только приватный конструктор, несколько
  конструкторов, конструктор с параметрами и публичные поля/свойства. 
При наличии нескольких конструкторов следует отдавать предпочтение конструктору с большим числом параметров, однако если
 при попытке его использования возникло исключение, следует пытаться использовать остальные. 
 */
using System;
using System.Collections.Generic;
using System.Reflection;
using FakerLibrary.Faker;
using FakerLibrary.Configuration;
using FakerTest;

namespace Faker
{
    class Program
    {
        #region Dependency
        class A
        {
            public int Int;
            public B B;
        }

        class B
        {
            public C C;
            public double Double;
        }

        class C
        {
            public A A;
        }
        #endregion

        static void Main(string[] args)
        {
            FakerInstance faker = new FakerInstance(null);

            // All Type
            PrintObjectValue(faker.Create<TestAllType>(), " ");
            Console.WriteLine("============================================" +
                              "============================================");

            // Constructor
            PrintObjectValue(faker.Create<TestConstructor>(), " ");
            Console.WriteLine("============================================" +
                              "============================================");

            // Circular Dependency
            PrintObjectValue(faker.Create<A>(), " ");
            Console.WriteLine("============================================" +
                              "============================================");

            // Class in Field
            PrintObjectValue(faker.Create<TestField>(), " ");
            Console.WriteLine("============================================" +
                              "============================================");

            // One Level List
            List<TestClass> oneLevelList = faker.Create<List<TestClass>>();
            foreach (TestClass testClass in oneLevelList) { PrintObjectValue(testClass, " "); }
            Console.WriteLine("============================================" +
                              "============================================");

            // Two Level List
            List<List<TestClass>> twoLevelList = faker.Create<List<List<TestClass>>>();
            foreach (List<TestClass> listTestClass in twoLevelList) 
            { 
                foreach (TestClass testClass in listTestClass) 
                { 
                    PrintObjectValue(testClass, " "); 
                }
                Console.WriteLine();
            }
            Console.WriteLine("============================================" +
                              "============================================");

            // Configuration
            
            /*
             Настройка генерируемых случайных значений для конкретного поля путем передачи собственного
             генератора для конкретного поля/свойства:
              
                var config = new FakerConfig();
                config.Add<Foo, string, CityGenerator>(foo => foo.City);
                 var faker = new Faker(config);
                 Foo foo = faker.Create<Foo>();
                  // при заполнении свойства City должен использоваться CityGenerator
             */
            FakerConfiguration configuration = new FakerConfiguration();
            configuration.Add<TestConfiguration, string, NewStringGenerator>(TestConfig => TestConfig.StringConfig);
            configuration.Add<TestConfiguration, int, NewIntGenerator>(TestConfig => TestConfig.IntConfig);
            configuration.Add<TestConfiguration, short, NewShortGenerator>(TestConfig => TestConfig.PropShortConfig);

            faker = new FakerInstance(configuration);
            PrintObjectValue(faker.Create<TestConfiguration>(), " "); 

            Console.ReadLine();
        }

        private static void PrintObjectValue(object obj, string offset)
        {
            if (obj != null)
            {
                Type classType = obj.GetType();
                Console.WriteLine(offset + classType.Name);
                FieldInfo[] fieldInfo = classType.GetFields();
                PropertyInfo[] propertyInfo = classType.GetProperties();

                foreach (var field in fieldInfo)
                {
                    Type fieldType = Type.GetType(field.FieldType.ToString());
                    if (fieldType != null && fieldType.IsClass && fieldType.Name != "String")
                    {
                        offset += " ";
                        PrintObjectValue(field.GetValue(obj), offset);
                        offset = offset.Remove(offset.Length - 1, 1);
                    }
                    else
                    {
                        Console.WriteLine(offset + "Name: " + field.Name + " Field Type: " + field.FieldType +" Value: " + field.GetValue(obj));
                    }
                }

                foreach (var property in propertyInfo)
                {
                    Type propertyType = Type.GetType(property.PropertyType.ToString());
                    if (propertyType != null && propertyType.IsClass && propertyType.Name != "String")
                    {
                        offset += " ";
                        PrintObjectValue(property.GetValue(obj), offset);
                        offset = offset.Remove(offset.Length - 1, 1);
                    }
                    else
                    {
                        Console.WriteLine(offset + "Name: " + property.Name + " Field Type: " + property.PropertyType +" Value: " + property.GetValue(obj));
                    }
                }
            }
        }
    }
}
