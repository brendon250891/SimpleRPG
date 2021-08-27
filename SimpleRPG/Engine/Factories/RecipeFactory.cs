using System.Collections.Generic;
using Engine.Models;
using System.Linq;

namespace Engine.Factories
{
    public static class RecipeFactory
    {
        private static readonly List<Recipe> _recipes = new();

        static RecipeFactory()
        {
            Recipe healthPotion = new(1, "Health Potion");
            healthPotion.AddIngredient(3001, 1);
            healthPotion.AddIngredient(3002, 1);
            healthPotion.AddIngredient(3003, 1);
            healthPotion.AddOutputItem(2001, 1);

            _recipes.Add(healthPotion);
        }

        public static Recipe RecipeByID(int id)
        {
            return _recipes.FirstOrDefault(recipe => recipe.ID == id);
        }
    }
}
