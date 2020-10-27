using System;
using System.Collections.Generic;
using System.Text;
using YamlDotNet.RepresentationModel;

namespace TeamHikingAdventureSimulator.Entities
{
    class Hiker
    {
        public string Name { get; set; }
        public int StartingLocation { get; set; }

        /// <summary>
        /// Feet per Minute
        /// </summary>
        public double Speed { get; set; }
    }
}
