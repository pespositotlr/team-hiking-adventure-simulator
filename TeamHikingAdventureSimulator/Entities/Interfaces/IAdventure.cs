using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeamHikingAdventureSimulator.Observers.Interfaces;

namespace TeamHikingAdventureSimulator.Entities.Interfaces
{
    public interface IAdventure
    {
        public void Attach(IAdventureObserver observer);

        public void Start();
    }
}
