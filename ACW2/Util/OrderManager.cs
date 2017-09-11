using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace ACW2
{
   public class OrderManager
    {
        List<Order> m_Orders; // List of all the orders

        public OrderManager()
        {
            m_Orders = new List<Order>();
        }
        /// <summary>
        /// adds an order to the order manager
        /// </summary>
        /// <param name="pOrder"></param>
        public void AddOrder(Order pOrder)
        {
            m_Orders.Add(pOrder);
        }
        /// <summary>
        /// Removes an order from the Manager
        /// </summary>
        /// <param name="pOrder"></param>
        public void RemoveOrder(Order pOrder)
        {
            m_Orders.Remove(pOrder);
        }
        // gets and sets
        public List<Order> getOrders()
        {

            return m_Orders;
        }
        /// <summary>
        /// gets the names of all the makeable pizza recipes
        /// </summary>
        /// <returns></returns>
        public List<string> getPizzaNames()
        {
            List<string> PizzaNames = new List<string>();
            List<string> LimitedIngredients = Menu.Get0Ingredients();

            StreamReader Reader = new StreamReader("menu.txt");

            while (!(Reader.EndOfStream))
            {
                string input = Reader.ReadLine();
                int x = 0;
                string[] ProcessedInput = input.Split(',');
                while((int)input[x] == 32)
                {
                    x++;
                }
                if(input[x] == 'p')
                {
                    
                    if (PizzaNames.Contains(ProcessedInput[1]))
                    {
                        continue;
                    } 
                    else
                    {
                        bool Add = true;
                      for(int y = 4; y < ProcessedInput.Length; y = y + 2)
                        {
                            foreach(string i in LimitedIngredients)
                            {
                                if(ProcessedInput[y] == i)
                                {
                                    Add = false;
                                }
                            }
                        }
                        if (Add) {
                            PizzaNames.Add(ProcessedInput[1]);
                        }
                       }
                }


            }

            return PizzaNames;
        }
        /// <summary>
        /// gets the pizza toppings that are not low on ingredients
        /// </summary>
        /// <returns></returns>
        public List<ingredient> getPizzaIngrediants()
        {
            List<ingredient> Ingredients = new List<ingredient>();
            List<string> LimitedIngredients = Menu.Get0Ingredients();
            foreach (ingredient i in MainWindow.masterInventory.getInventory())
            {
                if(i.getType() == type.pizza)
                {
                    if (!LimitedIngredients.Contains(i.getName()))
                    {
                        Ingredients.Add(i);
                    }
                }
            }
            return Ingredients;
        }
        /// <summary>
        /// gets the names of sundrys that are in stock
        /// </summary>
        /// <returns></returns>
        public List<string> getSundryNames()
        {
            List<string> Names = new List<string>();
            StreamReader Reader = new StreamReader("menu.txt");
            List<string> UsedIngredients = Menu.Get0Ingredients();
            while (!Reader.EndOfStream)
            {
               string[] input = Reader.ReadLine().Split(',');
                if(input[0] == "sundry")
                {
                    if (!UsedIngredients.Contains(input[1]))
                    {
                        Names.Add(input[1]);
                    }
                }
            }
            return Names;
        }
        /// <summary>
        /// gets the makeable pizza sizes for a specific pizza
        /// </summary>
        /// <param name="pName"></param>
        /// <returns></returns>
        public List<size> getPizzaSizes(string pName)
        {
            List<size> Sizes = new List<size>();
            List<ingredient> Regular = Menu.getRecipeIngredients(pName, size.regular);
            List<ingredient> Large = Menu.getRecipeIngredients(pName, size.large);
            List<ingredient> ExtraLarge = Menu.getRecipeIngredients(pName, size.extralarge);
            foreach(ingredient i in Regular)
            {
                recipeIngredient Ingredient = i as recipeIngredient;
                if(Ingredient != null)
                {
                    ingredient MasterIngredient = MainWindow.masterInventory.FindIngredient(Ingredient.getName());
                    if(Ingredient.getUsedPerRecipe() > MasterIngredient.getNumberOfUnits())
                    {
                        return Sizes;
                    }

                }
            }

            Sizes.Add(size.regular);
            foreach (ingredient i in Large)
            {
                recipeIngredient Ingredient = i as recipeIngredient;
                if (Ingredient != null)
                {
                    ingredient MasterIngredient = MainWindow.masterInventory.FindIngredient(Ingredient.getName());
                    if (Ingredient.getUsedPerRecipe() > MasterIngredient.getNumberOfUnits())
                    {
                        return Sizes;
                    }

                }
            }
            Sizes.Add(size.large);
            foreach (ingredient i in ExtraLarge)
            {
                recipeIngredient Ingredient = i as recipeIngredient;
                if (Ingredient != null)
                {
                    ingredient MasterIngredient = MainWindow.masterInventory.FindIngredient(Ingredient.getName());
                    if (Ingredient.getUsedPerRecipe() > MasterIngredient.getNumberOfUnits())
                    {
                        return Sizes;
                    }

                }
            }
            Sizes.Add(size.extralarge);

            return Sizes;
        }
        /// <summary>
        /// gets the price of all the orders in total
        /// </summary>
        /// <returns></returns>
        public string getPriceOfOrders()
        {
            float TotalCost = 0f;
            foreach(Order i in m_Orders)
            {
               TotalCost += i.GetCost();
            }
            return "" + TotalCost;
        }
        /// <summary>
        /// gets the names of makeable burgers
        /// </summary>
        /// <returns></returns>
        public List<string> getBurgerNames()
        {
            List<string> BurgerNames = new List<string>();
            StreamReader Reader = new StreamReader("menu.txt");
            List<string> LimitedIngredient = Menu.Get0Ingredients();
            while (!(Reader.EndOfStream))
            {
                string input = Reader.ReadLine();
                int x = 0;
                
                string[] ProcessedInput = input.Split(',');
                while ((int)input[x] == 32)
                {
                    x++;
                }
                if (input[x] == 'b')
                {

                    if (BurgerNames.Contains(ProcessedInput[1]))
                    {
                        continue;
                    }
                    else
                    {
                        bool add = true;
                        for(int y = 4; y < ProcessedInput.Length; y = y + 2)
                        {
                            if (LimitedIngredient.Contains(ProcessedInput[x].Trim()))
                            {
                                add = false;
                            }
                        }
                        if (add)
                        {
                            BurgerNames.Add(ProcessedInput[1]);
                        }
                     }
                }
            }

            return BurgerNames;
        }
        /// <summary>
        /// gets the makeable sizes of a given burger type
        /// </summary>
        /// <param name="pName"></param>
        /// <returns></returns>
        public List<size> GetMakeAbleBurgerSizes(string pName)
        {
            List<size> Sizes = new List<size>();
            List<ingredient> BurgerIngredientsRegular = Menu.getRecipeIngredients(pName, size.regular);
            List<ingredient> BurgerIngredientsLarge = Menu.getRecipeIngredients(pName, size.large);
            foreach(ingredient i in BurgerIngredientsRegular)
            {
                recipeIngredient Ingredient = i as recipeIngredient;
                if(Ingredient != null)
                {
                    ingredient MasterIngredient = MainWindow.masterInventory.FindIngredient(Ingredient.getName());
                    if(Ingredient.getUsedPerRecipe() > MasterIngredient.getNumberOfUnits())
                    {
                        return Sizes;
                    }
                }
            }
            Sizes.Add(size.regular);
            foreach (ingredient i in BurgerIngredientsLarge)
            {
                recipeIngredient Ingredient = i as recipeIngredient;
                if (Ingredient != null)
                {
                    ingredient MasterIngredient = MainWindow.masterInventory.FindIngredient(Ingredient.getName());
                    if (Ingredient.getUsedPerRecipe() > MasterIngredient.getNumberOfUnits())
                    {
                        return Sizes;
                    }
                }
            }
            Sizes.Add(size.large);
            return Sizes;

        }
    }
    }

