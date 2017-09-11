using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ACW2
{
        /// <summary>
        /// a class used to keep track of all the ingredients that have been used so far
        /// </summary>
         class UsedIngredient
        {
            string m_Name;
            float m_NoUnitsUsed;
            public UsedIngredient(string pName, float pNoUnitsUsed)
            {
                m_Name = pName;
                m_NoUnitsUsed = pNoUnitsUsed;
            }
            public string getName()
            {
                return m_Name;
            }
            public void AddUnitsUsed(float pUnitsAdded)
            {
                m_NoUnitsUsed += pUnitsAdded;
            }
            public override string ToString()
            {
                return m_Name + " | " + m_NoUnitsUsed;
            }
        }
    /// <summary>
    /// Interaction logic for CompletedOrdersWindow.xaml
    /// </summary>   
    public partial class CompletedOrdersWindow : Window
    {
        public static List<OrderManager> m_MasterOrderManager = new List<OrderManager>(); // the list of all the order managers, this is added to evertime an order is placed 
        private List<UsedIngredient> m_UsedIngredient = new List<UsedIngredient>(); // the list of all the ingredients that have been used
      
        
        public CompletedOrdersWindow()
        {
            InitializeComponent();
            int TotalNumberOfOrders = 0; // keeps track of the total number of orders
            foreach (OrderManager i in m_MasterOrderManager)
            {
                TotalNumberOfOrders += 1;
            }
            // sets all the text boxes to read only 
            GrossProfitTextbox.IsReadOnly = true;
            IngredientCostTextbox.IsReadOnly = true;
            numberOfBurgersSoldTextbox.IsReadOnly = true;
            numberOfPizzasSoldTextBox.IsReadOnly = true;
            NumberOfSundriesSoldTextbox.IsReadOnly = true;
            RevenueTextbox.IsReadOnly = true;
            TotalNumberOfOrdersTextbox.IsReadOnly = true;

            TotalNumberOfOrdersTextbox.Text = "" + TotalNumberOfOrders;
            calculateNumberOfOrders();
            calculateIngredientCost();
            calculateRevenu();
            calculateTotalProfit();
            GetUnitsUsed();
            DisplayUsedIngredients();
        }
        /// <summary>
        /// gets the number of units used in all the orders that have been made
        /// </summary>
        private void GetUnitsUsed()
        {
            foreach (OrderManager i in m_MasterOrderManager)
            {
                foreach (Order j in i.getOrders())
                {
                    List<Element> Ingredients = j.GetRecipe();
                    foreach (Element k in Ingredients)
                    {
                        bool hasBeenAdded = false;
                        if (m_UsedIngredient.Count == 0)
                        {
                            m_UsedIngredient.Add(new UsedIngredient(k.m_Ingredient.getName(), k.UsePerRecipe));
                            continue;
                        }
                        for (int x = 0; x < m_UsedIngredient.Count; x++)
                        {
                            if (k.m_Ingredient.getName().Trim() == m_UsedIngredient[x].getName().Trim())
                            {
                                m_UsedIngredient[x].AddUnitsUsed(k.UsePerRecipe);
                                hasBeenAdded = true;
                                break;
                            }

                        }
                        if (hasBeenAdded == false)
                        {
                            m_UsedIngredient.Add(new UsedIngredient(k.m_Ingredient.getName(), k.UsePerRecipe));
                        }
                    }
                }
            }
        }
        /// <summary>
        /// dispays all the used ingredients
        /// </summary>
        private void DisplayUsedIngredients()
        {
            foreach (UsedIngredient i in m_UsedIngredient)
            {
                IngredientsUsedListbox.Items.Add(i);
            }
        }
        /// <summary>
        /// calculates the number of the different types of orders
        /// </summary>
        private void calculateNumberOfOrders()
        {
            int Pizza = 0;
            int burger = 0;
            int sundry = 0;
            foreach (OrderManager i in m_MasterOrderManager)
            {
                foreach (Order j in i.getOrders())
                {

                    if (j as PizzaOrder != null)
                    {
                        Pizza++;
                    }
                    else if (j as BurgerOrder != null)
                    {
                        burger++;
                    }
                    else if (j as SundryOrder != null)
                    {
                        sundry++;
                    }


                }
            }
            numberOfPizzasSoldTextBox.Text = "" + Pizza;
            numberOfBurgersSoldTextbox.Text = "" + burger;
            NumberOfSundriesSoldTextbox.Text = "" + sundry;

        }
        /// <summary>
        /// calculates the total cost of all ingredients
        /// </summary>
        private void calculateIngredientCost()
        {
            float totalCost = 0;
            foreach (OrderManager i in m_MasterOrderManager)
            {
                foreach (Order j in i.getOrders())
                {
                    totalCost += j.getIngredientCost();
                }
            }
            IngredientCostTextbox.Text = "£ " + totalCost;
        }
        /// <summary>
        /// calculates the total revenue for the current order sets
        /// </summary>
        private void calculateRevenu()
        {
            double TotalCost = 0.0f;

            foreach (OrderManager i in m_MasterOrderManager)
            {
                foreach (Order j in i.getOrders())
                {
                    TotalCost += j.GetCost();
                }
            }
            TotalCost = Math.Round(TotalCost, 2);
            RevenueTextbox.Text = "£ " + TotalCost;
        }
        /// <summary>
        /// calculates the total profit of all orders
        /// </summary>
        private void calculateTotalProfit()
        {
            float Profit = float.Parse(RevenueTextbox.Text.Substring(1)) - float.Parse(IngredientCostTextbox.Text.Substring(1));
            GrossProfitTextbox.Text = "£ " + Profit;
        }

        private void IngredientsUsedListbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(IngredientsUsedListbox.SelectedItem != null)
            {
                IngredientsUsedListbox.SelectedItem = null;
            }
        }
    }
}
