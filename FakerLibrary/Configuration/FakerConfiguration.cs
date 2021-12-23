using System;
using System.Collections.Generic;
using FakerLibrary.Generator;

/*
           Настройка генерируемых случайных значений для конкретного поля путем передачи собственного
           генератора для конкретного поля/свойства:
            
              var config = new FakerConfig();
              config.Add<Foo, string, CityGenerator>(foo => foo.City);
               var faker = new Faker(config);
               Foo foo = faker.Create<Foo>();
                // при заполнении свойства City должен использоваться CityGenerator
*/

namespace FakerLibrary.Configuration
{
    public class FakerConfiguration
    {
        public List<Configuration> Configurations { get; }

        public FakerConfiguration()
        {
            Configurations = new List<Configuration>();
        }

        public void Add<T1, T2, T3>(System.Linq.Expressions.Expression<Func<T1, T2>> expression) where T3 : TypeGenerator<T2>
        {
            string name = ((System.Linq.Expressions.MemberExpression)expression.Body).Member.Name;
            Configuration configuration = new Configuration(name, typeof(T2), typeof(T3));
            Configurations.Add(configuration);
        }
    }
}
