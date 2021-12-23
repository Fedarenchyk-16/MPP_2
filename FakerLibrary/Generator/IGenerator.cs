namespace FakerLibrary.Generator
{
    public interface IGenerator
    {
        object GenerateValue(GeneratorContext context);
    }
}
