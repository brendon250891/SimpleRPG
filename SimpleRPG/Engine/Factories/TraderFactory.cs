using System.Collections.Generic;
using System.IO;
using System.Linq;
using Engine.Shared;
using System.Xml;
using Engine.Models;

namespace Engine.Factories
{
    public class TraderFactory
    {
        private const string GAME_DATA_FILE = ".\\GameData\\Traders.xml";

        private static readonly List<Trader> _traders = new();

        static TraderFactory()
        {
            if(File.Exists(GAME_DATA_FILE))
            {
                XmlDocument data = new();
                data.LoadXml(File.ReadAllText(GAME_DATA_FILE));

                LoadTradersFromNodes(data.SelectNodes("/Traders/Trader"));
            }
            else
            {
                throw new FileNotFoundException($"The file '{GAME_DATA_FILE}' does not exist.");
            }
        }

        public static Trader GetTraderByID(int id)
        {
            return _traders.FirstOrDefault(trader => trader.ID == id);
        }

        private static void LoadTradersFromNodes(XmlNodeList traders)
        {
            if(traders == null)
            {
                return;
            }

            foreach(XmlNode trader in traders)
            {
                Trader t = new(trader.AttributeAsInt("ID"), trader.SelectSingleNode("./Name")?.InnerText ?? "");

                foreach(XmlNode item in trader.SelectNodes("./InventoryItems/Item"))
                {
                    int quantity = item.AttributeAsInt("Quantity");

                    for(int i = 0; i < quantity; i++)
                    {
                        t.AddItemToInventory(ItemFactory.CreateGameItem(item.AttributeAsInt("ID")));
                    }
                }
                _traders.Add(t);
            }
        }
    }
}
