using System.Collections.Generic;
using System.Linq;
using Engine.Models;

namespace Engine.Factories
{
    internal static class QuestFactory
    {
        private static readonly List<Quest> _quests = new();

        static QuestFactory()
        {
            List<ItemQuantity> itemsNeeded = new();
            List<ItemQuantity> rewardItems = new();

            itemsNeeded.Add(new(9001, 5));
            rewardItems.Add(new(1002, 1));

            _quests.Add(new(1, "Clear the herb garden", "Defeat the snakes in the Herbalist's garden", itemsNeeded, 25, 10, rewardItems));
        }

        internal static Quest GetQuestByID(int id)
        {
            return _quests.FirstOrDefault(quest => quest.ID == id);
        }
    }
}
