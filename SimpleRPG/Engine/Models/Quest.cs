using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class Quest
    {
        public int ID { get; }
        public string Name { get; }
        public string Description { get; }
        public List<ItemQuantity> ItemsNeeded { get; }
        public int RewardExperiencePoints { get; }
        public int RewardGold { get; }
        public List<ItemQuantity> RewardItems { get; }

        public Quest(int id, string name, string description, List<ItemQuantity> itemsNeeded, int rewardExperiencePoints, int rewardGold, List<ItemQuantity> rewardItems)
        {
            ID = id;
            Name = name;
            Description = description;
            ItemsNeeded = itemsNeeded;
            RewardExperiencePoints = rewardExperiencePoints;
            RewardGold = rewardGold;
            RewardItems = rewardItems;
        }
    }
}
