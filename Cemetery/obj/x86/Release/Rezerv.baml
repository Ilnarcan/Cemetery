   M S B A M L   `   `   `  ����   Cemetery    Cemetery.Rezerv   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     -     � Title$-  'Захоронения в резерв ��$	��600��$
��1350�� � WindowStartupLocation$ CenterScreen=�$
��1350��$
��1350��$	��600��$	��600�� � Icon$( "/Cemetery;component/Images/App.ico�� � 
ResizeMode$ CanMinimize=� � DataContext ��  � 	Resources  �� Source�� $ Styles.xaml?�%    .��� 
�������.+��� -   $��TextBox2��$��32��$
��Left=�$��
261,91,0,0q�$	��Top=�$	��120��$��16(�$��Center=�$��2���� -   $��TextBox3��$��32��$
��Left=�$��1098,91,0,0q�$	��Top=�$	��151��$��16(�$��Center=�$��8��&   System.Windows.Controls.DataGrid  -   $��	DataGrid1��   AutoGenerateColumns . ���	#FFF6F6F6
�������   CanUserAddRows . 	   CanUserResizeRows	 .  $��15(�
   GridLinesVisibility$	
 All=�$	��237��$
��Left=�$��Center=�   HorizontalGridLinesBrush
 �����   
IsReadOnly . $��
30,137,0,0q�$	��Top=�$��Center=�$
��1269��   MinColumnWidth$ 30Q�   HeadersVisibility$	 All=�   VerticalGridLinesBrush
 �����   AlternatingRowBackground
 ������� -   $��Label4��$��18(�$
��Bold"�
���   �$��48��$	��Top=�$��12,0,0,0q�$
��Left=�$
��1048�� �� �� -   $��Button1��$ ��Редактировать��$��52��$
��Left=�$��127,446,0,0q�$	��Top=�$	��174��
������� � Cursor$
 Hand���� -   $��Button2��$��Удалить��$��52��$
��Left=�$��605,446,0,0q�$	��Top=�$	��169��
�������$
 Hand���� -   $��Button3��$��Печать��$��52��$
��Left=�$��1118,446,0,0q�$	��Top=�$	��147��
�������$
 Hand���� -	   $��TextBox5��$��23��$
��Left=�$��307,401,0,0q�$	��Top=�$	��120��$��14(�$��Right=�$��Center=��� -
   $��Label2��$��
ИТОГО��$��28��$
��Left=�$��201,396,0,0q�$	��Top=�$	��100��$��14(�
���   �$
��Bold"��� -   $��Label3��$��дата захор��$��27��$
��Left=�$��	30,58,0,0q�$	��Top=�$��93���� -   $��Label5��$��№ квит, дата��$��27��$
��Left=�$��
261,58,0,0q�$	��Top=�$��97���� -   $��Label6��$��Уч ряд место��$��27��$
��Left=�$��1098,58,0,0q�$	��Top=�$	��138���� -   $��TextBox4��$��16(�$��32��$
��Left=�$��
525,91,0,0q�$	��Top=�$��Center=�$	��141��$��4���� -   $��Label1��$%��содержание услуг��$��27��$
��Left=�$��
525,58,0,0q�$	��Top=�$	��116���� -   $��TextBox6��$��16(�$��32��$
��Left=�$��
672,91,0,0q�$	��Top=�$��Center=�$	��125��$��5���� -   $��Label7��$��ум ФИО��$��27��$
��Left=�$��
675,58,0,0q�$	��Top=�$��78���� -   $��TextBox7��$��16(�$��32��$��
803,91,0,0q�$	��Top=�$��Center=�$��6��$
��Left=�$��90���� -   $��Label8��$��№ договора��$��27��$
��Left=�$��
803,58,0,0q�$	��Top=�$��78���� -   $��Button4��$��+��$��42��$��1268,86,0,0q�$	��Top=�$
��Left=�$��48��$��30��
�������$
 Hand��(  "System.Windows.Controls.DatePicker  -   $��DatePicker1��   StyleCalendar   CalendarStyle# [  $
 Hand��$��LeftToRight=�$��32��$
��Left=�$��Center=�$��	30,91,0,0q�   SelectedDateFormat$
 Long=�$	��Top=�$��Center=�$	��209��$��1���� -   $��TextBox1��$��16(�$��32��$
��Left=�$��
398,91,0,0q�$��3��$	��Top=�$��Center=�$	��108���� -   $��Label9��$��
сумма��$��27��$
��Left=�$��
398,58,0,0q�$	��Top=�$��80���� -   $��Label10��$��дата сост��$��27��$
��Left=�$��
899,58,0,0q�$	��Top=�$��93��  -   $��DatePicker2��# [  $
 Hand��$��LeftToRight=�$��32��$
��Left=�$��Center=�$��
899,91,0,0q�$
 Long=�$	��Top=�$��Center=�$	��193��$��7��