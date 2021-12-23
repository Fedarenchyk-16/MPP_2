namespace FakerTest
{
    public class TestConstructor
    {
        public int Int { get; }
        public bool Bool { get; }
        public string String { get; }

        public TestConstructor() { }
        public TestConstructor(bool isFirst)
        {
            Int = -777;
            Bool = true;
            String = "Done";
        }
    }
}
