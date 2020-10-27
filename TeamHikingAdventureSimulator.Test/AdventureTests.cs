using NUnit.Framework;
using System.Collections.Generic;
using TeamHikingAdventureSimulator.Entities;
using TeamHikingAdventureSimulator.Observers;

namespace TeamHikingAdventureSimulator.Test
{
    public class AdventureTests
    {
        AdventureData _adventureData;

        [SetUp]
        public void Setup()
        {
            _adventureData = new AdventureData();
        }

        private void SeedData()
        {
            List<Hiker> testHikers = new List<Hiker>();
            testHikers.Add(new Hiker { Name = "Hiker A", Speed = 10, StartingLocation = 0 });
            testHikers.Add(new Hiker { Name = "Hiker B", Speed = 5, StartingLocation = 0 });
            testHikers.Add(new Hiker { Name = "Hiker C", Speed = 3, StartingLocation = 1 });
            testHikers.Add(new Hiker { Name = "Hiker D", Speed = 77, StartingLocation = 2 });
            _adventureData.Hikers = testHikers;

            List<Bridge> testBridges = new List<Bridge>();
            testBridges.Add(new Bridge { Location = 1, Length = 10 });
            testBridges.Add(new Bridge { Location = 2, Length = 15 });
            testBridges.Add(new Bridge { Location = 3, Length = 30 });
            _adventureData.Bridges = testBridges;
        }

        [Test]
        public void TestAdventureFinishes()
        {
            SeedData();

            Adventure currentAdventure = new Adventure(_adventureData);
            currentAdventure.Start();

            Assert.IsTrue(currentAdventure.IsFinished);
        }

        [Test]
        public void TestAdventureFinishes_WithObserver()
        {
            SeedData();

            Adventure currentAdventure = new Adventure(_adventureData);            
            AdventureObserver adventureObserver = new AdventureObserver(currentAdventure);
            currentAdventure.Attach(adventureObserver);
            currentAdventure.Start();

            Assert.IsTrue(currentAdventure.IsFinished);
        }

        [Test]
        public void TestAdventure_ThrowsOverflow_OnTooLongTimespan()
        {
            SeedData();

            _adventureData.Hikers.Add(new Hiker { Name = "Hiker E", Speed = .1, StartingLocation = 0 });
            _adventureData.Hikers.Add(new Hiker { Name = "Hiker F", Speed = 999, StartingLocation = 2 });
            _adventureData.Hikers.Add(new Hiker { Name = "Hiker G", Speed = 200000, StartingLocation = 1 });
            _adventureData.Hikers.Add(new Hiker { Name = "Hiker H", Speed = 1, StartingLocation = 3 });
            _adventureData.Hikers.Add(new Hiker { Name = "Hiker I", Speed = 33, StartingLocation = 4 });
            _adventureData.Hikers.Add(new Hiker { Name = "Hiker J", Speed = 99999, StartingLocation = 1 });
            _adventureData.Hikers.Add(new Hiker { Name = "Hiker K", Speed = 555, StartingLocation = 2 });
            _adventureData.Hikers.Add(new Hiker { Name = "Hiker L", Speed = 110, StartingLocation = 4 });

            _adventureData.Bridges.Add(new Bridge { Location = 4, Length = 99999999999999999 });
            _adventureData.Bridges.Add(new Bridge { Location = 5, Length = 666666666666 });
            _adventureData.Bridges.Add(new Bridge { Location = 6, Length = 4444444444444 });
            _adventureData.Bridges.Add(new Bridge { Location = 7, Length = 66666666 });
            _adventureData.Bridges.Add(new Bridge { Location = 8, Length = .1 });
            _adventureData.Bridges.Add(new Bridge { Location = 9, Length = 1111 });
            _adventureData.Bridges.Add(new Bridge { Location = 10, Length = 99999 });

            Adventure currentAdventure = new Adventure(_adventureData);
            AdventureObserver adventureObserver = new AdventureObserver(currentAdventure);
            currentAdventure.Attach(adventureObserver);

            Assert.Throws<System.OverflowException>(() => currentAdventure.Start());
        }

        [Test]
        public void TestAdventure_EmptyData_ThrowsException()
        {
            Adventure currentAdventure = new Adventure(_adventureData);
            AdventureObserver adventureObserver = new AdventureObserver(currentAdventure);
            currentAdventure.Attach(adventureObserver);

            Assert.Throws<System.ArgumentNullException>(() => currentAdventure.Start());
        }

        [Test]

        public void TestAdventure_Bridges_BridgesNull_ThrowsExeption()
        {
            _adventureData.Hikers = new List<Hiker>();
            _adventureData.Hikers.Add(new Hiker { Name = "Hiker A", Speed = 10, StartingLocation = 0 });

            Adventure currentAdventure = new Adventure(_adventureData);
            AdventureObserver adventureObserver = new AdventureObserver(currentAdventure);
            currentAdventure.Attach(adventureObserver);

            Assert.Throws<System.ArgumentNullException>(() => currentAdventure.Start());
        }

        [Test]

        public void TestAdventure_Bridges_MultipleBridgesInSameLocation_ThrowsExeption()
        {
            SeedData();

            _adventureData.Bridges.Add(new Bridge { Location = 1, Length = 44444 });
            Adventure currentAdventure = new Adventure(_adventureData);
            AdventureObserver adventureObserver = new AdventureObserver(currentAdventure);
            currentAdventure.Attach(adventureObserver);

            Assert.Throws<System.ArgumentException>(() => currentAdventure.Start());
        }

        [Test]
        public void TestAdventure_HikersNull_ThrowsException()
        {
            _adventureData.Bridges = new List<Bridge>();
            _adventureData.Bridges.Add(new Bridge { Location = 1, Length = 100 });

            Adventure currentAdventure = new Adventure(_adventureData);
            AdventureObserver adventureObserver = new AdventureObserver(currentAdventure);
            currentAdventure.Attach(adventureObserver);

            Assert.Throws<System.ArgumentNullException>(() => currentAdventure.Start());
        }

        [Test]
        public void TestAdventure_NoHikersAtStartingLocation_ThrowsException()
        {
            _adventureData.Bridges = new List<Bridge>();
            _adventureData.Bridges.Add(new Bridge { Location = 1, Length = 100 });

            _adventureData.Hikers = new List<Hiker>();
            _adventureData.Hikers.Add(new Hiker { Name = "Hiker I", Speed = 33, StartingLocation = 4 });

            Adventure currentAdventure = new Adventure(_adventureData);
            AdventureObserver adventureObserver = new AdventureObserver(currentAdventure);
            currentAdventure.Attach(adventureObserver);

            Assert.Throws<System.ArgumentException>(() => currentAdventure.Start());
        }
        [Test]
        public void TestAdventure_HikerWithNegativeSpeed_ThrowsException()
        {
            SeedData();
            _adventureData.Hikers.Add(new Hiker { Name = "Hiker I", Speed = -1, StartingLocation = 4 });

            Adventure currentAdventure = new Adventure(_adventureData);
            AdventureObserver adventureObserver = new AdventureObserver(currentAdventure);
            currentAdventure.Attach(adventureObserver);

            Assert.Throws<System.ArgumentException>(() => currentAdventure.Start());
        }
    }
}