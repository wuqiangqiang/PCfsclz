   M S B A M L   `   `   `  ����   ZRDFSSystem/    )FoodSafetyMonitoring.Manager.SetTaskGrade   \ XPresentationFramework, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      8x,http://schemas.microsoft.com/winfx/2006/xaml    $	��354��$	��194��  � WindowStyle$
  None=� � WindowStartupLocation$ CenterScreen=�
������  � 
ResizeMode$ NoResize=�
������  � AllowsTransparency .  � 	Resources  �� MergedDictionaries �� Source��  �� $ Themes/TextBlock.xaml?��� $ Themes/TextBox.xaml?�%    .��� ����  �� ImageSource$ ../res/details_small.png�� � RowDefinitions �� $��55 ��� .+�� $��0��$��
10,20,20,0q�$��35��.+�n� -   $	��290��$��35��
������ 	 J� Opacity$	 0Q�$
��Left=��� $��20��$	��150��$
��Left=�$��16(�$��Center=�
�������参数重置�� -   
 ��Name$

 exit��$��29��$��26��$��/res/close.png�� � ToolTip$ 关闭��$��Right=�$��Center=�� $��1�� �� $��60 ��� $��20 ��� $��40 �.+�� $��0��.+��� .+��� -    ��Name$ _grade_down��$��60�� �� Left$ 60��
 �� Top$ 20�� �� IsInputMethodEnabled .   �� 	MaxLength$ 3���� $��黑体)�$��16(�$��%��$��20��$	 145��$ 23��$��0q��� $��宋体)�$��16(�$��-��$��20��$��Center=�$	 175��$ 23��$��0q��� -   $ 	_grade_up��$��60��$	 205��$ 20�� .  $ 3���� $��宋体)�$��16(�$��%��$��20��$��Center=�$	 290��$ 23��$��0q��� -    ��Name$ _txtmsg��$��1��$��宋体)�$��14(�$	��200��
���  ��$��Center=��� -   $��btnSave��$��2��$��80��$��28��.��确定