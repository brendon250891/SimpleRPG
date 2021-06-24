using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Models;

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
            _standardGameItems.Add(new GameItem(GameItem.ItemCategory.Weapon, itemID, name, price, true, minimumDamage, maximumDamage));
        }

        private static void BuildMiscellaneousItem(int itemID, string name, int price)
        {
            _standardGameItems.Add(new GameItem(GameItem.ItemCategory.Miscellaneous, itemID, name, price));
        }
    }
}
