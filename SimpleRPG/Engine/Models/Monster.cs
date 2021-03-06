using System.Collections.Generic;
using Engine.Factories;

namespace Engine.Models
{
    public class Monster : LivingEntity
    {
        private readonly List<ItemPercentage> _lootTable = new();

        #region Public Properties

        public int ID { get; }

        public string ImageName { get; }

        public int RewardExperiencePoints { get; }

        #endregion

        public Monster(int id, string name, string imageName, int maximumHitPoints, int currentHitPoints, GameItem currentWeapon, int rewardExperiencePoints, int gold)
            : base(name, maximumHitPoints, currentHitPoints, gold)
        {
            ID = id;
            ImageName = imageName;
            CurrentWeapon = currentWeapon;
            RewardExperiencePoints = rewardExperiencePoints;
        }

        public void AddItemToLootTable(int id, int percentage)
        {
            _lootTable.RemoveAll(item => item.ID == id);

            _lootTable.Add(new ItemPercentage(id, percentage));
        }

        public Monster GetNewInstance()
        {
            Monster newMonster = new(ID, Name, ImageName, MaximumHitPoints, CurrentHitPoints, CurrentWeapon, RewardExperiencePoints, Gold);

            foreach(ItemPercentage itemPercentage in _lootTable)
            {
                newMonster.AddItemToLootTable(itemPercentage.ID, itemPercentage.Percentage);

                if(RandomNumberGenerator.NumberBetween(1, 100) <= itemPercentage.Percentage)
                {
                    newMonster.AddItemToInventory(ItemFactory.CreateGameItem(itemPercentage.ID));
                }
            }

            return newMonster;
        }
    } 
}
