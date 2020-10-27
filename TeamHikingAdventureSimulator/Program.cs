using System;
using TeamHikingAdventureSimulator.Builders;
using TeamHikingAdventureSimulator.Entities;
using TeamHikingAdventureSimulator.Observers;


namespace TeamHikingAdventureSimulator
{
    class Program
    {
        static void Main(string[] args)
        {
            //Instructions:
            //Write a C# program that simulates a team hiking through a forest at night. 
            //The team encounters a series of narrow bridges along the way. At each bridge they may meet additional hikers who need their help to cross the bridge.

            //The narrow bridge can only hold two people at a time. 
            //They have one torch and because it's night, the torch has to be used when crossing the bridge. 
            //Each hiker can cross the bridge at different speeds. When two hikers cross the bridge together, they must move at the slower person's pace.            
            //Determine the fastest time that the hikers can cross each bridge and the total time for all crossings. 
            //The input to the program will be a yaml file that describes the speeds of each person, the bridges encountered, their length, 
            //and the additional hikers encountered along the way.
            //Your solution should assume any number of people and bridges can be provided as inputs.

            //Demonstrate the operation of your program using the following inputs: 
            //the hikers cross 3 bridges, at the first bridge(100 ft long) 
            //4 hikers cross(hiker A can cross at 100 ft/minute, B at 50 ft/minute, C at 20 ft/minute, and D at 10 ft/minute), 
            //at the second bridge(250 ft long) an additional hiker crosses with the team(E at 2.5 ft / minute),
            //and finally at the last bridge(150 ft long) two hikers are encountered(F at 25 ft / minute and G at 15 ft / minute).

            //You will be judged on the following:
            //Strategy(s) - there are several ways to solve the problem, you can provide more than one. The goal is to show us how you think.
            //Architecture and design - we want to see how well you architect and design solutions to complex problems.
            //Testing - we want to see how you approach testing of the solution.
            //Standards and best practices.
            //Explanation - as a C# thought leader in the organization you will be working with and mentoring other engineers. How well you can describe and explain your solution is very important.

            //Please provide a link to your git repo such as github or bitbucket with your solution.

            //I'm building the Adventure ata from the yaml file with a builder pattern.
            string inputFilePath = "./Data/Input.yaml";
            AdventureDataBuilder adventureDataBuilder = new AdventureDataBuilder(inputFilePath);
            adventureDataBuilder.Build();
            Adventure currentAdventure = new Adventure(adventureDataBuilder.GetAdventureData());
            //I'm using the observer pattern to fetch information about the adventure to log to the console to track progress.
            AdventureObserver adventureObserver = new AdventureObserver(currentAdventure);
            currentAdventure.Attach(adventureObserver);
            currentAdventure.Start();

        }
    }
}
