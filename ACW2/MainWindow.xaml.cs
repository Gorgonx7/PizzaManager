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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ACW2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    /*
     * Master To-Do list
     * 
     */
    public partial class MainWindow : Window
    {
        public static Inventory masterInventory = new Inventory(); // Master Inventory to keep track of ingredients that have been used
       
        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void inventoryButton_Click(object sender, RoutedEventArgs e)
        {
            InventoryWindow wnd = new InventoryWindow();
            wnd.ShowDialog();
        }

        private void foodMenuButton_Click(object sender, RoutedEventArgs e)
        {
            FoodMenuWindow wnd = new FoodMenuWindow();
            wnd.ShowDialog();
        }

        private void newOrderButton_Click(object sender, RoutedEventArgs e)
        {
            OrderWindow wnd = new OrderWindow();
            wnd.ShowDialog();
        }

        private void completedOrdersButton_Click(object sender, RoutedEventArgs e)
        {
            CompletedOrdersWindow wnd = new CompletedOrdersWindow();
            wnd.ShowDialog();
        }
        /// <summary>
        /// Method that allows user feedback from any class/file
        /// </summary>
        /// <param name="input"> The message that </param>
        public static void showMessage(string input)
        {
            MessageBox.Show(input); // Shows a message box to the user with the inputted message
        }
        private void quitButton_Click(object sender, RoutedEventArgs e)
        {
            //write to the inventory
            masterInventory.printInventory(); // prints the inventory to the file contained within the same folder as exe
            this.Close();
        }
    }
}
