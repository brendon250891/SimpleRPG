using System.Collections.Generic;
using Engine.Models;
using System.Linq;
using System.IO;
using System.Xml;
using Engine.Shared;
using System.Reflection;

namespace Engine.Factories
{
    public static class RecipeFactory
    {
        private const string GAME_DATA_FILE = ".\\GameData\\Recipes.xml";

        private static readonly List<Recipe> _recipes = new();

        static RecipeFactory()
        {
            if (File.Exists(GAME_DATA_FILE))
            {
                XmlDocument data = new();
                data.LoadXml(File.ReadAllText(GAME_DATA_FILE));

                LoadRecipesFromNodes(data.SelectNodes("/Recipes/Recipe"));
            }
            else
            {
                throw new FileNotFoundException($"The file '{GAME_DATA_FILE} does not exist.");
            }
        }

        public static Recipe RecipeByID(int id)
        {
            return _recipes.FirstOrDefault(recipe => recipe.ID == id);
        }

        private static void LoadRecipesFromNodes(XmlNodeList recipes)
        {
            if(recipes == null)
            {
                return;
            }

            foreach(XmlNode recipe in recipes)
            {
                Recipe r = new(recipe.AttributeAsInt("ID"), recipe.SelectSingleNode("./Name").InnerText);
                GetRecipeItems(r, recipe.SelectNodes("./Ingredients/Item"));
                GetRecipeItems(r, recipe.SelectNodes("./OutputItems/Item"), false);

                _recipes.Add(r);
            }
        }

        private static void GetRecipeItems(Recipe recipe, XmlNodeList items, bool ingredient = true)
        {
            if(items == null)
            {
                return;
            }

            foreach(XmlNode item in items)
            {
                int id = item.AttributeAsInt("ID");
                int quantity = item.AttributeAsInt("Quantity");

                if (ingredient)
                {
                    recipe.AddIngredient(id, quantity);
                }
                else
                {
                    recipe.AddOutputItem(id, quantity);
                }
            }
        }
    }
}
