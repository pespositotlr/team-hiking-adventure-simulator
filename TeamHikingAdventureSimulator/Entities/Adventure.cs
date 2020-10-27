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
    public class Adventure : IAdventure
    {
        public List<Bridge> Bridges { get; set; }
        public List<Hiker> Hikers { get; set; }
        public TimeSpan TotalTripTime { get; set; }
        public int CurrentLocation { get; set; }
        public List<Bridge> BridgesCrossed { get; set; }
        public List<Trip> TripsMade { get; set; }
        public bool IsFinished { get; set; }

        private List<IAdventureObserver> _observers;

        public Adventure(AdventureData data)
        {
            Bridges = data.Bridges;
            Hikers = data.Hikers;
            TotalTripTime = new TimeSpan(0, 0, 0);
            CurrentLocation = 0;
            BridgesCrossed = new List<Bridge>();
            TripsMade = new List<Trip>();
            IsFinished = false;
            _observers = new List<IAdventureObserver>();
        }

        public void Attach(IAdventureObserver observer)
        {
            _observers.Add(observer);
        }

        public void Start()
        {
            CheckForBridgeExceptions();
            CheckForHikerExceptions();

            foreach (var observer in _observers)
                observer.NotifyStartingAdventure();

            TraverseBridges();
        }

        private void TraverseBridges()
        {
            //Traverse the bridges in numberical order (1 being the first one the hikers run into)
            foreach(Bridge bridge in Bridges.OrderBy(x => x.Location))
            {
                Traverse(bridge);
            }

            this.IsFinished = true;

            foreach (var observer in _observers)
                observer.NotifyAdventureFinished();
        }

        private void CheckForBridgeExceptions()
        {
            if (Bridges == null)
                throw new ArgumentNullException("No bridges found.");

            if (Bridges.GroupBy(x => x.Location).Any(g => g.Count() > 1))
                throw new ArgumentException("Multiple bridges in the same location.");
        }
        private void CheckForHikerExceptions()
        {
            if (Hikers == null)
                throw new ArgumentNullException("No hikers found.");

            if (!Hikers.Any(x => x.StartingLocation == 0))
                throw new ArgumentException("No hikers at the start location.");

            if (Hikers.Any(x => x.Speed <= 0))
                throw new ArgumentException("Hiker found with non-positive speed.");
        }

        private void Traverse(Bridge bridge)
        {
            CurrentLocation = bridge.Location;

            foreach (var observer in _observers)
                observer.NotifyLocationChanged();

            //Only hikers whose starting location is before the bridge have to cross it.
            List<Hiker> hikersTraversing = Hikers.Where(x => x.StartingLocation < bridge.Location).OrderBy(s => s.Speed).ToList();

            //Make groups of up to 2 hikers. Pair slowest hikers together to save time.
            List<List<Hiker>> hikerGroups = hikersTraversing.Select((x, idx) => new { x, idx })
                      .GroupBy(x => x.idx / 2)
                      .Select(g => g.Select(a => a.x).ToList()).ToList();

            foreach(List<Hiker> group in hikerGroups)
            {
                MakeTrip(group, bridge);
            }

            BridgesCrossed.Add(bridge);

            foreach (var observer in _observers)
                observer.NotifyBridgeCrossed();
        }

        private void MakeTrip(List<Hiker> group, Bridge bridgeToCross)
        {
            double tripSpeed = group.Min(t => t.Speed);
            double tripTimeMinutes = bridgeToCross.Length / tripSpeed;
            TimeSpan tripTime = TimeSpan.FromMinutes(tripTimeMinutes);

            //Keep track of trips made and who made them for logging purposes.
            TripsMade.Add(new Trip(group, tripTime, tripSpeed, bridgeToCross.Location));
            //Keep track of total time to save time having to re-calculate total time so far.
            TotalTripTime = TotalTripTime.Add(tripTime);

            foreach (var observer in _observers)
                observer.NotifyTripMade();
        }

    }
}
