   M S B A M L   `   `   `  ����   Cemetery     Cemetery.CancelOfMaterials   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     -     � Title$H  BСписание материалов в производство ��$	��600��$
��1200�� � WindowStartupLocation$ CenterScreen=�$
��1200��$
��1200��$	��600��$	��600�� � Icon$( "/Cemetery;component/Images/App.ico�� � 
ResizeMode$ CanMinimize=� � DataContext ��  � 	Resources  �� Source�� $ Styles.xaml?�%    .��� 
�������.+��� -   $��TextBox2��$��32��$
��Left=�$��
234,91,0,0q�$	��Top=�$��97��$��16(�$��Center=�$��2���� -   $��TextBox3��$��32��$
��Left=�$��
337,91,0,0q�$	��Top=�$	��125��$��16(�$��Center=�$��3��&   System.Windows.Controls.DataGrid  -   $��	DataGrid1��   AutoGenerateColumns . ���	#FFF6F6F6
�������   CanUserAddRows . 	   CanUserResizeRows	 .  $��15(�
   GridLinesVisibility$	
 All=�$	��237��$
��Left=�$��Center=�   HorizontalGridLinesBrush
 �����   
IsReadOnly . $��
30,137,0,0q�$	��Top=�$��Center=�$
��1120��   MinColumnWidth$ 30Q�   HeadersVisibility$	 All=�   VerticalGridLinesBrush
 �����   AlternatingRowBackground
 ������� -   $��Label4��$��18(�$
��Bold"�
���   �$��48��$	��Top=�$��12,0,0,0q�$
��Left=�$
��1048�� �� �� -   $��Button1��$ ��Редактировать��$��52��$
��Left=�$��127,446,0,0q�$	��Top=�$	��174��
������� � Cursor$
 Hand���� -   $��Button2��$��Удалить��$��52��$
��Left=�$��522,446,0,0q�$	��Top=�$	��169��
�������$
 Hand���� -   $��Button3��$��Печать��$��52��$
��Left=�$��933,446,0,0q�$	��Top=�$	��147��
�������$
 Hand���� -	   $��TextBox5��$��23��$
��Left=�$��1030,400,0,0q�$	��Top=�$	��120��$��14(�$��Right=�$��Center=��� -
   $��Label2��$��
ИТОГО��$��28��$
��Left=�$��924,395,0,0q�$	��Top=�$	��100��$��14(�
���   �$
��Bold"��� -   $��Label3��$��дата��$��27��$
��Left=�$��	30,58,0,0q�$	��Top=�$��93���� -   $��Label5��$��№ квит��$��27��$
��Left=�$��
234,58,0,0q�$	��Top=�$��97���� -   $��Label6��$��наименование��$��27��$
��Left=�$��
345,58,0,0q�$	��Top=�$	��138���� -   $��TextBox4��$��16(�$��32��$
��Left=�$��
482,91,0,0q�$	��Top=�$��Center=�$	��123��$��4���� -   $��Label1��$��полировка��$��27��$
��Left=�$��
489,58,0,0q�$	��Top=�$��79���� -   $��TextBox6��$��16(�$��32��$
��Left=�$��
611,91,0,0q�$	��Top=�$��Center=�$��89��$��5���� -   $��Label7��$��размер��$��27��$
��Left=�$��
611,58,0,0q�$	��Top=�$��78���� -   $��TextBox7��$��16(�$��32��$��
716,91,0,0q�$	��Top=�$��Center=�$��6��$
��Left=�$��78���� -   $��Label8��$��количество��$��27��$
��Left=�$��
716,58,0,0q�$	��Top=�$��78���� -   $��Button4��$��+��$��42��$��1118,81,0,0q�$	��Top=�$
��Left=�$��48��$��30��
�������$
 Hand��(  "System.Windows.Controls.DatePicker  -   $��DatePicker1��   StyleCalendar   CalendarStyle# [  $
 Hand��$��LeftToRight=�$��32��$
��Left=�$��Center=�$��	30,91,0,0q�   SelectedDateFormat$
 Long=�$	��Top=�$��Center=�$	��198���� -   $��TextBox1��$��16(�$��32��$
��Left=�$��
816,91,0,0q�$��7��$	��Top=�$��Center=�$��94���� -   $��Label9��$��стоимость��$��27��$
��Left=�$��
820,58,0,0q�$	��Top=�$��80���� -   $��TextBox8��$��16(�$��32��$
��Left=�$��
932,91,0,0q�$��7��$	��Top=�$��Center=�$	��148���� -   $��Label10��$+��%ум.Ф.И.О.,место захор.��$��27��$
��Left=�$��
932,58,0,0q�$	��Top=�$	��148��