   M S B A M L   `   `   `  ����   Cemetery    Cemetery.ComeApp   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     -     � Title$:  4Приход материальных средств��$	��600��$
��1000�� � WindowStartupLocation$ CenterScreen=�$
��1000��$
��1000��$	��600��$	��600�� � Icon$( "/Cemetery;component/Images/App.ico�� � 
ResizeMode$ CanMinimize=� � DataContext ��  � 	Resources  �� Source�� $ Styles.xaml?�%    .��� 
�������.+��� -   $��TextBox2��$��32��$
��Left=�$��
249,91,0,0q�$	��Top=�$	��125��$��16(�$��Center=�$��2���� -   $��TextBox3��$��32��$
��Left=�$��
394,91,0,0q�$	��Top=�$	��146��$��16(�$��Center=�$��3��&   System.Windows.Controls.DataGrid  -   $��	DataGrid1��   AutoGenerateColumns . ���	#FFF6F6F6
�������   CanUserAddRows . 	   CanUserResizeRows	 .  $��15(�
   GridLinesVisibility$	
 All=�$	��237��$
��Left=�$��Center=�   HorizontalGridLinesBrush
 �����   
IsReadOnly . $��
34,137,0,0q�$	��Top=�$��Center=�$	��901��   MinColumnWidth$ 30Q�   HeadersVisibility$	 All=�   VerticalGridLinesBrush
 �����   AlternatingRowBackground
 ������� -   $��Label4��$��18(�$
��Bold"�
���   �$��48��$
��Left=�$	��Top=�$	��816��$��12,0,0,0q� �� �� -   $��Button1��$ ��Редактировать��$��52��$
��Left=�$��127,446,0,0q�$	��Top=�$	��174��
������� � Cursor$
 Hand���� -   $��Button2��$��Удалить��$��52��$
��Left=�$��447,446,0,0q�$	��Top=�$	��169��
�������$
 Hand���� -   $��Button3��$��Печать��$��52��$
��Left=�$��773,446,0,0q�$	��Top=�$	��147��
�������$
 Hand���� -	   $��TextBox5��$��23��$
��Left=�$��814,395,0,0q�$	��Top=�$	��120��$��14(�$��Right=�$��Center=��� -
   $��Label2��$��
ИТОГО��$��28��$
��Left=�$��708,390,0,0q�$	��Top=�$	��100��$��14(�
���   �$
��Bold"��� -   $��Label3��$��дата��$��27��$
��Left=�$��	34,58,0,0q�$	��Top=�$��93���� -   $��Label5��$��поставщик��$��27��$
��Left=�$��
249,58,0,0q�$	��Top=�$��97���� -   $��Label6��$��наименование��$��27��$
��Left=�$��
394,58,0,0q�$	��Top=�$	��138���� -   $��TextBox4��$��16(�$��32��$
��Left=�$��
558,91,0,0q�$	��Top=�$��Center=�$��58��$��4���� -   $��Label1��$��ед. изм.��$��27��$
��Left=�$��
558,58,0,0q�$	��Top=�$��58���� -   $��TextBox6��$��16(�$��32��$
��Left=�$��
622,91,0,0q�$	��Top=�$��Center=�$��78��$��5���� -   $��Label7��$��количество��$��27��$
��Left=�$��
622,58,0,0q�$	��Top=�$��78���� -   $��TextBox7��$��16(�$��32��$
��Left=�$��
713,91,0,0q�$	��Top=�$��Center=�$��78��$��6���� -   $��Label8��$��
сумма��$��27��$
��Left=�$��
713,58,0,0q�$	��Top=�$��57���� -   $��Button4��$��+��$��42��$��
918,81,0,0q�$	��Top=�$
��Left=�$��48��$��30��
�������$
 Hand��(  "System.Windows.Controls.DatePicker  -   $��DatePicker1��   StyleCalendar   CalendarStyle# [  $
 Hand��$��LeftToRight=�$��32��$
��Left=�$��Center=�$��	34,91,0,0q�   SelectedDateFormat$
 Long=�$	��Top=�$��Center=�$	��198���� -   $��TextBox1��$��16(�$��32��$
��Left=�$��
797,91,0,0q�$��7��$	��Top=�$��Center=�$��96���� -   $��Label9��$��
итого��$��27��$
��Left=�$��
797,58,0,0q�$	��Top=�$��57��