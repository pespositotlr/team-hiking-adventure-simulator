using NUnit.Framework;
using System.Collections.Generic;
using TeamHikingAdventureSimulator.Builders;
using TeamHikingAdventureSimulator.Entities;

namespace TeamHikingAdventureSimulator.Test
{
    public class AdventureDataBuilderTests
    {
        string _inputFilePath;

        [SetUp]
        public void Setup()
        {
            _inputFilePath = "./Data/Input.yaml";
        }
        [Test]
        public void TestATestAdventureDataBuilder_TestBuild()
        {
            AdventureDataBuilder builder = new AdventureDataBuilder(_inputFilePath);
            builder.Build();
            AdventureData data = builder.GetAdventureData();

            Assert.IsNotNull(data);
        }
    }
}