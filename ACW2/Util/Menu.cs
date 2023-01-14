using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace ACW2
{
    /// <summary>
    /// size to keep track of the different sizes of recipe
    /// </summary>
    public enum size
    {
        regular, large, extralarge
    }
    public class recipeIngredient : ingredient
    {
        private ingredient m_Ingredient; // the ingredient that is associated with this recipe ingredient
        private float m_usedPerRecipe; // the use rate of the ingredient in it's associated recipe
        private float m_costPerRecipe; // the cost per recipe 
        public recipeIngredient(ingredient pIngredient, float pUsedPerRecipe) 
        {
            m_Ingredient = pIngredient;
            m_usedPerRecipe = pUsedPerRecipe;
            m_costPerRecipe = pIngredient.getCostPerUnit() * m_usedPerRecipe; 
        }
        // gets and sets
        public float getCostPerRecipe()
        {
            return m_costPerRecipe;
        }
        public override float getCostPerUnit()
        {
            return m_Ingredient.getCostPerUnit();
        }
        public float getUsedPerRecipe()
        {
            return m_usedPerRecipe;
        }
        public override string getName()
        {
            return m_Ingredient.getName();
        }
        public override float getNumberOfUnits()
        {
            return m_Ingredient.getNumberOfUnits();
        }
        public override string ToString()
        {
            return m_Ingredient.getName() + " | " + m_usedPerRecipe + " | " + m_Ingredient.getCostPerUnit();
        }
    }
    struct Recipe : IComparable<Recipe>
    {
        //pizza, margherita, regular, 4.40, dough, 1.00, sauce, 0.25, mozzarella, 0.75
        private type m_Type; // the type of recipe i.e pizza burger or sundry
        private string m_SubType; // the subtype i.e name
        private size m_Size; // the size of the recipe i.e regular large ex large
        private decimal m_Price; // the price of the recipe
        private List<ingredient> m_Ingredients; // the ingredients that are used in the recipe
        public Recipe(type pType, string pSubType, size pSize, decimal pPrice, List<ingredient> pIngredients)
        {
            m_Type = pType;
            m_SubType = pSubType;
            m_Size = pSize;
            m_Price = pPrice;
            m_Ingredients = pIngredients;
        }
        //gets and sets
        public List<ingredient> getIngredients()
        {
            m_Ingredients.Sort();
            return m_Ingredients;
        }
        public decimal getPrice()
        {
            return m_Price;
        }
        public string getSubtype()
        {
            return m_SubType;
        }
        public type getType()
        {
            return m_Type;
        }
        public size GetSize()
        {
            return m_Size;
        }


        public override string ToString()
        {
            return m_Type + " | " + m_SubType + " | " + m_Size + " | " + m_Price;
        }
        /// <summary>
        /// Implimentation of the icomparible
        /// </summary>
        /// <param name="other">the comparible recipe</param>
        /// <returns>-1 lower in the alphabet, 0 the same 1 is heigher</returns>
        public int CompareTo(Recipe other)
        {
            int x = 0;
            int y = 0;
            while((int)m_SubType[x] == 32)
            {
                x++;
            }
            while((int)other.m_SubType[y] == 32)
            {
                y++;
            }
            if((int)m_SubType[x] < other.m_SubType[y])
            {
                return -1;
            } else if((int)m_SubType[x] > other.m_SubType[y])
            {
                return 1;   
            }
            else
            {
                return 0;
            }
        }
    }
    class Menu
    {
        List<Recipe> m_RecipeList; // the master list of recipes
        public Menu()
        {
            m_RecipeList = LoadInMenu();
        }
        /// <summary>
        /// Finds the ingredients for a recipe but not the relivent size
        /// </summary>
        /// <param name="pName"></param>
        /// <returns>returns the ingredients from the inventroy</returns>
        public static List<ingredient> FindIngredients(string pName)
        {
            List<ingredient> Ingredients = new List<ingredient>();
            StreamReader Reader = new StreamReader("menu.txt");

            while (!(Reader.EndOfStream))
            {
                string UnprocessedInput = Reader.ReadLine();
                string[] ProcessedString = UnprocessedInput.Split(',');
                if (ProcessedString[1].Trim() == pName.Trim())
                {
                    for (int x = 4; x < ProcessedString.Length; x = x + 2) // start the counter as at for due to the menu template having ingredients at space 4
                    {
                        Ingredients.Add(MainWindow.masterInventory.FindIngredient(ProcessedString[x].Trim())); // find the ingredient using the name
                    }
                    return Ingredients; // return all the ingredients found
                }
            }
            return Ingredients; // if not found return a null set of ingredients
        }
        /// <summary>
        /// Get the Recipes ingredients using the name of the recipe
        /// </summary>
        /// <param name="input">The name of the recipe that You want to find the ingredients too</param>
        /// <returns>returns the recipeingredients or null</returns>
        public static List<ingredient> getRecipeIngredients(string input)
        {
            List<Recipe> Recipes = LoadInMenu();

            for(int x = 0; x < Recipes.Count; x++) // for each recipe that has been loaded in
            {
                if(Recipes[x].getSubtype() == input) // if the name of the recipe is the same as the one we are looking for...
                { 
                    return Recipes[x].getIngredients(); // get the ingredients and return them
                }
            }
            return null;
        }
        /// <summary>
        /// Gets all the ingredients that are currently at zero
        /// </summary>
        /// <returns>a string of names of ingredients that are at zero</returns>
        public static List<string> Get0Ingredients()
        {
            List<string> NoIngredients = new List<string>();
            foreach(ingredient i in MainWindow.masterInventory.getInventory())// for each ingredient in the master inventory...
            {
                if(i.getNumberOfUnits() == 0.0f) // if the number of units are at zero
                {
                    NoIngredients.Add(i.getName());
                }
            }
            return NoIngredients; // return any ingredients found
        }      
        /// <summary>
        /// Finds the menu price for the pizza and size that 
        /// </summary>
        /// <param name="pName">the name of the pizza</param>
        /// <param name="pSize"> the size of the pizza</param>
        /// <returns> the price of the pizza in string form</returns>
        public static string findMenuPrice(string pName, size pSize)
        {
            
            StreamReader Reader = new StreamReader("menu.txt");
            while (!(Reader.EndOfStream))
            {
                string unprocessedinput = Reader.ReadLine();
                string[] Input = unprocessedinput.Split(',');
                string sizeString = "";
                switch (pSize) // switch the size to get the valid size string to look for
                {
                    case size.regular:
                    case size.large:
                        sizeString = pSize.ToString();
                        break;
                    case size.extralarge:
                        sizeString = "extra-large"; // as there is a discrepancey in the naming convention for the size
                        break;
                }
                if(Input[1].Trim() == pName.Trim() && Input[2].Trim() == sizeString.Trim()) // if the input maches the name and the size...
                {
                    return Input[3]; //return the price 
                }

            }
            throw new Exception("Unfound Recipe"); // throw exception if recipe isnt found
        }
        /// <summary>
        /// Finds the douch and cheese recipe ingredient for the relivent recipe
        /// </summary>
        /// <param name="pName"> the name of the pizza</param>
        /// <param name="pSize">the size of the pizza</param>
        /// <returns>either nothing or the cheese and dough ingredients</returns>
        public static List<recipeIngredient> FindDoughAndCheese(string pName, size pSize)
        {
           
            List<recipeIngredient> Ingredients = new List<recipeIngredient>();
            List<ingredient> Recipe = getRecipeIngredients(pName, pSize); // finds the ingredients found in the recipe
            
                
                    foreach(ingredient j in Recipe) // for each ingredient in the found recipe
                    {
                        if(j as recipeIngredient != null && (j.getName().Trim() == "dough" || j.getName().Trim() == "mozzarella")) // if the ingreidient is a recipeingredient and is either dough or cheese
                        Ingredients.Add(j as recipeIngredient); // add it to the list
                    }
                    return Ingredients; // return the ingredients to the end
        }

        /// <summary>
        /// Loads in the Recipes from the menu.txt and converts them to recipes
        /// </summary>
        /// <returns>a list of recipes that have been obtained from menu.txt</returns>
        private static List<Recipe> LoadInMenu()
        {
            List<Recipe> output = new List<Recipe>();
            StreamReader reader = new StreamReader("menu.txt");
            while (!reader.EndOfStream)
            {
                string unProcessedLine = reader.ReadLine();
                string[] ProcessedString = unProcessedLine.Split(',');
                type holder;
                switch (ProcessedString[0].ToLower()) // find the type of recipe from the text file
                {
                    case "pizza":
                        holder = type.pizza;
                        break;
                    case "burger":
                        holder = type.burger;
                        break;
                    case "sundry":
                        holder = type.sundry;
                        break;
                    default:
                        holder = type.pizza;
                        throw new Exception("THERE IS A ERROR IN  THE MENU FILE");

                }
                size SizeHolder;
                
                switch (ProcessedString[2].ToLower().Trim()) // finding the size of the recipe
                {
                    case "regular":
                        SizeHolder = size.regular;
                        break;
                    case "large":
                        SizeHolder = size.large;
                        break;
                    case "extra-large":
                        SizeHolder = size.extralarge;
                        break;
                    default:
                        MainWindow.showMessage(ProcessedString[2]);
                        throw new Exception("THERE IS A ERROR IN THE MENU FILE");// throws an error 
                }
                List<string> IngredientNames = new List<string>();
                List<string> IngredientUseRates = new List<string>();
                for (int x = 4; x < ProcessedString.Length; x++) // for each word after the first ingredient word
                {
                    if (x % 2 == 0) // if its even its a ingredient name
                    {
                        IngredientNames.Add(ProcessedString[x]);
                    }
                    else // if it's odd its a use rate
                    {
                        IngredientUseRates.Add(ProcessedString[x]);
                    }
                }
                // create a new recipe using the find ingredients method to make the recipe ingredients
                output.Add(new Recipe(holder, ProcessedString[1], SizeHolder, decimal.Parse(ProcessedString[3]), FindIngredients(IngredientNames, IngredientUseRates))); 
            }


            return output;
        }      
        /// <summary>
        /// Find the ingredients in the master inventory and make them into recipe ingredients
        /// </summary>
        /// <param name="pName">Name of the ingredient</param>
        /// <param name="pProcessedString">use rate</param>
        /// <returns>list of recipe ingredients</returns>
        private static List<ingredient> FindIngredients(List<string> pName, List<string> pProcessedString)
        {
            List<ingredient> ingredients = new List<ingredient>();
            for (int x = 0; x < pName.Count; x++) //  for each ingredient the recipe has..
            {
                foreach (ingredient i in MainWindow.masterInventory.getInventory()) // for each ingredient in the master inventory...
                {
                    
                    if (i.getName().ToLower() == pName[x].ToLower()) //compare the name of the recipe ingredient to the inventory ingredient
                    {
                        ingredients.Add(new recipeIngredient(i,float.Parse(pProcessedString[x]))); // create a new recipe ingredient with the use rate and the normal ingredient
                    }
                }
            }
            return ingredients;
        }
        public List<Recipe> GetRecipes()
        {
            m_RecipeList.Sort();
            return m_RecipeList;
        }       
        /// <summary>
        /// get the recipe ingredients to a recipe without having to create a new Menu instance
        /// </summary>
        /// <param name="pName">Name of the recipe</param>
        /// <param name="pSize">Size of the Recipe</param>
        /// <returns></returns>
        public static List<ingredient> getRecipeIngredients(string pName, size pSize)
        {
            StreamReader Reader = new StreamReader("menu.txt");

            List<ingredient> recipe = new List<ingredient>();
            while (!(Reader.EndOfStream))
            {
                string UnprocessedString = Reader.ReadLine();
                string[] Input = UnprocessedString.Split(',');
                if (pSize == size.extralarge) // special case if extra large due to naming discrepancy
                {
                    if (Input[1].Trim() == pName.Trim() && Input[2].Trim() == "extra-large") // if the recipe matches the search criteria...
                    {
                        for (int x = 4; x < Input.Length; x += 2)
                        {
                            recipe.Add(new recipeIngredient(MainWindow.masterInventory.FindIngredient(Input[x].Trim()), float.Parse(Input[x + 1]))); // create a new list of recipe ingredients 
                        }
                        return recipe;
                    }
                }
                else
                {
                    if (Input[1].Trim() == pName.Trim() && Input[2].Trim() == pSize.ToString().Trim())// if the recipe matches the search criteria...
                    {
                        for (int x = 4; x < Input.Length; x += 2)
                        {
                            recipe.Add(new recipeIngredient(MainWindow.masterInventory.FindIngredient(Input[x].Trim()), float.Parse(Input[x + 1])));// create a new list of recipe ingredients 
                        }
                        return recipe;
                    }
                }


            }

            return recipe;

        }
    }
}
