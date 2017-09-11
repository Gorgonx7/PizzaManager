using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACW2
{
    public static class MoneyManager
    {
        /// <summary>
        /// calculates the total cost of a list of recipe ingredients
        /// </summary>
        /// <param name="Ingredients"></param>
        /// <returns></returns>
        public static float calculateIngredientCost(List<ingredient> Ingredients)
        {
            float total = 0f;
            
            for (int x = 0; x < Ingredients.Count; x++)
            {
                recipeIngredient Ingredient = Ingredients[x] as recipeIngredient;
                if (Ingredient != null)
                {
                    total += Ingredient.getCostPerRecipe();
                }
                }
            return total;
        }



    }
}
