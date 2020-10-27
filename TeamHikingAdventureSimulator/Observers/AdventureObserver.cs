using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeamHikingAdventureSimulator.Entities;
using TeamHikingAdventureSimulator.Observers.Interfaces;

namespace TeamHikingAdventureSimulator.Observers
{
    class AdventureObserver : IAdventureObserver
    {
        public Adventure _subject;
        public TimeSpan _loggedTime;
        public AdventureObserver(Adventure subject)
        {
            _subject = subject;
            _loggedTime = new TimeSpan(0, 0, 0);
        }

        public void NotifyStartingAdventure()
        {
            LogStartingAdventure();
            LogCurrentLocation();
        }
        public void NotifyTripMade()
        {
            LogNewestTrip();
            LogTotalTimeSoFar();
        }

        public void NotifyBridgeCrossed()
        {
            LogNewestBridgeCrossed();
            LogTotalTimeSoFar();
        }

        public void NotifyLocationChanged()
        {
            LogCurrentLocation();
        }

        public void NotifyAdventureFinished()
        {
            LogAdventureFinished();
        }

        private void LogStartingAdventure()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Starting a new adventure!");
            Console.WriteLine(sb.ToString());
        }

        private void LogNewestTrip()
        {
            if (!_subject.TripsMade.Any())
                throw new InvalidOperationException("Tried to log a trip when none have been made.");

            var tripToLog = _subject.TripsMade[_subject.TripsMade.Count - 1];
            StringBuilder sb = new StringBuilder();
            sb.Append("Trip made across by bridge at location ");
            sb.Append(tripToLog.Location);
            sb.Append(" by ");

            List<Hiker> alphOrderHikers = tripToLog.Hikers.OrderBy(x => x.Name).ToList();

            if (alphOrderHikers.Count > 1)
            {
                sb.Append(alphOrderHikers[0].Name);
                sb.Append(" and ");
                sb.Append(alphOrderHikers[1].Name);
                sb.Append(" ");

            }
            else
            {
                sb.Append(alphOrderHikers.FirstOrDefault().Name);
                sb.Append(" alone ");
            }
            sb.Append("in ");
            sb.Append(GetFormattedTimespan(tripToLog.TripTime));
            sb.Append(" at a speed of ");
            sb.Append(tripToLog.Speed);
            sb.Append(" ft/min.");

            Console.WriteLine(sb.ToString());
        }
        private void LogCurrentLocation()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Current location: Location ");
            sb.Append(_subject.CurrentLocation);
            sb.Append(".");

            Console.WriteLine(sb.ToString());
        }

        private void LogTotalTimeSoFar()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Total adventure time so far: ");
            sb.Append(GetFormattedTimespan(_subject.TotalTripTime));
            sb.Append(".");

            Console.WriteLine(sb.ToString());
        }

        private void LogNewestBridgeCrossed()
        {
            if (!_subject.BridgesCrossed.Any())
                throw new InvalidOperationException("Tried to log a bridge crossed when none have been crossed.");

            var bridgeCrossToLog = _subject.BridgesCrossed[_subject.BridgesCrossed.Count - 1];
            StringBuilder sb = new StringBuilder();
            sb.Append("Bridge at location ");
            sb.Append(bridgeCrossToLog.Location);
            sb.Append(" fully crossed by all hikers!");

            Console.WriteLine(sb.ToString());
        }

        private void LogAdventureFinished()
        {
            if (!_subject.BridgesCrossed.Any() || !_subject.TripsMade.Any())
                throw new InvalidOperationException("Tried to log adventure finished when you haven't gone anywhere.");

            StringBuilder sb = new StringBuilder();
            sb.Append("Adventure finished! ");
            sb.Append("Total adventure time: ");
            sb.Append(GetFormattedTimespan(_subject.TotalTripTime));
            sb.Append(".");

            Console.WriteLine(sb.ToString());
        }

        private string GetFormattedTimespan(TimeSpan timeSpan)
        {
            StringBuilder sb = new StringBuilder();
            
            if (timeSpan.TotalHours > 24)
            {
                sb.Append(timeSpan.Days);
                sb.Append(" days, ");
            }

            if (timeSpan.TotalMinutes > 60)
            {
                sb.Append(timeSpan.Hours);
                sb.Append(" hours, ");
            }

            if (timeSpan.TotalSeconds > 60)
            {
                sb.Append(timeSpan.Minutes);
                sb.Append(" minutes, ");
            }

            sb.Append(timeSpan.Seconds);
            sb.Append(" seconds");

            return sb.ToString();
        }
    }
}
