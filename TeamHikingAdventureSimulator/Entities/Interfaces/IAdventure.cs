using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeamHikingAdventureSimulator.Observers.Interfaces;

namespace TeamHikingAdventureSimulator.Entities.Interfaces
{
    interface IAdventure
    {
        void Attach(IAdventureObserver observer);

        void Start();
    }
}
