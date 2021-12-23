using System;

namespace FakerLibrary.Faker
{
    public interface IFaker
    {
        T Create<T>();
        object Create(Type type);
    }
}
