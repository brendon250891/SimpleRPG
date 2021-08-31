using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class Recipe
    {
        public int ID { get; }

        public string Name { get; }

        public List<ItemQuantity> Ingredients { get; } = new();

        public List<ItemQuantity> OutputItems { get; } = new();

        public string ToolTipContents =>
            "Ingredients\r\n" +
            "===========\r\n" +
            $"{string.Join("\r\n", Ingredients.Select(ingredient => ingredient.QuantityItemDescription))}\r\n\r\n" +
            "Creates\r\n" +
            "==========\r\n" +
            $"{string.Join("\r\n", OutputItems.Select(outputItem => outputItem.QuantityItemDescription))}";

        public Recipe(int id, string name)
        {
            ID = id;
            Name = name;
        }

        public void AddIngredient(int itemId, int quantity)
        {
            if (!Ingredients.Any(i => i.ItemID == itemId))
            {
                Ingredients.Add(new ItemQuantity(itemId, quantity));
            }
        }

        public void AddOutputItem(int itemId, int quantity)
        {
            if (!OutputItems.Any(i => i.ItemID == itemId))
            {
                OutputItems.Add(new ItemQuantity(itemId, quantity));
            }
        }
    }
}
