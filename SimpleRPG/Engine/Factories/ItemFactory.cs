using System.Collections.Generic;
using System.Linq;
using Engine.Models;
using Engine.Actions;

namespace Engine.Factories
{
    public static class ItemFactory
    {
        private static readonly List<GameItem> _standardGameItems = new();

        static ItemFactory()
        {
            _standardGameItems = new();

            BuildWeapon(1001, "Pointy Stick", 1, 1, 2);
            BuildWeapon(1002, "Rusty Sword", 5, 1, 3);

            BuildWeapon(1501, "Snake fangs", 0, 0, 2);
            BuildWeapon(1502, "Rat claws", 0, 0, 2);
            BuildWeapon(1503, "Spider fangs", 0, 0, 4);

            BuildHealingItem(2001, "Health Potion", 5, 2);

            BuildMiscellaneousItem(3001, "Vial", 1);
            BuildMiscellaneousItem(3002, "Berries", 2);
            BuildMiscellaneousItem(3003, "Herbs", 2);

            BuildMiscellaneousItem(9001, "Snake Fang", 1);
            BuildMiscellaneousItem(9002, "Snakeskin", 2);
            BuildMiscellaneousItem(9003, "Rat tail", 1);
            BuildMiscellaneousItem(9004, "Rat fur", 2);
            BuildMiscellaneousItem(9005, "Spider fang", 1);
            BuildMiscellaneousItem(9006, "Spider silk", 2);
        }

        public static GameItem CreateGameItem(int itemTypeID)
        {
            return _standardGameItems.FirstOrDefault(gameItem => gameItem.ItemTypeID == itemTypeID)?.Clone();
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
    }
}
