   M S B A M L   `   `   `  ����   ZRDFSSystem4    .FoodSafetyMonitoring.Manager.NewOrModifyDevice   \ XPresentationFramework, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      8x,http://schemas.microsoft.com/winfx/2006/xaml    $	��510��$	��463��  � WindowStyle$
  None=� � WindowStartupLocation$ CenterScreen=�
������  � 
ResizeMode$ NoResize=�
������  � AllowsTransparency .  � 	Resources  �� MergedDictionaries �� Source��  �� $ Themes/StackPanel.xaml?��� $ Themes/TextBlock.xaml?��� $ Themes/Combobox.xaml?��� $ Themes/TextBox.xaml?��� $" Themes/ImageButtonStyle.xaml?�%    .��� ����  �� ImageSource$ ../res/sampling.png�� � RowDefinitions �� $��55 ��� �� $	��120 �.+�� $��
13,11,11,0q�.+�n� -   $	��440��$��40��
������ 	 J� Opacity$	 0Q�$
��Left=��� $��20��$	��160��$
��Left=�$��16(�$��Center=�
�������档案详细信息�� -   
 ��Name$

 exit��$��29��$��26��$��/res/close.png�� � ToolTip$ 关闭��$��Right=�$	��Top=�� -    �Name$ _grid_detail��$��	8,50,10,0q�$	��Top=�$��Center=�$��1�� �� $��37 ��� $��37 ��� $��37 ��� $��37 ��� $��37 �.+�� $��0��.+��� $��Center=�.+��� $��70��检测单位：�� -    ��Name$ 	_deptname��$	��250�� �� 	MaxLength$ 30��{�.  � $��1��.+��� $��Center=�.+��� $��70��来源单位：�� -   $ _companyname��$	��250��$	 100��� $��2��.+��� $��Center=�.+��� $��70��来源产地：�� -    ��Name$ 	_province��$��80�� �� Text$	 省��$��5,0,5,0q��� -   $ _city��$��80��$ 市(州)��$��0,0,5,0q��� -   $ _region��$��80��$ 区县��� $��3��.+��� $��Center=�.+��� $��70��负责人姓名：�� -	   $ 
_contacter��$	��250��$ 20��� $��4��.+��� $��Center=�.+��� $��70��	手机：�� -
   $ _phone��$	��250��$ 11�� �� IsInputMethodEnabled .  � $��2��$��
13,0,13,50q�.+��� -   $��btnSave��$	��120��$��32��   SaveImageButtonTemplate#��[  