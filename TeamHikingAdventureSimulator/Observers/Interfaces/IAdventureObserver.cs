using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TeamHikingAdventureSimulator.Observers.Interfaces
{
    interface IAdventureObserver
    {
        public void NotifyStartingAdventure();

        public void NotifyTripMade();

        public void NotifyBridgeCrossed();

        public void NotifyLocationChanged();

        public void NotifyAdventureFinished();

    }
}
