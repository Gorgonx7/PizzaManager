   M S B A M L   `   `   `  ���� ?  ;ACW2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:ACW2ACW2      ACW2.OrderWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     $localclr-namespace:ACW2     -     � Title$  OrderWindow�� � SizeToContent$ WidthAndHeight=�.����  �� Orientation$ Vertical=�.+��� -    ��Name$ CreateOrderLable��$��Create Order��$
��Left=�$��5q�$	��Top=�$��24(��� $ 
Horizontal=�$	��Top=�.+��� $ Vertical=�$	��Top=�.+��� -   $ OrderSummaryLable��$��Order Summary��$��5q�$��16(��� -    ��Name$ OrderSummarylistBox��$	��160��$��5q�$	��180���� -    ��Name$ RemoveItemButton��$��Remove Item��$��5q�$��75���� $ 
Horizontal=�$��Right=�.+��� -   $ TotalIngredientCostLable��$��Ingredient Cost (£)��$��5q�$	��Top=��� -    ��Name$  TotalIngredientCostTextBox��$��24��$��5q� �� TextWrapping$
 Wrap=�$��£��$��80���� $ 
Horizontal=�$��Right=�.+��� -   $ NetProfitLable��$��Net Profit (£)��$��5q��� -	   $ NetProfitTextBox��$��24��$��5q�$
 Wrap=�$��£��$��80���� $ 
Horizontal=�$��Right=�.+��� -
   $ TotalPriceLable��$��Total Price(£)��$��5q��� -   $ TotalPriceTextbox��$��24��$��5q�$
 Wrap=�$��£��$��80���� -   $ CompleteOrderButton��$��Complete Order��$��5q��� $ Vertical=�$	��Top=�.+��� -   $ 
PizzaLable��$��	1. Pizzas��$��5q�$��16(��� 
�������$��1q�$��5q�._��� $ Vertical=�.+��� $ 
Horizontal=�$��Right=�.+��� -   $ label1��$��Pizza��$��5q��� -    ��Name$ PizzacomboBox��$��5q�$	��120���� $ 
Horizontal=�$��Right=�.+��� -   $ label��$
��Size��$��5q��� -   $ PizzaSizecomboBox1��$��5q�$	��120���� -   	 ��Name$	 hasStuffedCrust��$��Stuffed Crust��$��5q�$��Center=��� -   $ ExtraToppingsLable��$��Extra Toppings��$��5,5,5,0q�$��Center=��� -   $ ExtraToppingList��$	��120��$��5,0,5,5q�$	��140��$��Center=��� $ 
Horizontal=�$��Right=�.+��� -   $ PizzaPriceLable��$��
Price (£)��$��5q��� -   $ PizzaPriceTextBox��$��24��$��5q�$
 Wrap=�$��£��$	��Top=�$��80���� -   $ PizzaAddToOrder��$��Add to Order��$��5q�$��80��$��Right=��� $ Vertical=�$	��Top=�.+��� -   $ BurgerTitleLable��$��
2. Burgers��$��5q�$��16(��� 
�������$��1q�$��5q�._��� $ Vertical=�.+��� $ 
Horizontal=�$��Right=�.+��� -   $ label2��$��Burger��$
��Left=�$��5q�$	��Top=��� -   $ BurgercomboBox��$
��Left=�$��5q�$	��Top=�$	��107���� $ 
Horizontal=�$��Right=�.+��� -   $ label3��$
��Size��$��5q�$��Center=�	� -   
 	�Name$
 QuaterRadioButton��$��1/4lb��$
��Left=�$��5q�$��Center=�	� -   $
 HalfRadioButton��$��1/2lb��$
��Left=�$��5q�$��Center=��� $ Vertical=�$��Center=�.+��� -   $	 hasSalad��$��Salad��$��2q��� -   $	 	hasCheese��$��Cheese��$��2q��� $ 
Horizontal=�$��Right=�.+��� -    $ BurgerPriceLable��$��
Price (£)��$
��Left=�$��5q��� -!   $ BurgerPriceTextbox��$��24��$��5q�$
 Wrap=�$��£��$��80���� -"   $ BurgerAddToOrder��$��Add to Order��$��5q�$��80��$��Right=��� $ Vertical=�$	��Top=�.+��� -#   $ SundriesLable��$��3. Sundries��$��5q�$��16(��� 
�������$��1q�$��5q�._��� $ Vertical=�.+��� -$   $ SundriesListBox��$	��120��$	��140��$��5q��� $ 
Horizontal=�.+��� -%   $ SundriesPriceLablel��$��
Price (£)��$��5q�$	��Top=��� -&   $ SundriesPriceTextbox��$��24��$��5q�$
 Wrap=�$��£��$��80���� -'   $ SundriesAddToOrderButton��$��Add to Order��$��Right=�$��5q�$��80��