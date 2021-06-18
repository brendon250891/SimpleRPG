using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Models;

namespace Engine.Factories
{
    internal static class WorldFactory
    {
        internal static World CreateWorld()
        {
            World newWorld = new();

            newWorld.AddLocation(-2, -1, "Farmer's Field", "There are rows of corn growing here, with giant rats hiding between them.", "FarmFields");
            newWorld.AddLocation(-1, -1, "Farmer's House", "This is the house of your neighbor, Farmer Ted.", "Farmhouse");
            newWorld.AddLocation(0, -1, "Home", "This is your home.", "Home");
            newWorld.AddLocation(-1, 0, "Trading Shop", "The shop of Susan, the trader.", "Trader");
            newWorld.AddLocation(0, 0, "Town Square", "You see a fountain here.", "TownSquare");
            newWorld.AddLocation(1, 0, "Town Gate", "There is a gate here, protecting the town from giant spiders.", "TownGate");
            newWorld.AddLocation(2, 0, "Spider Forest", "The trees in this forest are covered with spider webs.", "SpiderForest");
            newWorld.AddLocation(0, 1, "Herbalist's Hut", "You see a small hut, with plants drying from the roof.", "HerbalistsHut");

            newWorld.LocationAt(0, 1).AvailableQuests.Add(QuestFactory.GetQuestByID(1));

            newWorld.AddLocation(0, 2, "Herbalist's Garden", "There are many plants here, with snakes hiding behind them.", "HerbalistsGarden");

            return newWorld;
        }
    }
}
