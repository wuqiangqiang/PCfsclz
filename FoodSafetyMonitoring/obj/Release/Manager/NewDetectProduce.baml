   M S B A M L   `   `   `  ����   ZRDFSSystem3    -FoodSafetyMonitoring.Manager.NewDetectProduce   \ XPresentationFramework, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      8x,http://schemas.microsoft.com/winfx/2006/xaml    $	��906��$	��479��  � WindowStyle$
  None=� � WindowStartupLocation$ CenterScreen=�
������  � 
ResizeMode$ NoResize=�
������  � AllowsTransparency .  � 	Resources  �� MergedDictionaries �� Source��  �� $" Themes/ImageButtonStyle.xaml?��� $ Themes/TextBox.xaml?��� $ Themes/PasswordBox.xaml?��� $ Themes/Button_new.xaml?��� $ Themes/StackPanel.xaml?��� $ Themes/ComboBox.xaml?��� $ Themes/TextBlock.xaml?��� $ Themes/CheckBox.xaml?�%    .��� .+�� $��Center=�$��Center=�$	��906��$	��479������  �� ImageSource$ /res/detect_back.png�� j� Stretch$
 Fill=�	 � RowDefinitions	 �� $��40 ��� .+�� .+�n� -   $	��860��$��40��
������ 
 J� Opacity$
 0Q�$
��Left=��� -    ��Name$
 exit��$��29��$��26��$��/res/close.png�� � ToolTip$ 关闭��$��Right=�$	��Top=�$��	0,12,15,0q�� $��1��$��
30,0,50,30q�	 �� $��40 ��� $��50 ��� $��50 ��� $��50 ��� $��50 ��� $��50 ��� $��80 �.+�� $��0��.+��� $��20(�$$��农产品检测信息录入单��$��Center=�$��Center=�$��0q�� $��1��.+��� .+��� $��70�� J� SnapsToDevicePixels .  来源产地:�� -    ��Name$ 	_province��$	��100���� $��20��$��10,0,0,0q�省�� -   $ _city��$	��100���� $��20��$��10,0,0,0q�市�� -   $ _region��$	��100���� $��10,0,0,0q�区（县）� $��2��.+���  �� Orientation$ 
Horizontal=�.+��� $��70��被检单位:�� -   $ _source_company��$	��360�� �� 
IsEditable . � $��3��.+��� .+��� $��70��检测项目:�� -   $ _detect_item��$	��100��$��0,0,0,0q��� $��60��检测方法:�� -   $ _detect_method��$	��230��$��0,0,0,0q��� $��60��检测对象:�� -	   $ _detect_object��$	��210��$��0q�{�.  � $��4��.+��� .+��� $��70��检测样本:�� -
   $ _detect_sample��$	��100��{�.  �� $��60��
检测值:�� -    ��Name$ _detect_value��$��0,0,0,0q� �� 	MaxLength$ 10��$	��230�� �� IsInputMethodEnabled .  �� $��60��检测结果:�� -   $ _detect_result��$	��210��� $��5��.+��� .+��� $��70��
检测师:�� -   $ _detect_person��$��0,0,0,0q�$	��100��{�.  �� $��60��检测单位:�� -   $ _detect_site��$��0,0,0,0q�$	��230��{�.  �� $��60��录入时间:�� -   $ _entering_datetime��$��0,0,0,0q�$	��210��{�.  � $��6��.+��� $��1��$ 
Horizontal=�$��Center=�.+��� -   $��btnSave��$	��120��$��32��   SaveImageButtonTemplate#��[  $��	30,0,20,0q��� -   $��	btnCancel��$	��120��$��32��  CancelImageButtonTemplate#��[ $��20,0,0,0q�