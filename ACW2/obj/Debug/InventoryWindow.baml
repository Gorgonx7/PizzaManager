   M S B A M L   `   `   `  ����?  ;ACW2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:ACW2ACW2  5   	       ACW2.InventoryWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   $localclr-namespace:ACW2     5   	     � Title$  InventoryWindow��5   	    � SizeToContent$ WidthAndHeight=�6!   .��5	      ��  �� Orientation$ Vertical=�6   $��Center=�6(   .+�5
   
   �� -   5         ��Name$ InventoryTitle��5
      $��	Inventory��6(   $
��Left=�6<   $��20,0,0,0q�6W   $	��Top=�6i   $��20(�6�   �� 5   
   $ 
Horizontal=�6   $��Center=�6.   .+�5      �� -   5        $ CategoryLable��5      $��Category��6+   $��5q�6>   �� 5      -   5         ��Name$ categoryComboBox��5      $��5q�61   $	��100��6<   6H   5      �� 5   
   $ Vertical=�6   $��Center=�6,   .+�5      �� -   5        $ KeyLable��5      $6��0Category, Name, Cost per Unit, Quantity in Stock��6&   $��
5, 5, 0, 0q�6a   $��0q�6u   $��9(�6�   $��Italic$�6�   �� 5      -   5         ��Name$ InventoryListBox��5      $	��200��60   $��
5, 0, 5, 5q�6=    �� SelectionMode$ Single=�6Q   6h   5      �� 5   
   $ 
Horizontal=�6   $��Right=�6.   .+�5      �� -   5        $ StockTextBox��5      $��Quantity in stock��6*   $��5q�6F   �� 5      -   5         ��Name$ UpdateStockTextbox��5      $��24��62   $��5q�6>    �� TextWrapping$
 Wrap=�6I   $�� ��6]   $��80��6e   6s   5      �� 5   
   -   5        	 ��Name$	 UpdateInventoryButton��5      $��Update��60   $��Right=�6A   $��5q�6]   $	��Top=�6h   $��80��6�   $��24��6�   6�   5      5      