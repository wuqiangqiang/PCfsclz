   M S B A M L   `   `   `  ����   ZRDFSSystem-    'FoodSafetyMonitoring.Manager.NewMessage   \ XPresentationFramework, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      8x,http://schemas.microsoft.com/winfx/2006/xaml    $	��510��$	��463��  � WindowStyle$
  None=� � WindowStartupLocation$ CenterScreen=�
������  � 
ResizeMode$ NoResize=�
������  � AllowsTransparency .  � 	Resources  �� MergedDictionaries �� Source��  �� $ Themes/StackPanel.xaml?��� $ Themes/TextBlock.xaml?��� $ Themes/Combobox.xaml?��� $ Themes/TextBox.xaml?��� $" Themes/ImageButtonStyle.xaml?�%    .��� ����  �� ImageSource$ ../res/sampling.png�� � RowDefinitions �� $��55 ��� �� $	��120 �.+�� $��
13,11,11,0q�.+�n� -   $	��440��$��40��
������ 	 J� Opacity$	 0Q�$
��Left=��� $��20��$	��160��$
��Left=�$��16(�$��Center=�
�������信息发布�� -   
 ��Name$

 exit��$��29��$��26��$��/res/close.png�� � ToolTip$ 关闭��$��Right=�$	��Top=�� -    �Name$ _grid_detail��$��	8,50,10,0q�$	��Top=�$��Center=�$��1�� �� $��37 ��� $	��120 �.+�� $��0��.+��� $��Center=�.+��� $��70��信息标题：�� -    ��Name$ _messagetitle��$	��250�� �� 	MaxLength$ 30��� $��1��.+��� $��Center=�.+��� $��70��信息内容：�� -   $ _messagecontent��$	��250��$	 100��$	��100�� �� TextWrapping$
 Wrap=�� $��2��$��
13,0,13,50q�.+��� -   $��btnSave��$	��120��$��32��   SendImageButtonTemplate#��[  