﻿#pragma checksum "..\..\InventoryWindow.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "C73E6D1D1321F381F21CE39422610B69"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using ACW2;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace ACW2 {
    
    
    /// <summary>
    /// InventoryWindow
    /// </summary>
    public partial class InventoryWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 10 "..\..\InventoryWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label InventoryTitle;
        
        #line default
        #line hidden
        
        
        #line 12 "..\..\InventoryWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label CategoryLable;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\InventoryWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox categoryComboBox;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\InventoryWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label KeyLable;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\InventoryWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox InventoryListBox;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\InventoryWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label StockTextBox;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\InventoryWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox UpdateStockTextbox;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\InventoryWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button UpdateInventoryButton;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/ACW2;component/inventorywindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\InventoryWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.InventoryTitle = ((System.Windows.Controls.Label)(target));
            return;
            case 2:
            this.CategoryLable = ((System.Windows.Controls.Label)(target));
            return;
            case 3:
            this.categoryComboBox = ((System.Windows.Controls.ComboBox)(target));
            
            #line 13 "..\..\InventoryWindow.xaml"
            this.categoryComboBox.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.categoryComboBox_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            this.KeyLable = ((System.Windows.Controls.Label)(target));
            return;
            case 5:
            this.InventoryListBox = ((System.Windows.Controls.ListBox)(target));
            
            #line 17 "..\..\InventoryWindow.xaml"
            this.InventoryListBox.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.InventoryListBox_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 6:
            this.StockTextBox = ((System.Windows.Controls.Label)(target));
            return;
            case 7:
            this.UpdateStockTextbox = ((System.Windows.Controls.TextBox)(target));
            
            #line 21 "..\..\InventoryWindow.xaml"
            this.UpdateStockTextbox.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.UpdateStockTextbox_TextChanged);
            
            #line default
            #line hidden
            return;
            case 8:
            this.UpdateInventoryButton = ((System.Windows.Controls.Button)(target));
            
            #line 23 "..\..\InventoryWindow.xaml"
            this.UpdateInventoryButton.Click += new System.Windows.RoutedEventHandler(this.UpdateInventoryButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

