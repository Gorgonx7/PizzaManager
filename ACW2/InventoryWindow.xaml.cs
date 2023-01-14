
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;


namespace ACW2
{
    /// <summary>
    /// Interaction logic for InventoryWindow.xaml
    /// </summary>
    ///

    public partial class InventoryWindow : Window
    {
        ingredient m_CurrentIngredient; // keeps track of the current selected ingredient

        public InventoryWindow()
        {
            InitializeComponent();
            ResetListBox(); // Loads in the list of ingredients into the inventory box
            categoryComboBox.Items.Add("All"); // add all to the category combo box
            categoryComboBox.Items.Add("Pizza"); // add pizza to the category combo box
            categoryComboBox.Items.Add("Burger"); // add burger to the category combo box
            categoryComboBox.Items.Add("Sundry"); // add sundry to the category combo box
            categoryComboBox.SelectedValue = "All"; // selected all as the current filter
        }
        /// <summary>
        ///  resets the list box to the final 
        /// </summary>
        private void ResetListBox()
        {
            InventoryListBox.Items.Clear(); // clears all the items in the ingredient list box

            for (int x = 0; x < MainWindow.masterInventory.getInventory().Count; x++) // for each ingredient in the inventroy
            {
                ListBoxItem item = new ListBoxItem(); // create a new list box item
                item.Content = MainWindow.masterInventory.getInventory()[x]; // assigne the content of the item to the next ingredient 
                if (MainWindow.masterInventory.getInventory()[x].getNumberOfUnits() <= 100) // if a ingredient has less than 100 units left
                {
                    item.Background = Brushes.Yellow; // display it as yellow in the inventory listbox
                }
                if (MainWindow.masterInventory.getInventory()[x].getNumberOfUnits() <= 0) // if a ingredient has less than or 0 units left
                {
                    item.Background = Brushes.Red; // display it as red in the inventory list box
                }
                InventoryListBox.Items.Add(item); // add the item to the list box
            }
        }
        /// <summary>
        /// filter the list box dependent on the current catagory selected
        /// </summary>
        /// <param name="pFilter"></param>
        private void FilterListBox(type pFilter)
        {
            InventoryListBox.Items.Clear(); // clear the inventory list box
            for (int x = 0; x < MainWindow.masterInventory.getInventory().Count; x++) // for each ingredient in the inventory
            {
                if (MainWindow.masterInventory.getInventory()[x].getType() == pFilter) // if it's type is the same type as the filter
                {
                    ListBoxItem item = new ListBoxItem(); // create a new list box item
                    item.Content = MainWindow.masterInventory.getInventory()[x]; // asign the ingredient to the content of the item
                    if (MainWindow.masterInventory.getInventory()[x].getNumberOfUnits() <= 100) // if the ingredient has 100 or less units left 
                    {
                        item.Background = Brushes.Yellow; // change the colour of the item to yellow
                    }
                    if (MainWindow.masterInventory.getInventory()[x].getNumberOfUnits() <= 0) // if the ingredient has 0 or less
                    {
                        item.Background = Brushes.Red; // change the background to red
                    }
                    InventoryListBox.Items.Add(item); // add the item to the inventory list box
                }
            }
        }
        private void categoryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (categoryComboBox.SelectedIndex) // swich the currently selected catagory combo
            {
                case 0:
                    ResetListBox(); // if all is selected reset the list box
                    break;
                case 1:
                    FilterListBox(type.pizza); // if pizza is selected filter the pizza ingredients 
                    break;
                case 2:
                    FilterListBox(type.burger);// if burger is selected filter the burger ingredients
                    break;
                case 3:
                    FilterListBox(type.sundry);// if sundry is selected filter the sundry ingredients
                    break;
            }

        }
        private void InventoryListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (InventoryListBox.SelectedItem != null) // if the selected ingredient is not null
            {
                m_CurrentIngredient = ((InventoryListBox.SelectedItem as ListBoxItem).Content) as ingredient; // assign the ingredient to a holder ingredient 
            }

        }
        private void UpdateInventoryButton_Click(object sender, RoutedEventArgs e)
        {


            int findItem = MainWindow.masterInventory.getInventory().IndexOf(m_CurrentIngredient); // get the index of the selected ingredient in the master inventory

            try
            {
                if (float.Parse(UpdateStockTextbox.Text) < 0) // if the update text box contains a negative number 
                {
                    MessageBox.Show("Please Enter A Positive Number"); // show a feed back message to the user
                    return; // return to the top of the method
                }
                MainWindow.masterInventory.getInventory()[findItem].setNumberOfUnits(float.Parse(UpdateStockTextbox.Text)); // change the numbner of units to the contents of the update stock text box


            }
            catch
            {
                MessageBox.Show("Please enter a valid numerical string and/or select a valid ingredient"); // if an error is detected show feed back to the user
                UpdateStockTextbox.Text = ""; // reset the text box to nothing
                return; // return to the top of the method
            }
            InventoryListBox.SelectedItem = MainWindow.masterInventory.getInventory()[findItem]; // reselect the new item
            if (categoryComboBox.SelectedItem.ToString() == "All") // if all is selected in the combo box
            {
                ResetListBox(); // reset the list box
            }
            else
            {
                switch (categoryComboBox.SelectedItem.ToString()) // swich the selected category combo box item to see which one is selected
                {
                    case "Pizza": // if pizza
                        FilterListBox(type.pizza); // filter list box for pizza
                        break;
                    case "Burger": // if burger
                        FilterListBox(type.burger); // filter listbox for burger
                        break;
                    case "Sundry": // if sundry
                        FilterListBox(type.sundry); // filter list box for sundry
                        break;
                }
            }
        }
        private void UpdateStockTextbox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
