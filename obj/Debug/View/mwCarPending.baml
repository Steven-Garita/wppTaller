   M S B A M L   `   `   `  ����E  Awpp_Taller, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:wpp_Taller.Viewwpp_Taller.View  5   	   "    wpp_Taller.View.mwCarPending   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:wpp_Taller.View     5   	     � Title$  mwCarPending��5   	   $	��900��6   $
��1260��6+    � WindowStyle$
 None=�68   .��5	      � .+�5
   
   &   System.Windows.Controls.DataGrid  $��	0,189,0,0q�6   
�������
���   �$��15(�6M      GridLinesVisibility$	 All=�6[      HorizontalGridLinesBrush
 �   �   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Marca��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$	 150 64     5      $ Modelo��6%   $	 150 65     5      $ Detalles��6%   $
  200 67     5      $ Reparación��6%   $	 250 69     5      $ Fecha Ingreso��6%   $	 150 6<     5      $ Estado��6%   $ 75 65     5      $ Cliente��6%   $	 200 66     5      $ Retirar��6%   $	 100 66   5      �� 5   
   $��Vehículos Pendientes��6   $
��Left=�6/   $��
218,63,0,0q�6J   $	��Top=�6^   $��60��6v   $	��530��6�   ���	#FFC50000$��35(�6�   ���	#00000000$
��Bold"�6�   �� 5   
   -   5         ��Name$ 	btnAddCar��5   b    � Cursor$
 Hand��6   $��
0,63,279,0q�6u   $��68��6�   $��60��6�   $	��Top=�6�   $��Right=�6�   ��6   Z� ��65   Z� ��6L   Z� ��5      �� 5      	 �� 
TargetType"	 ��6   .�5      �� 
 �� Property
 � �� �� Value �	#FF17151563    �� Triggers 5      P� 5       P� Property �@J�IsMouseOver P� Value . . �5      �� 
 � �� �	#FF2924236;   5      5       ��5"      �� 5#       �� 
TargetType" ��6"   .R�5$      �� $��50��6   $��50��6(    �� CornerRadius$ 9��5%      #��z��5&      ._�5'      �� $��Center=�6+   $��Center=�5(      5)      5*      .��5,      �� $��/Images/add.png��6   $��50��6-   $��50��69   5-      5/      50      