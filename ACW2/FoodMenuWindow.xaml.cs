using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Interaction logic for FoodMenuWindow.xaml
    /// </summary>
    public partial class FoodMenuWindow : Window
    {
        Menu m_masterMenu; // declare the master menu for he menu window
        public FoodMenuWindow()
        {
            InitializeComponent();
            m_masterMenu = new Menu(); // initialise the master menu
            ItemCategoryComboBox.Items.Add("All"); // add all to the item catagory combo box
            ItemCategoryComboBox.Items.Add("Pizza"); // add Pizza to the item catagory combo box
            ItemCategoryComboBox.Items.Add("Burger"); // add Burger to the item catagory combo box
            ItemCategoryComboBox.Items.Add("Sundry"); // add Sundry to the item catagory combo box
            ItemCategoryComboBox.SelectedValue = "All"; // select all in for the item catagory combo box
            ItemSizeComboBox.Items.Add("All"); // add all to the item size combo box
            ItemSizeComboBox.Items.Add("Regular"); // add Regular to the item size combo box
            ItemSizeComboBox.Items.Add("Large"); // add Large to the item size combo box
            ItemSizeComboBox.Items.Add("Extra-Large"); // add Extra-large to the item size combo box
            ItemSizeComboBox.SelectedValue = "All"; // select all in the item size combo box
            ItemPriceTextBox.IsReadOnly = true; // declare item price text box to read only
            IngredientCostTextbox.IsReadOnly = true; // declare ingredient cost text box to be read only
            GrossProfitTextbox.IsReadOnly = true; // deckare gross profit text box as readonly 

            ReloadRecipeListBox(); // load in the recipes
        }
        /// <summary>
        /// Reloads the Recipe list into the table
        /// </summary>
        public void ReloadRecipeListBox()
        {
            MenuListBox.Items.Clear(); // clear the menu list box
            IngredientsListBox.Items.Clear(); // clear the ingredient list box
            List<Recipe> RecipeList = m_masterMenu.GetRecipes(); // get the list of recipes from the menu
            foreach (Recipe i in RecipeList) // for each recipe in the recipe list do...
            {
                ListBoxItem item = new ListBoxItem(); // create a new list box item
                item.Content = i; // add the recipe to the content of the item
                foreach (ingredient j in i.getIngredients()) // for each ingredient in the recipe...
                {
                    recipeIngredient Ingredient = j as recipeIngredient; // assigne it to a holder ingredient as a recipe ingredient
                    if (Ingredient != null) // if the ingredient is not equal to null
                    {
                        if (Ingredient.getNumberOfUnits() < 100) // if any ingredient has less than 100 units left
                        {
                            item.Background = Brushes.Yellow; // change the background to yellow
                        }

                        ingredient MasterIngredient = MainWindow.masterInventory.FindIngredient(Ingredient.getName()); // Find the normal ingreident in the master inventroy
                        if (Ingredient.getUsedPerRecipe() > MasterIngredient.getNumberOfUnits()) // compare the use rate of the recipe ingredient to the number of units in the master inventory
                        {
                            item.Background = Brushes.Red; // change the background to red
                            break; // breake to increase the efficency of this algorithm
                        }
                    }

                }
                MenuListBox.Items.Add(item); // add the recipe to the menu list
            }
        }
        /// <summary>
        /// Fills the ingredient box with the relivent ingredietns and coloruises them accordingly
        /// </summary>
        /// <param name="pIngredients"></param>
        private void fillIngredientsBox(List<ingredient> pIngredients)
        {
            IngredientsListBox.Items.Clear(); // clear the ingredients list box

            for (int x = 0; x < pIngredients.Count; x++) // for each ingredient...
            {
                ListBoxItem item = new ListBoxItem(); // create a new list box item
                item.Content = pIngredients[x]; // assign the ingredient to its content
                if (pIngredients[x].getNumberOfUnits() <= 100) // if the number of units that the unit has is less than 100..
                {
                    item.Background = Brushes.Yellow; // assign the background colour to yellow
                }
                if (pIngredients[x].getNumberOfUnits() <= 0) // if the number of units is less than or equal too 0...
                {
                    item.Background = Brushes.Red; // make the background red
                }
                //MainWindow.showMessage(pIngredients[x].ToString());

                IngredientsListBox.Items.Add(item); // add the ingredient ot the list box
            }
        }

        private void MenuListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MenuListBox.SelectedItem != null) // if the selected item is not equal to null
            {
                Recipe selectedRecipe = (Recipe)((MenuListBox.SelectedItem as ListBoxItem).Content); // assign the selected listbox item to as a recipe
                fillIngredientsBox(selectedRecipe.getIngredients()); // filter the ingredients list box with the selected recipe's ingredients
                // NEED TO CHECK THIS IS WORKING
                Debug.WriteLine("Plz fix line 90 foodmenuwindow"); 
                IngredientCostTextbox.Text = "£ " + MoneyManager.calculateIngredientCost(selectedRecipe.getIngredients()); // call the money manager to see calculate the cos of the ingredients
                ItemPriceTextBox.Text = "£ " + selectedRecipe.getPrice(); // get the price of the recipe
                GrossProfitTextbox.Text = "£ " + (selectedRecipe.getPrice() - (decimal)MoneyManager.calculateIngredientCost(selectedRecipe.getIngredients())); // get the gross profit of the recipe by using the two different relivent numbers
            }

        }
        /// <summary>
        /// Filters the menu list by type of recipe
        /// </summary>
        /// <param name="pFilter"></param>
        private void FilterMenuList(type pFilter)
        {
            ItemSizeComboBox.SelectedIndex = 0; // reset the item size combo box
            MenuListBox.Items.Clear(); // clear the menu list box
            IngredientsListBox.Items.Clear(); // clear the ingredient list box
            List<Recipe> RecipeList = m_masterMenu.GetRecipes(); // get the list of recipes from the menu
            foreach (Recipe i in RecipeList) // for each recipe in the recipe list do...
            {
                if (i.getType() == pFilter) // if the filter maches the type of recipe... 
                {
                    ListBoxItem item = new ListBoxItem(); // create a new list box item
                    item.Content = i; // add the recipe to the content of the item
                    foreach (ingredient j in i.getIngredients()) // for each ingredient in the recipe...
                    {
                        recipeIngredient Ingredient = j as recipeIngredient; // assigne it to a holder ingredient as a recipe ingredient
                        if (Ingredient != null) // if the ingredient is not equal to null
                        {
                            if (Ingredient.getNumberOfUnits() < 100) // if any ingredient has less than 100 units left
                            {
                                item.Background = Brushes.Yellow; // change the background to yellow
                            }

                            ingredient MasterIngredient = MainWindow.masterInventory.FindIngredient(Ingredient.getName()); // Find the normal ingreident in the master inventroy
                            if (Ingredient.getUsedPerRecipe() > MasterIngredient.getNumberOfUnits()) // compare the use rate of the recipe ingredient to the number of units in the master inventory
                            {
                                item.Background = Brushes.Red; // change the background to red
                                break; // breake to increase the efficency of this algorithm
                            }
                        }

                    }
                    MenuListBox.Items.Add(item); // add the recipe to the menu list
                }
            }

        }
        /// <summary>
        /// Filters the menu list by size of recipe
        /// </summary>
        /// <param name="pFilter"></param>
        private void FilterSizeList(size pFilter)
        {
            ItemCategoryComboBox.SelectedIndex = 0; // reset the item catagory combo box
            MenuListBox.Items.Clear(); // clear the menu list box
            IngredientsListBox.Items.Clear(); // clear the ingredient list box
            List<Recipe> RecipeList = m_masterMenu.GetRecipes(); // get the list of recipes from the menu
            foreach (Recipe i in RecipeList) // for each recipe in the recipe list do...
            {
                if (i.GetSize() == pFilter) // if the filter maches the size of recipe... 
                {
                    ListBoxItem item = new ListBoxItem(); // create a new list box item
                    item.Content = i; // add the recipe to the content of the item
                    foreach (ingredient j in i.getIngredients()) // for each ingredient in the recipe...
                    {
                        recipeIngredient Ingredient = j as recipeIngredient; // assigne it to a holder ingredient as a recipe ingredient
                        if (Ingredient != null) // if the ingredient is not equal to null
                        {
                            if (Ingredient.getNumberOfUnits() < 100) // if any ingredient has less than 100 units left
                            {
                                item.Background = Brushes.Yellow; // change the background to yellow
                            }

                            ingredient MasterIngredient = MainWindow.masterInventory.FindIngredient(Ingredient.getName()); // Find the normal ingreident in the master inventroy
                            if (Ingredient.getUsedPerRecipe() > MasterIngredient.getNumberOfUnits()) // compare the use rate of the recipe ingredient to the number of units in the master inventory
                            {
                                item.Background = Brushes.Red; // change the background to red
                                break; // breake to increase the efficency of this algorithm
                            }
                        }

                    }
                    MenuListBox.Items.Add(item); // add the recipe to the menu list
                }
            }
        }
        private void ItemCategoryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (ItemCategoryComboBox.SelectedIndex) // swich the selected combo box index...
            {
                case 0: // if all
                    ReloadRecipeListBox(); // reset the list box
                    break;
                case 1: // if Pizza
                    FilterMenuList(type.pizza); // filter for pizza recipes
                    break;
                case 2: // if Burger
                    FilterMenuList(type.burger); // filter for burger recipes
                    break;
                case 3: // if sundry
                    FilterMenuList(type.sundry); // filter for sundry recipes
                    break;
            }
        }

        private void ItemSizeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (ItemSizeComboBox.SelectedIndex) // switch the item size combo box selected index
            {
                case 0: // if all
                    ReloadRecipeListBox(); // reset list box
                    break;
                case 1: // if regualr
                    FilterSizeList(size.regular); // filter menu list box to regular recipes
                    break;
                case 2: // if large
                    FilterSizeList(size.large); // filter menu list box to large recipes
                    break;
                case 3: // if extra large
                    FilterSizeList(size.extralarge); // filter menu list box to extra large recipes
                    break;
            }
        }

        private void IngredientsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IngredientsListBox.SelectedItem != null) // if ingredient list box selected item is not equal to null
                IngredientsListBox.SelectedItem = null; // set the ingredient list box item to null
        }
    }
}
