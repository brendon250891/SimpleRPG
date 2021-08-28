using System;
using System.IO;
using System.Xml;
using System.Collections.Generic;
using System.Linq;
using Engine.Models;
using Engine.Actions;

namespace Engine.Factories
{
    public static class ItemFactory
    {
        private const string GAME_DATA_FILENAME = ".\\GameData\\GameItems.xml";

        private static readonly List<GameItem> _standardGameItems = new();

        static ItemFactory()
        {
            if (File.Exists(GAME_DATA_FILENAME))
            {
                XmlDocument data = new XmlDocument();
                data.LoadXml(File.ReadAllText(GAME_DATA_FILENAME));

                LoadItemsFromNodes(data.SelectNodes("/GameItems/Weapons/Weapon"));
                LoadItemsFromNodes(data.SelectNodes("/GameItems/HealingItems/HealingItem"));
                LoadItemsFromNodes(data.SelectNodes("/GameItems/MiscellaneousItems/MiscellaneousItem"));
            }
            else
            {
                throw new FileNotFoundException($"Missing data file: {GAME_DATA_FILENAME}");
            }
        }

        public static GameItem CreateGameItem(int itemTypeID)
        {
            return _standardGameItems.FirstOrDefault(gameItem => gameItem.ItemTypeID == itemTypeID)?.Clone();
        }

        public static string ItemName(int itemTypeId)
        {
            return _standardGameItems.FirstOrDefault(item => item.ItemTypeID == itemTypeId)?.Name ?? "";
        }

        private static void LoadItemsFromNodes(XmlNodeList nodes)
        {
            if (nodes == null)
            {
                return;
            }

            foreach (XmlNode node in nodes)
            {
                GameItem.ItemCategory itemCategory = DetermineItemCategory(node.Name);

                GameItem gameItem = new(itemCategory, GetXmlAttributeAsInt(node, "ID"), GetXmlAttributeAsString(node, "Name"), GetXmlAttributeAsInt(node, "Price"),
                    itemCategory == GameItem.ItemCategory.Weapon);

                if (itemCategory == GameItem.ItemCategory.Weapon)
                {
                    gameItem.Action = new AttackWithWeapon(gameItem, GetXmlAttributeAsInt(node, "MinimumDamage"), GetXmlAttributeAsInt(node, "MaximumDamage"));
                }
                else if (itemCategory == GameItem.ItemCategory.Consumable)
                {
                    gameItem.Action = new Heal(gameItem, GetXmlAttributeAsInt(node, "HitPointsToHeal"));
                }

                _standardGameItems.Add(gameItem);
            }
        }

        private static void BuildWeapon(int itemID, string name, int price, int minimumDamage, int maximumDamage)
        {
            GameItem weapon = new(GameItem.ItemCategory.Weapon, itemID, name, price, true);

            weapon.Action = new AttackWithWeapon(weapon, minimumDamage, maximumDamage);

            _standardGameItems.Add(weapon);
        }

        private static void BuildMiscellaneousItem(int itemID, string name, int price)
        {
            _standardGameItems.Add(new GameItem(GameItem.ItemCategory.Miscellaneous, itemID, name, price));
        }

        private static void BuildHealingItem(int ItemID, string name, int price, int hitPoints)
        {
            GameItem healingItem = new(GameItem.ItemCategory.Consumable, ItemID, name, price);
            healingItem.Action = new Heal(healingItem, hitPoints);

            _standardGameItems.Add(healingItem);
        }

        private static GameItem.ItemCategory DetermineItemCategory(string itemType)
        {
            switch (itemType)
            {
                case "Weapon":
                    return GameItem.ItemCategory.Weapon;
                case "HealingItem":
                    return GameItem.ItemCategory.Consumable;
                default:
                    return GameItem.ItemCategory.Miscellaneous;
            }
        }

        private static int GetXmlAttributeAsInt(XmlNode node, string attributeName)
        {
            return Convert.ToInt32(GetXmlAttribute(node, attributeName));
        }

        private static string GetXmlAttributeAsString(XmlNode node, string attributeName)
        {
            return GetXmlAttribute(node, attributeName);
        }

        private static string GetXmlAttribute(XmlNode node, string attributeName)
        {
            XmlAttribute attribute = node.Attributes?[attributeName];

            if (attribute == null)
            {
                throw new ArgumentException($"the attribute '{attributeName}' does not exist.");
            }

            return attribute.Value;
        }
    }
}
