using System.IO;
using System.Xml;
using Engine.Shared;
using Engine.Models;

namespace Engine.Factories
{
    internal static class WorldFactory
    {
        private const string GAME_DATA_FILENAME = ".\\GameData\\Locations.xml";

        internal static World CreateWorld()
        {
            World world = new();

            if (File.Exists(GAME_DATA_FILENAME))
            {
                XmlDocument data = new();
                data.LoadXml(File.ReadAllText(GAME_DATA_FILENAME));

                string rootImagePath = data.SelectSingleNode("/Locations").AttributeAsString("RootImagePath");

                LoadLocationsFromNodes(world, rootImagePath, data.SelectNodes("/Locations/Location"));
            }
            else
            {
                throw new FileNotFoundException($"Missing data file: {GAME_DATA_FILENAME}");
            }

            return world;
        }

        private static void LoadLocationsFromNodes(World world, string rootImagePath, XmlNodeList nodes)
        {
            if (nodes == null)
            {
                return;
            }

            foreach (XmlNode node in nodes)
            {
                Location location = new(node.AttributeAsInt("X"), node.AttributeAsInt("Y"), node.AttributeAsString("Name"), node.SelectSingleNode("./Description")?.InnerText ?? "", 
                    $".{rootImagePath}{node.AttributeAsString("ImageName")}");

                AddMonster(location, node.SelectNodes("./Monsters/Monster"));
                AddQuests(location, node.SelectNodes("./Quests/Quest"));
                AddTrader(location, node.SelectSingleNode("./Trader"));

                world.AddLocation(location);
            }
        }

        private static void AddMonster(Location location, XmlNodeList monsterNodes)
        {
            if (monsterNodes == null)
            {
                return;
            }

            foreach (XmlNode monster in monsterNodes)
            {
                location.AddMonster(monster.AttributeAsInt("ID"), monster.AttributeAsInt("Percent"));
            }
        }

        private static void AddQuests(Location location, XmlNodeList questNodes)
        {
            if (questNodes == null)
            {
                return;
            }

            foreach (XmlNode quest in questNodes)
            {
                location.AvailableQuests.Add(QuestFactory.GetQuestByID(quest.AttributeAsInt("ID")));
            }
        }

        private static void AddTrader(Location location, XmlNode traderNode)
        {
            if (traderNode == null)
            {
                return;
            }

            location.Trader = TraderFactory.GetTraderByID(traderNode.AttributeAsInt("ID"));
        }
    }
}
