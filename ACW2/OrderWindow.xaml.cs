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
    /// Interaction logic for OrderWindow.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {
        OrderManager m_OrderManager = new OrderManager(); // creates a new order uppon creation of the window
        bool hasBeenCompleted = false; // if the order has not been completed all the ingredients need to be replaced

        public OrderWindow()
        {
            InitializeComponent();

            UpdatePizzaSelection();
            UpdateBurgerSelection();
            UpdateSundrySelection();
            UpdateCheeseAndSaladBox();
            ExtraToppingList.SelectionMode = SelectionMode.Multiple;


            BurgerPriceTextbox.Text = "£ 0.0";
            BurgerPriceTextbox.IsReadOnly = true;
            PizzaPriceTextBox.Text = "£ 0.0";
            PizzaPriceTextBox.IsReadOnly = true;
            SundriesPriceTextbox.Text = "£ 0.0";
            SundriesPriceTextbox.IsReadOnly = true;
            NetProfitTextBox.Text = "£ 0.0";
            NetProfitTextBox.IsReadOnly = true;
            TotalIngredientCostTextBox.Text = "£ 0.0";
            TotalIngredientCostTextBox.IsReadOnly = true;
            TotalPriceTextbox.Text = "£ 0.0";
            TotalPriceTextbox.IsReadOnly = true;
        }
        /// <summary>
        /// updates the cheese and salad boxes on the burger order section of the window
        /// </summary>
        private void UpdateCheeseAndSaladBox()
        {
            hasSalad.IsChecked = false;
            hasCheese.IsChecked = false;

            if (MainWindow.masterInventory.FindIngredient("cheddar").getNumberOfUnits() < 0.5f)
            {
                hasCheese.IsEnabled = false;
            }
            else
            {
                hasCheese.IsEnabled = true;
            }
            if (MainWindow.masterInventory.FindIngredient("salad").getNumberOfUnits() < 0.2f)
            {
                hasSalad.IsEnabled = false;
            }
            else
            {
                hasSalad.IsEnabled = true;
            }
        }
        /// <summary>
        /// checks if the selected pizza can have stuffed crust due to ingredient sizes
        /// </summary>
        private void UpdateStuffedCrustBox()
        {
            hasStuffedCrust.IsChecked = false;
            if (PizzacomboBox.SelectedItem != null && PizzaSizecomboBox1.SelectedItem != null)
            {
                ingredient cheese = MainWindow.masterInventory.FindIngredient("mozzarella");
                ingredient dough = MainWindow.masterInventory.FindIngredient("dough");
                List<ingredient> recipeIngredients = Menu.getRecipeIngredients(PizzacomboBox.SelectedItem.ToString(), (size)PizzaSizecomboBox1.SelectedItem);

                recipeIngredient cheeseUsed = null;
                recipeIngredient doughUsed = null;
                foreach (ingredient i in recipeIngredients)
                {
                    recipeIngredient Ingredient = i as recipeIngredient;
                    if (Ingredient != null)
                    {
                        if (i.getName().Trim() == "mozzarella")
                        {
                            cheeseUsed = Ingredient;
                        }
                        if (i.getName().Trim() == "dough")
                        {
                            doughUsed = Ingredient;
                        }
                    }
                }

                if (cheese.getNumberOfUnits() < cheeseUsed.getUsedPerRecipe() + (cheeseUsed.getUsedPerRecipe() * 0.1f) || dough.getNumberOfUnits() < doughUsed.getUsedPerRecipe() + (doughUsed.getUsedPerRecipe() * 0.15f))
                {
                    hasStuffedCrust.IsEnabled = false;
                }
                else
                {
                    hasStuffedCrust.IsEnabled = true;
                }
            }

        }
        /// <summary>
        /// sees if a burger can be made and in what sizes
        /// </summary>
        private void UpdateBurgerSize()
        {
            List<size> UseableSizes;
            QuaterRadioButton.IsEnabled = true;
            HalfRadioButton.IsEnabled = true;
            QuaterRadioButton.IsChecked = false;
            HalfRadioButton.IsChecked = false;

            if (BurgercomboBox.SelectedItem != null)
            {
                UseableSizes = m_OrderManager.GetMakeAbleBurgerSizes(BurgercomboBox.SelectedItem.ToString());
                if (!UseableSizes.Contains(size.regular))
                {
                    QuaterRadioButton.IsEnabled = false;
                }
                else
                {
                    QuaterRadioButton.IsEnabled = true;
                }
                if (!UseableSizes.Contains(size.large))
                {
                    HalfRadioButton.IsEnabled = false;
                }
                else
                {
                    HalfRadioButton.IsEnabled = true;
                }

            }
        }
        /// <summary>
        /// gets the names of the pizza selections and then displays them
        /// </summary>
        private void UpdatePizzaSelection()
        {

            PizzacomboBox.Items.Clear();
            List<string> PizzaNames = m_OrderManager.getPizzaNames();
            for (int x = 0; x < PizzaNames.Count; x++)
            {
                PizzacomboBox.Items.Add(PizzaNames[x]);
            }
        }
        /// <summary>
        /// gets the names of the burger selections and then displays them
        /// </summary>
        private void UpdateBurgerSelection()
        {
            BurgercomboBox.Items.Clear();
            List<string> BurgerNames = m_OrderManager.getBurgerNames();
            foreach (string i in BurgerNames)
            {
                BurgercomboBox.Items.Add(i);
            }
        }
        /// <summary>
        /// gets the names of the sundry selections and then displays them
        /// </summary>
        private void UpdateSundrySelection()
        {
            SundriesListBox.Items.Clear();
            List<string> SundryNames = m_OrderManager.getSundryNames();
            foreach (string i in SundryNames)
            {
                SundriesListBox.Items.Add(i.Trim());
            }
        }
        /// <summary>
        /// check the sizes of pizzas that can be made and then displays the makeable pizzas
        /// </summary>
        private void UpdateSizeBox()
        {
            if (PizzacomboBox.SelectedItem != null)
            {
                PizzaSizecomboBox1.Items.Clear();
                List<size> Sizes = m_OrderManager.getPizzaSizes(PizzacomboBox.SelectedItem.ToString());
                foreach (size i in Sizes)
                {
                    PizzaSizecomboBox1.Items.Add(i);
                }
            }
        }
        /// <summary>
        /// Filters the toppings in the toppings box so that only usable ingredients are shown
        /// </summary>
        /// <param name="pFilter"></param>
        private void FilterToppingsListBox(string pFilter)
        {
            if (PizzaSizecomboBox1.SelectedItem != null) {
                List<ingredient> PizzaIngredients = m_OrderManager.getPizzaIngrediants();
                List<ingredient> RecipeIngredients = Menu.FindIngredients(pFilter);
                float IngredientUseRate = 0;
                switch ((size)PizzaSizecomboBox1.SelectedItem)
                {
                    case size.regular:
                        IngredientUseRate = 0.25f;
                        break;
                    case size.large:
                        IngredientUseRate = 0.35f;
                        break;
                    case size.extralarge:
                        IngredientUseRate = 0.75f;
                        break;
                }
            ExtraToppingList.Items.Clear();
                foreach (ingredient i in PizzaIngredients)
                {


                    if (i.getName().Trim() == "dough" || RecipeIngredients.Contains(i) || i.getNumberOfUnits() < IngredientUseRate)
                    {
                        continue;
                    }
                    else
                    {

                        ExtraToppingList.Items.Add(i);
                    }
                }
            }
        }
        /// <summary>
        /// updates the master order list box
        /// </summary>
        public void UpdateOrderList()
        {
            OrderSummarylistBox.Items.Clear();
            List<Order> Orders = m_OrderManager.getOrders();
            for (int x = 0; x < Orders.Count; x++)
            {
                OrderSummarylistBox.Items.Add(Orders[x]);
            }
        }
        /// <summary>
        /// gets the topping use rates for the pizza that is curretly selected 
        /// </summary>
        /// <returns></returns>
        private float getToppingUseRate()
        {
            float useRate = 0;
            switch ((size)PizzaSizecomboBox1.SelectedItem)
            {
                case size.regular:
                    useRate = 0.25f;
                    break;
                case size.large:
                    useRate = 0.35f;
                    break;
                case size.extralarge:
                    useRate = 0.75f;
                    break;
            }
            return useRate;
        }
        /// <summary>
        /// updates the total cost of the pizza options selected 
        /// </summary>
        private void UpdatePizzaCostBox()
        {
            if (PizzacomboBox.SelectedItem != null && PizzaSizecomboBox1.SelectedItem != null)
            {

                double totals = 0;
                float useRate = getToppingUseRate();

                foreach (ingredient i in ExtraToppingList.SelectedItems)
                {
                    if (i != null)
                        totals += i.getCostPerUnit() * useRate;

                }


                if ((bool)hasStuffedCrust.IsChecked)
                {
                    List<recipeIngredient> Recipe = Menu.FindDoughAndCheese(PizzacomboBox.SelectedItem.ToString(), (size)PizzaSizecomboBox1.SelectedItem);
                    // need to find the correct recipe and size now
                    foreach (recipeIngredient i in Recipe)
                    {
                        if (i.getName().Trim() == "dough")
                        {
                            totals += i.getCostPerUnit() * i.getUsedPerRecipe() * 0.1f;
                        }
                        if (i.getName().Trim() == "mozzarella")
                        {
                            totals += i.getCostPerUnit() * i.getUsedPerRecipe() * 0.1f;
                        }
                    }
                }
                totals += float.Parse(Menu.findMenuPrice(PizzacomboBox.SelectedItem.ToString(), (size)PizzaSizecomboBox1.SelectedItem));
                totals = Math.Round(totals, 2);
                PizzaPriceTextBox.Text = "£ " + totals + "";



            }
        }
        /// <summary>
        ///  updates the total of the order
        /// </summary>
        private void UpdateOrderTotal()
        {
            TotalPriceTextbox.Text = "£ " + m_OrderManager.getPriceOfOrders();
            UpdateIngredientCost();
            UpdateNetProfit();
        }
        /// <summary>
        ///  updates the total of the ingredients cost
        /// </summary>
        private void UpdateIngredientCost()
        {

            List<Order> Orders = m_OrderManager.getOrders();
            double Total = 0.0f;
            foreach (Order i in Orders)
            {
                List<Element> Ingredients = i.GetRecipe();
                foreach (Element j in Ingredients)
                {
                    Total += j.GetIngredientCost();
                }
            }
            Total = Math.Round(Total, 2);
            TotalIngredientCostTextBox.Text = "£ " + Total;
        }
        /// <summary>
        /// updates the net profit 
        /// </summary>
        private void UpdateNetProfit()
        {
            double Total = float.Parse(TotalPriceTextbox.Text.Substring(1)) - float.Parse(TotalIngredientCostTextBox.Text.Substring(1));
            Total = Math.Round(Total, 2);
            NetProfitTextBox.Text = "£ " + Total;
        }
        /// <summary>
        /// updates the total burger cost with the currently selected burger option
        /// </summary>
        private void UpdateBurgerCostBox()
        {
            BurgerPriceTextbox.Text = "";
            double totalPrice = 0.0f;
            if (BurgercomboBox.SelectedItem != null && ((bool)QuaterRadioButton.IsChecked || (bool)HalfRadioButton.IsChecked))
            {

                if ((bool)QuaterRadioButton.IsChecked)
                {
                    totalPrice += float.Parse(Menu.findMenuPrice((string)BurgercomboBox.SelectedItem, size.regular));

                }
                else
                {
                    totalPrice += float.Parse(Menu.findMenuPrice((string)BurgercomboBox.SelectedItem, size.large));
                }
            }
            if ((bool)hasCheese.IsChecked)
            {
                totalPrice += MainWindow.masterInventory.FindIngredient("cheddar").getCostPerUnit() * 0.2f;
            }
            if ((bool)hasSalad.IsChecked)
            {
                totalPrice += MainWindow.masterInventory.FindIngredient("salad").getCostPerUnit() * 0.5f;
            }
            totalPrice = Math.Round(totalPrice, 2);
            BurgerPriceTextbox.Text = "£ " + totalPrice;
        }
      /// <summary>
        /// Master Update method that calls each indervidual update method
        /// </summary>
        private void Update()
        {
            UpdateCheeseAndSaladBox();
            UpdateStuffedCrustBox();
            UpdateBurgerSelection();
            UpdateBurgerSize();
            UpdateBurgerCostBox();
            UpdateIngredientCost();
            UpdateNetProfit();
            UpdatePizzaSelection();
            UpdatePizzaCostBox();
            UpdateSizeBox();
            UpdateSundrySelection();
            UpdateNetProfit();
            ExtraToppingList.Items.Clear();
            UpdateOrderList();
            UpdateOrderTotal();
        }

        private void OrderSummarylistBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void PizzacomboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PizzacomboBox.SelectedItem != null)
            {
                FilterToppingsListBox(PizzacomboBox.SelectedValue.ToString());
            }
            UpdateStuffedCrustBox();
            UpdateSizeBox();
            UpdatePizzaCostBox();

        }
        private void PizzaAddToOrder_Click(object sender, RoutedEventArgs e)
        {
            List<ingredient> m_selectedItems = new List<ingredient>();
            // todoAdd all the info to this 
            foreach (ingredient i in ExtraToppingList.SelectedItems)
            {
                m_selectedItems.Add(i);
            }


            try
            {
                PizzaOrder order = new PizzaOrder(PizzacomboBox.SelectedItem.ToString(), (size)PizzaSizecomboBox1.SelectedItem, (bool)hasStuffedCrust.IsChecked, float.Parse(PizzaPriceTextBox.Text.Substring(1)), getToppingUseRate(), m_selectedItems);
                m_OrderManager.AddOrder(order);
                order.EditInventory();
            }
            catch
            {
                MessageBox.Show("Please select all required textboxes, if the problem persists reset the inventory file");
            }
            ExtraToppingList.Items.Clear();
            UpdateStuffedCrustBox();
            UpdateSizeBox();
            UpdatePizzaSelection();
            UpdateOrderList();
            UpdateOrderTotal();

        }
        private void CompleteOrderButton_Click(object sender, RoutedEventArgs e)
        {
            hasBeenCompleted = true;
            CompletedOrdersWindow.m_MasterOrderManager.Add(m_OrderManager);
            Close();
        }
        private void ExtraToppingList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ExtraToppingList.SelectedItem != null)
            {
                if (ExtraToppingList.SelectedItems.Count > 5)
                {
                    ExtraToppingList.SelectedItems[5] = null;
                }
            }
            UpdatePizzaCostBox();

        }
        private void PizzaSizecomboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(PizzacomboBox.SelectedItem != null)
            {
                FilterToppingsListBox(PizzacomboBox.SelectedItem.ToString());
            }
            UpdateStuffedCrustBox();
            UpdatePizzaCostBox();
        }
        private void hasStuffedCrust_Checked(object sender, RoutedEventArgs e)
        {

            UpdatePizzaCostBox();
        }
        private void BurgerAddToOrder_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if ((bool)QuaterRadioButton.IsChecked == false && (bool)HalfRadioButton.IsChecked == false)
                {
                    throw new Exception();
                }
                BurgerOrder order = new BurgerOrder(BurgercomboBox.SelectedItem.ToString(), float.Parse(BurgerPriceTextbox.Text.Substring(1)), (bool)hasCheese.IsChecked, (bool)hasSalad.IsChecked, (bool)QuaterRadioButton.IsChecked);
                m_OrderManager.AddOrder(order);
                order.EditInventory();
            }
            catch
            {
                MessageBox.Show("Please select all required textboxes, if the problem persists reset the inventory file");

            }
            UpdateBurgerSelection();
            UpdateBurgerSize();
            UpdateCheeseAndSaladBox();
            UpdateOrderList();
            UpdateOrderTotal();

        }
        private void RemoveItemButton_Click(object sender, RoutedEventArgs e)
        {
            if (OrderSummarylistBox.SelectedItem != null)
            {
                Order order = (Order)OrderSummarylistBox.SelectedItem;
                order.ReplenishInventory();
                m_OrderManager.RemoveOrder(order);

                Update();
                
            }
        }
  
        private void BurgercomboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateBurgerCostBox();
            UpdateBurgerSize();
            UpdateBurgerCostBox();

        }
        private void QuaterRadioButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateBurgerCostBox();
        }
        private void HalfRadioButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateBurgerCostBox();
        }
        private void hasSalad_Click(object sender, RoutedEventArgs e)
        {
            UpdateBurgerCostBox();
        }
        private void hasCheese_Click(object sender, RoutedEventArgs e)
        {
            UpdateBurgerCostBox();
        }
        private void SundriesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SundriesListBox.SelectedItem != null)
                SundriesPriceTextbox.Text = "£ " + Menu.findMenuPrice((string)SundriesListBox.SelectedItem, size.regular);
        }
        private void SundriesAddToOrderButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SundryOrder order = new SundryOrder((string)SundriesListBox.SelectedItem, float.Parse(SundriesPriceTextbox.Text.Substring(1)));
                m_OrderManager.AddOrder(order);
                order.EditInventory();
            }
            catch
            {
                MessageBox.Show("Please Select a Sundry");
            }


            UpdateOrderList();
            UpdateOrderTotal();
            UpdateSundrySelection();
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!hasBeenCompleted)
            {
                foreach (Order i in m_OrderManager.getOrders())
                {
                    i.ReplenishInventory();
                }
            }
        }
    }
}
