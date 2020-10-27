using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeamHikingAdventureSimulator.Entities.Interfaces;
using TeamHikingAdventureSimulator.Observers;
using TeamHikingAdventureSimulator.Observers.Interfaces;

namespace TeamHikingAdventureSimulator.Entities
{
    /// <summary>
    /// For storing deserialized data from yaml files
    /// </summary>
    class AdventureData
    {
        public List<Bridge> Bridges { get; set; }
        public List<Hiker> Hikers { get; set; }

    }
}
