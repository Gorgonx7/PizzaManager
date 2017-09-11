using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace ACW2
{
    /// <summary>
    /// Element is used to combine use rates and ingredients, this is used instead of recipe ingredients as it will not change once created
    /// 
    /// </summary>
    public struct Element
    {
        public ingredient m_Ingredient; // associated ingredient
        public float UsePerRecipe; // use rates
        public Element(ingredient pIngredient, float pUsePerRecipe)
        {
            m_Ingredient = pIngredient;
            UsePerRecipe = pUsePerRecipe;
        }
        // gets and sets
        public float GetIngredientCost()
        {
            return m_Ingredient.getCostPerUnit() * UsePerRecipe;
        }
        public void useIngredient()
        {
            m_Ingredient.UseAmount(UsePerRecipe);
        }
        public void ReplenishIngredient()
        {
            m_Ingredient.UseAmount(-UsePerRecipe);
        }
    }
    /// <summary>
    /// order is the base class for all three of the order classes, it's abstract so it cannot be created itself
    /// </summary>
    public abstract class Order
    {
        protected string m_Name; // name of the item ordered
        protected float m_Cost; // the cost of the order
        protected float m_IngredientCost; // to total ingredient cost
        protected List<Element> m_Recipe = new List<Element>(); // all the ingredients that will be used in the order
        public Order(string pName, float pCost)
        {
            m_Name = pName;
            m_Cost = pCost;
        }
        // gets and sets
        public string GetName()
        {
            return m_Name;
        }
        public float GetCost()
        {
            return m_Cost;
        }
        /// <summary>
        /// used to return the recipe to the user
        /// </summary>
        /// <returns></returns>
        public List<Element> GetRecipe()
        {
            return m_Recipe;
        }
        public virtual List<Element> GetRecipe(string pName, size? pSize)
        {
            List<Element> Recipe = new List<Element>();
            StreamReader Reader = new StreamReader("menu.txt");
            while (!(Reader.EndOfStream))
            {
                string unProcessedInput = Reader.ReadLine();
                string[] ProcessedInput = unProcessedInput.Split(',');

                if (ProcessedInput[1].Trim() == pName.Trim() && ProcessedInput[2].Trim() == pSize.ToString())
                {
                    // we have found the matching recipe and extract the ingredientes
                    for (int x = 4; x < ProcessedInput.Length; x = x + 2)
                    {
                        Recipe.Add(new Element(MainWindow.masterInventory.FindIngredient(ProcessedInput[x]), float.Parse(ProcessedInput[x + 1])));
                    }
                }
            }


            return Recipe;

        }
        public float getIngredientCost()
        {
            float totalCost = 0;
            foreach (Element i in m_Recipe)
            {
                totalCost += i.GetIngredientCost();
            }
            return totalCost;
        }
        /// <summary>
        /// Every order has to edit and replenish the inventory, this will be done as orders are created not completed
        /// </summary>
        public abstract void EditInventory();

        public abstract void ReplenishInventory();
    }

    public class PizzaOrder : Order
    {
        private float m_extraPortionsize; // is decided due to the size of the pizza
        private bool m_hasStuffedCrust; // is taken directly from the tick box
        private float m_IngredientUseRate = 0f; // 
        private size m_Size; // the size of the order
        private List<ingredient> m_AdditionalIngredients; // the additional ingredients that will be used in the inventory
        public PizzaOrder(string pName, size pSize, bool pHasStuffedCrust, float pCost, float pExtraPortionSize, List<ingredient> pAdditionalIngredients) : base(pName, pCost)
        {
            m_AdditionalIngredients = pAdditionalIngredients;
            m_hasStuffedCrust = pHasStuffedCrust;

            switch (pSize)
            {
                case size.regular:
                    m_IngredientUseRate = 0.25f;
                    break;
                case size.large:
                    m_IngredientUseRate = 0.35f;
                    break;
                case size.extralarge:
                    m_IngredientUseRate = 0.75f;
                    break;
            }
            m_extraPortionsize = pExtraPortionSize;
            foreach (ingredient i in pAdditionalIngredients)
            {
                m_Recipe.Add(new Element(i, m_IngredientUseRate));
            }
            m_Recipe.AddRange(GetRecipe(m_Name, m_Size));
            m_Size = pSize;
        }
        public override List<Element> GetRecipe(string pName, size? pSize)
        {
            return base.GetRecipe(pName, pSize);
        }
        public override string ToString()
        {
            string Output = "Pizza, " + m_Name.Trim();
            if (m_hasStuffedCrust)
            {
                Output += ", Sutffed Crust";
            }
            foreach (ingredient i in m_AdditionalIngredients)
            {
                Output += ", Extra " + i.getName().Trim();
            }
            Output += ", £" + m_Cost;
            return Output;

        }
        /// <summary>
        /// edits the inventory for each ingredient that is used
        /// </summary>
        public override void EditInventory()
        {

            float DoughFactor = 1;
            float MozzarellaFactor = 1;
            if (m_hasStuffedCrust == true)
            {
                // take away 10% more dough and 15% more cheese 
                DoughFactor = 1.1f;
                MozzarellaFactor = 1.15f;
            }
            foreach (Element i in m_Recipe)
            {
                ingredient Ingredient = null;
                if (MainWindow.masterInventory.getInventory().Contains(i.m_Ingredient))
                {
                    int index = MainWindow.masterInventory.getInventory().LastIndexOf(i.m_Ingredient);
                    Ingredient = MainWindow.masterInventory.getInventory()[index];
                }


                if (Ingredient.getName() == "mozzarella")
                {
                    Ingredient.UseAmount(i.UsePerRecipe * MozzarellaFactor);
                    Debug.WriteLine(Ingredient.getNumberOfUnits());
                }
                else if (Ingredient.getName() == "dough")
                {
                    Ingredient.UseAmount(i.UsePerRecipe * DoughFactor);
                    Debug.WriteLine(Ingredient.getNumberOfUnits());
                }
                else
                {
                    Ingredient.UseAmount(i.UsePerRecipe);
                    Debug.WriteLine(Ingredient.getNumberOfUnits());
                }

            }

        }
        /// <summary>
        /// replenishes the inventory for each ingredient that has been used.
        /// </summary>
        public override void ReplenishInventory()
        {
            float DoughFactor = 1;
            float MozzarellaFactor = 1;
            if (m_hasStuffedCrust == true)
            {
                // take away 10% more dough and 15% more cheese 
                DoughFactor = 1.1f;
                MozzarellaFactor = 1.15f;
            }
            foreach (Element i in m_Recipe)
            {
                ingredient Ingredient = null;
                if (MainWindow.masterInventory.getInventory().Contains(i.m_Ingredient))
                {
                    int index = MainWindow.masterInventory.getInventory().LastIndexOf(i.m_Ingredient);
                    Ingredient = MainWindow.masterInventory.getInventory()[index];
                }


                if (Ingredient.getName() == "mozzarella")
                {
                    Ingredient.UseAmount(-(i.UsePerRecipe * MozzarellaFactor));

                }
                else if (Ingredient.getName() == "dough")
                {
                    Ingredient.UseAmount(-(i.UsePerRecipe * DoughFactor));

                }
                else
                {
                    Ingredient.UseAmount(-(i.UsePerRecipe));

                }

            }

        }

    }


    public class BurgerOrder : Order
    {
        bool m_hasCheese; // if the burger has cheese
        bool m_hasSalad; // if the burger has salad
        bool m_isBigger;
       
        /// <param name="pName">Name of the burger</param>
        /// <param name="pCost">Cost of the burger</param>
        /// <param name="hasCheese">if the burger has cheese</param>
        /// <param name="hasSalad">if the burger has salad</param>
        /// <param name="isBigger">if the burger is bigger, if not then it's a smaller burger</param>
        public BurgerOrder(string pName, float pCost, bool hasCheese, bool hasSalad, bool isBigger) : base(pName, pCost)
        {
            m_hasCheese = hasCheese;
            m_hasSalad = hasSalad;
            m_isBigger = isBigger;
            m_Cost = pCost;
            m_Recipe = GetRecipe(pName, null);
        }

        public override List<Element> GetRecipe(string pName, size? pSize)
        {
            if (m_isBigger)
            {
                return base.GetRecipe(pName, size.regular);
            }
            else
            {
                return base.GetRecipe(pName, size.large);
            }
        }
        public override void EditInventory()
        {
            if (m_hasCheese)
            {
                //subtract the relivent amount of cheese   
                ingredient cheese = MainWindow.masterInventory.FindIngredient("cheddar");
                cheese.UseAmount(0.2f);
            }
            if (m_hasSalad)
            {
                //subtract the relivent amount of salad
                ingredient salad = MainWindow.masterInventory.FindIngredient("salad");
                salad.UseAmount(0.5f);
            }
            foreach (Element i in m_Recipe)
            {
                i.useIngredient();
            }


        }
        public override void ReplenishInventory()
        {
            if (m_hasCheese)
            {
                //subtract the relivent amount of cheese   
                ingredient cheese = MainWindow.masterInventory.FindIngredient("cheddar");
                cheese.UseAmount(-0.2f);
            }
            if (m_hasSalad)
            {
                //subtract the relivent amount of salad
                ingredient salad = MainWindow.masterInventory.FindIngredient("salad");
                salad.UseAmount(-0.5f);
            }
            foreach (Element i in m_Recipe)
            {
                i.ReplenishIngredient();
            }

        }
        public override string ToString()
        {
            string Output = "Burger, " + m_Name.Trim() + ", ";
            if (m_isBigger)
            {
                Output += "Half-Pounder";
            }
            else
            {
                Output += "QuaterPounder";
            }
            if (m_hasCheese)
            {
                Output += ", Cheese";
            }
            if (m_hasSalad)
            {
                Output += ", Salad";
            }
            Output += ", £" + m_Cost;
            return Output;
        }
    }
    public class SundryOrder : Order
    {
        public SundryOrder(string pName, float pCost) : base(pName, pCost)
        {
            m_Recipe = GetRecipe(pName, size.regular);

        }
        public override string ToString()
        {
            return "Sundry, " + m_Name.Trim() + ", £" + m_Cost;
        }
        public override void EditInventory()
        {
            foreach (Element i in m_Recipe)
            {
                i.useIngredient();
            }
        }
        public override void ReplenishInventory()
        {
            foreach (Element i in m_Recipe)
            {
                i.ReplenishIngredient();
            }
        }
    }

}
