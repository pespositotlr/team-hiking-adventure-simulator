using System;
using System.Collections.Generic;
using System.Text;
using YamlDotNet.RepresentationModel;

namespace TeamHikingAdventureSimulator.Entities
{
    public class Trip
    {
        public Trip(List<Hiker> hikers, TimeSpan tripLength, double speed, int location)
        {
            this.Hikers = hikers;
            this.TripTime = tripLength;
            this.Speed = speed;
            this.Location = location;
        }
        public List<Hiker> Hikers { get; set; }
        public TimeSpan TripTime { get; set; }
        /// <summary>
        /// In feet per minute
        /// </summary>
        public double Speed { get; set; }
        public int Location { get; set; }
    }
}
