using System;
using Engine.Shared;
using Engine.Models;
using System.Xml;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Engine.Factories
{
    public static class MonsterFactory
    {
        private const string GAME_DATA_FILE = ".\\GameData\\Monsters.xml";

        private static readonly List<Monster> _baseMonsters = new();

        static MonsterFactory()
        {
            if (File.Exists(GAME_DATA_FILE))
            {
                XmlDocument data = new();
                data.LoadXml(File.ReadAllText(GAME_DATA_FILE));

                string rootImagePath = data.SelectSingleNode("/Monsters").AttributeAsString("RootImagePath");

                LoadMonstersFromNodes(data.SelectNodes("/Monsters/Monster"), rootImagePath);
            }
            else
            {
                throw new FileNotFoundException($"The file '{GAME_DATA_FILE}' does not exist.");
            }
        }

        public static Monster GetMonster(int id)
        {
            return _baseMonsters.FirstOrDefault(monster => monster.ID == id)?.GetNewInstance();
        }

        private static void AddLootItem(Monster monster, int itemID, int percentage)
        {
            if (RandomNumberGenerator.NumberBetween(1, 100) <= percentage)
            {
                monster.AddItemToInventory(ItemFactory.CreateGameItem(itemID));
            }
        }

        private static void LoadMonstersFromNodes(XmlNodeList monsters, string rootImagePath)
        {
            if(monsters == null)
            {
                return;
            }

            foreach(XmlNode monster in monsters)
            {
                Monster m = new(monster.AttributeAsInt("ID"), monster.AttributeAsString("Name"), $"{rootImagePath}{monster.AttributeAsString("ImageName")}", 
                    monster.AttributeAsInt("MaximumHitPoints"), monster.AttributeAsInt("MaximumHitPoints"), ItemFactory.CreateGameItem(monster.AttributeAsInt("WeaponID")), 
                    monster.AttributeAsInt("RewardExperience"), monster.AttributeAsInt("Gold"));

                XmlNodeList lootItems = monster.SelectNodes("./LootItems/LootItem");

                if (lootItems != null)
                {
                    foreach (XmlNode lootItem in lootItems)
                    {
                        m.AddItemToLootTable(lootItem.AttributeAsInt("ID"), lootItem.AttributeAsInt("Percentage"));
                    }
                }

                _baseMonsters.Add(m);
            }
        }
    }
}
