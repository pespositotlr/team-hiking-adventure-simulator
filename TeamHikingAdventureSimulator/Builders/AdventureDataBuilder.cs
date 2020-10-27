using System.IO;
using TeamHikingAdventureSimulator.Entities;
using TeamHikingAdventureSimulator.Entities.Interfaces;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace TeamHikingAdventureSimulator.Builders
{
    class AdventureDataBuilder
    {

        private AdventureData _adventureData;
        private string _inputFilePath;

        public AdventureDataBuilder(string inputFilePath)
        {
            _inputFilePath = inputFilePath;
        }

        public void Build()
        {
            //Deserialize the yaml file that holds the hikers and bridges data
            var deserializer = new DeserializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .Build();

            _adventureData = deserializer.Deserialize<AdventureData>(File.OpenText(_inputFilePath));
        }

        public AdventureData GetAdventureData()
        {
            return _adventureData;
        }
        
    }
}
