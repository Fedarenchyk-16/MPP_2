namespace FakerTest
{
    public class TestConfiguration
    {
        public int Int;
        public int IntConfig;

        public string String;
        public string StringConfig;        

        public short PropShort { get; }
        public short PropShortConfig { get; }

        public TestConfiguration(short PropShort, short PropShortConfig)
        {
            this.PropShort = PropShort;
            this.PropShortConfig = PropShortConfig;
        }
    }
}
