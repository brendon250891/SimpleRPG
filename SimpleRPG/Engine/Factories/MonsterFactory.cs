﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Models;

namespace Engine.Factories
{
    public static class MonsterFactory
    {
        public static Monster GetMonster(int monsterID)
        {
            switch(monsterID)
            {
                case 1:
                    Monster snake = new("Snake", "Snake", 4, 1, 2, 5, 1);
                    AddLootItem(snake, 9001, 25);
                    AddLootItem(snake, 9002, 75);

                    return snake;

                case 2:
                    Monster rat = new("Rat", "Rat", 5, 1, 2, 5, 1);
                    AddLootItem(rat, 9003, 25);
                    AddLootItem(rat, 9004, 75);

                    return rat;

                case 3:
                    Monster giantSpider = new("Giant Spider", "GiantSpider", 10, 1, 4, 10, 3);
                    AddLootItem(giantSpider, 9005, 25);
                    AddLootItem(giantSpider, 9006, 25);

                    return giantSpider;

                default:
                    throw new ArgumentException($"MonsterType '{monsterID}' does not exist");
            }
        }

        private static void AddLootItem(Monster monster, int itemID, int percentage)
        {
            if (RandomNumberGenerator.NumberBetween(1, 100) <= percentage)
            {
                monster.AddItemToInventory(ItemFactory.CreateGameItem(itemID));
            }
        }
    }
}
