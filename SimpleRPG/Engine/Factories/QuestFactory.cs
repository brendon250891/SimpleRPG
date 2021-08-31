using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using Engine.Models;
using Engine.Shared;

namespace Engine.Factories
{
    internal static class QuestFactory
    {
        private const string GAME_DATA_FILE = ".\\GameData\\Quests.xml";

        private static readonly List<Quest> _quests = new();

        static QuestFactory()
        {
            if(File.Exists(GAME_DATA_FILE))
            {
                XmlDocument data = new();
                data.LoadXml(File.ReadAllText(GAME_DATA_FILE));

                LoadQuestsFromNodes(data.SelectNodes("/Quests/Quest"));
            }
            else
            {
                throw new FileNotFoundException($"The file '{GAME_DATA_FILE}' does not exist.");
            }

        }

        internal static Quest GetQuestByID(int id)
        {
            return _quests.FirstOrDefault(quest => quest.ID == id);
        }

        private static void LoadQuestsFromNodes(XmlNodeList quests)
        {
            if(quests == null)
            {
                return;
            }

            foreach(XmlNode quest in quests)
            {
                List<ItemQuantity> itemsNeeded = GetQuestItems(quest.SelectNodes("./ItemsNeeded/Item"));
                List<ItemQuantity> rewardItems = GetQuestItems(quest.SelectNodes("./RewardItems/Item"));

                Quest q = new(quest.AttributeAsInt("ID"), quest.SelectSingleNode("./Name").InnerText, quest.SelectSingleNode("./Description").InnerText, itemsNeeded,
                    quest.AttributeAsInt("RewardExperiencePoints"), quest.AttributeAsInt("RewardGold"), rewardItems);

                _quests.Add(q);
            }
        }

        private static List<ItemQuantity> GetQuestItems(XmlNodeList items)
        {
            List<ItemQuantity> questItems = new();
            if(items != null)
            {
                foreach(XmlNode item in items)
                {
                    questItems.Add(new ItemQuantity(item.AttributeAsInt("ID"), item.AttributeAsInt("Quantity")));
                }
            }

            return questItems;
        }
    }
}
