using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using FakerLibrary.Faker;
using FakerLibrary.Configuration;

namespace FakerTest
{
    [TestClass]
    public class FakerTest
    {
        private static FakerInstance _faker;

        [TestInitialize]
        public void Init()
        {
            FakerConfiguration configuration = new FakerConfiguration();
            configuration.Add<TestConfiguration, string, NewStringGenerator>(TestConfig => TestConfig.StringConfig);
            configuration.Add<TestConfiguration, int, NewIntGenerator>(TestConfig => TestConfig.IntConfig);
            configuration.Add<TestConfiguration, short, NewShortGenerator>(TestConfig => TestConfig.PropShortConfig);
            _faker = new FakerInstance(configuration);
        }

        [TestMethod]
        public void ConfigurationTest()
        {
            string[] names = { "Daffy Duck", "Porky Pig", "Bugs Bunny", "Pepé Le Pew", "Tweety", "Sylvester" };

            TestConfiguration testConfig = _faker.Create<TestConfiguration>();
            Assert.IsNotNull(testConfig);
            Assert.AreEqual(testConfig.IntConfig, 21);
            Assert.AreEqual(testConfig.PropShortConfig, 2021);
            CollectionAssert.Contains(names, testConfig.StringConfig);
        }

        [TestMethod]
        public void ConstructorTest()
        {
            TestConstructor testConstructor = _faker.Create<TestConstructor>();
            Assert.IsNotNull(testConstructor);
            Assert.AreEqual(testConstructor.Int, -777);            
            Assert.AreEqual(testConstructor.Bool, true);
            Assert.AreEqual(testConstructor.String, "Done");
        }

        [TestMethod]
        public void ListTest()
        {
            List<TestAllType> testConstructor = _faker.Create<List<TestAllType>>();
            Assert.IsNotNull(testConstructor);
            CollectionAssert.AllItemsAreNotNull(testConstructor);
        }

        [TestMethod]
        public void DoubleListTest()
        {
            List<List<TestAllType>> testConstructor = _faker.Create<List<List<TestAllType>>>();
            Assert.IsNotNull(testConstructor);
            CollectionAssert.AllItemsAreNotNull(testConstructor);
        }
    }
}
