using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ACW2
{
    /// <summary>
    /// Enum type that keeps track of the type of recipe the ingredient is used in
    /// </summary>
    public enum type
    {
        pizza, burger, sundry
    }
     
     public class ingredient : IComparable<ingredient> // used to compare the ingredient to another ingredient
    {
        //pizza, beef, 0.50, 500
        type m_Type; // the type of recipe the ingredient is used in
        string m_Name; // the name of the ingredient 
        float m_CostPerUnit; // the cost per unit of the ingredient
        float m_NumberOfUnits; // the number of units that we posses 
        /// <summary>
        /// Constructor for ingredient class
        /// </summary>
        /// <param name="pType"> the type of recipe the ingredient is used in</param>
        /// <param name="pName">The name of the ingredient</param>
        /// <param name="pCostPerUnit">The cost per unit</param>
        /// <param name="pNumberOfUnits">The number of units that we posses</param>
        public ingredient(type pType, string pName, float pCostPerUnit, float pNumberOfUnits)
        {
            m_Type = pType; 
            m_Name = pName;
            m_CostPerUnit = pCostPerUnit;
            m_NumberOfUnits = pNumberOfUnits;
        }       
        protected ingredient()
        {
            // this is the constructor for the recipeingredient class
        }
        /// <summary>
        /// Used to edit the number of units when a unit is ordered, this is also used to replenish ingredients too
        /// </summary>
        /// <param name="pAmount">amount to us/replace</param>
        public void UseAmount(float pAmount)
        {          
            m_NumberOfUnits -= pAmount; //if pAmount is positive it will use, if negative it will replace 
        }
        /// <summary>
        /// This is used to display the information about ingredients
        /// </summary>
        /// <returns>the type, name, cost per unit and number of units of an ingredient</returns>
        public override string ToString()
        {
            return m_Type + " | " + m_Name + " | " + m_CostPerUnit + " | " + m_NumberOfUnits;
        }
        // gets and sets
        public type getType()
        {
            return m_Type;
        }
        public virtual string getName()
        {
            return m_Name;
        }
        public virtual float getCostPerUnit()
        {
            return m_CostPerUnit;
        }
        public void setNumberOfUnits(float pNumberOfUnits)
        {
            m_NumberOfUnits = pNumberOfUnits;
        }
        public virtual float getNumberOfUnits()
        {
            return m_NumberOfUnits;
        }
        /// <summary>
        /// implimentation of the icomparible interface
        /// </summary>
        /// <param name="obj"> is the ingredient to compare too</param>
        /// <returns> if 1 it's above object, 0 is the same and -1 is below object</returns>
        public int CompareTo(ingredient obj)
        {
            ingredient Item = obj; // assign the obj to a ingredient holder 
           
            if(Item == null) // if the ingredient is null...
            {
                throw new NullReferenceException("Tried to compare to an ingredient to a non ingredient"); // throw a new exception
            }
            int x = 0; // create the marker for the start of the word
            int y = 0; // create the marker for the second word
            while ((int)getName()[x] == 32) // check if the pointer is pointing at a white space
            {
                x++; // if it is increment x
            }
            while((int)Item.getName()[y] == 32) // check if the pointer is pointing at a white space
            {
                y++; // if it is increment y
            }
            if((int)getName()[x] < (int)Item.getName()[y]) // compare the first letters of there name to check that the alphebetical order
            {
                
                return -1; // if this is lower in the aphabet that the item, return -1
            } else if((int)getName()[x] > (int)Item.getName()[y])
            {
                return 1; // if this is heigher in the aphabet that the item, return 1
            }
            else
            {
                return 0; // if it's the same return 0
            }
        }
    }
    public class Inventory 
    {
        List<ingredient> m_Ingredient = new List<ingredient>(); // this is the master list of ingredients 
        public Inventory()
        {
            LoadInInventory(); // load in all the ingredients
        }
        /// <summary>
        /// Retruns the sorted invetory
        /// </summary>
        /// <returns></returns>
        public  List<ingredient> getInventory()
        {
            m_Ingredient.Sort();
            return m_Ingredient;
        }
        /// <summary>
        /// Finds an ingredient in the inventory
        /// </summary>
        /// <param name="pName">The name of the ingredient to Find</param>
        /// <returns>The ingredient or null</returns>
        public ingredient FindIngredient(string pName)
        {
            for(int x = 0; x < m_Ingredient.Count; x++) // for each ingredient in the inventory
            {
                if(m_Ingredient[x].getName().Trim() == pName.Trim()) // if the ingredient name matches the string...
                {
                    return m_Ingredient[x]; // return the ingredient
                }
            }
            return null; // if no ingredient found return null
        }
        /// <summary>
        /// get all the ingredietns that are used in a pizza
        /// </summary>
        /// <returns> a list of all the ingredients that are of a type pizza</returns>
        public  List<ingredient> getPizzaToppings()
        {
            List<ingredient> PizzaTopping = new List<ingredient>(); // create the list of pizza toppings
            foreach( ingredient i in m_Ingredient) // for each ingredient in the inventory...
            {
                if(i.getType() == type.pizza) // if the type is pizza...
                {
                    PizzaTopping.Add(i); // add the ingredient to the list
                }
            }
            return PizzaTopping; // return what ever is found
        }

        /// <summary>
        /// This method loads in all the ingredients
        /// </summary>
        private void LoadInInventory()
        {
            StreamReader reader = new StreamReader("inventory.txt"); // open a new stream reader to read the inventory in
            while (!reader.EndOfStream) // for each line in the inventory...
            {
                type TypeHolder; // type holder 
                string UnclippedInput = reader.ReadLine(); // read the line in
                string[] ProcessedInput = UnclippedInput.Split(','); // split the string by each ',' into an array
                switch (ProcessedInput[0].ToLower()) // switch the type string
                {
                    case "pizza": // if pizza set type
                        TypeHolder = type.pizza;
                        break;
                    case "burger": // if burger set type
                        TypeHolder = type.burger;
                        break;
                    case "sundry": // if sundry set type
                        TypeHolder = type.sundry;
                        break;
                    default: // should never reach this
                        TypeHolder = type.pizza; // if the text file is incorrect the default type is pizza
                        throw new Exception("THERE IS A ERROR IN THE INVENTORY FILE");
                        
                }
                m_Ingredient.Add(new ingredient(TypeHolder, ProcessedInput[1], float.Parse(ProcessedInput[2]), float.Parse(ProcessedInput[3]))); // add a new ingredient to the list using the information read in
            }
        }
     
     /// <summary>
     /// Print out the inventory to the text file uppon exiting the program
     /// </summary>
        public void printInventory()
        {
            StreamWriter writer = new StreamWriter("inventory.txt"); // create a new stream writer
            for(int x = 0; x < m_Ingredient.Count; x++) // for each ingredient in the inventory
            {
                
                writer.WriteLine(m_Ingredient[x].getType() + "," + m_Ingredient[x].getName() + "," + m_Ingredient[x].getCostPerUnit() + "," + m_Ingredient[x].getNumberOfUnits()); // write the details to the line
               
                
            }
            writer.Flush(); // flush the writer
            writer.Close(); // close the writer
        }

    }
}
