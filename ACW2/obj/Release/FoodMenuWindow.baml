   M S B A M L   `   `   `  ���� ?  ;ACW2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:ACW2ACW2      ACW2.FoodMenuWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     $localclr-namespace:ACW2       � Title$  FoodMenuWindow�� � SizeToContent$ WidthAndHeight=�.����  �� Orientation$ 
Horizontal=�.+��� $ Vertical=�$��Bottom=�.+��� -    ��Name$ FoodItemLable��$��	Food Item��$
��Left=�$��5q�$	��Top=�$��20(��� $ 
Horizontal=�$��Right=�.+��� -   $ ItemCategorylabel��$��Item Category��$��5q��� -    ��Name$ ItemCategoryComboBox��$��5q�$	��120���� $ 
Horizontal=�$��Right=�.+��� -   $ ItemSizeLable��$��	Item Size��$��5q��� -   $ ItemSizeComboBox��$��5q�$	��120�� �� SelectedValue$	 All���� $ Vertical=�$��Right=�.+��� -   $ CatagoryTableKeyLable��$&�� Category, Name, Size, Price (£)��$��0,0,0,0q�$��Italic$�$��9(��� -    ��Name$ MenuListBox��$	��130��$��5q�$	��200���� $ Vertical=�$��Bottom=�.+��� -   $ IngredientsLable��$��Ingredients��$��5q�$	��Top=�$��16(��� -	   $ IngredientsTableKeyLable��$.��(Name, Portion (Units), Portion Cost (£)��$
��Left=�$��0q�$	��Top=�$��Italic$�$��9(��� -
   $ IngredientsListBox��$	��130��$��5q�$	��200���� $ Vertical=�$��Bottom=�.+��� -   $ SummaryLable��$��Summary��$��5q�$��16(��� 
�������$��1q�$	��130��$��5q�._��� $��Bottom=�.+��� $ 
Horizontal=�$��Right=�.+��� -   $ ItemPriceLable��$��
Item Price��$��5q��� -    ��Name$ ItemPriceTextBox��$��24��$��5q� �� TextWrapping$
 Wrap=�$��£��$��76���� $ 
Horizontal=�$��Right=�.+��� -   $ IngredientCostLable��$��Total Ingredient Cost��$��5q��� -   $ IngredientCostTextbox��$��24��$��5q�$
 Wrap=�$��£��$��76���� $ 
Horizontal=�$��Right=�.+��� -   $ GrossProfitLable��$��Gross Profit��$��5q��� -   $ GrossProfitTextbox��$��24��$��5q�$
 Wrap=�$��£��$��76��