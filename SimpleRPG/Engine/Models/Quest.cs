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

        public string ToolTipContents =>
            $"{Description}\r\n\r\n" +
            $"Items to complete the quest\r\n" +
            $"===========================\r\n" +
            $"{string.Join("\r\n", ItemsNeeded.Select(item => item.QuantityItemDescription))}\r\n\r\n" +
            $"Rewards\r\n" +
            $"===========================\r\n" +
            $"{RewardExperiencePoints} experience points\r\n" +
            $"{RewardGold} gold pieces\r\n" +
            $"{string.Join("\r\n", RewardItems.Select(item => item.QuantityItemDescription))}";

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
