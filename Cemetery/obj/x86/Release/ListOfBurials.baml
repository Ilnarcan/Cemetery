   M S B A M L   `   `   `  ����   Cemetery    Cemetery.ListOfBurials   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     -     � Title$7  1Список захоронений, оплата��$	��600��$
��1000�� � WindowStartupLocation$ CenterScreen=�$
��1000��$
��1000��$	��600��$	��600�� � Icon$( "/Cemetery;component/Images/App.ico�� � 
ResizeMode$ CanMinimize=� � DataContext ��  � 	Resources  �� Source�� $ Styles.xaml?�%    .��� 
�������.+��� -   $��TextBox2��$��32��$
��Left=�$��
245,91,0,0q�$	��Top=�$��66��$��16(�$��Center=�$��2���� -   $��TextBox3��$��32��$
��Left=�$��
317,91,0,0q�$	��Top=�$��81��$��16(�$��Center=�$��3��&   System.Windows.Controls.DataGrid  -   $��	DataGrid1��   AutoGenerateColumns . ���	#FFF6F6F6
�������   CanUserAddRows . 	   CanUserResizeRows	 .  $��15(�
   GridLinesVisibility$	
 All=�$	��237��$
��Left=�$��Center=�   HorizontalGridLinesBrush
 �����   
IsReadOnly . $��
30,137,0,0q�$	��Top=�$��Center=�$	��905��   MinColumnWidth$ 30Q�   HeadersVisibility$	 All=�   VerticalGridLinesBrush
 �����   AlternatingRowBackground
 ������� -   $��Label4��$��18(�$
��Bold"�
���   �$��48��$
��Left=�$	��Top=�$	��816��$��12,0,0,0q� �� �� -   $��Button1��$ ��Редактировать��$��52��$
��Left=�$��127,446,0,0q�$	��Top=�$	��174��
������� � Cursor$
 Hand���� -   $��Button2��$��Удалить��$��52��$
��Left=�$��457,446,0,0q�$	��Top=�$	��169��
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
��Bold"��� -   $��Label3��$��дата оплаты��$��27��$
��Left=�$��	30,58,0,0q�$	��Top=�$��93���� -   $��Label5��$��№ заказа��$��27��$
��Left=�$��
245,58,0,0q�$	��Top=�$��80���� -   $��Label6��$��№ квитанции��$��27��$
��Left=�$��
317,58,0,0q�$	��Top=�$��87���� -   $��TextBox4��$��16(�$��32��$
��Left=�$��
415,91,0,0q�$	��Top=�$��Center=�$	��107��$��4���� -   $��Label1��$%��стоимость услуги��$��27��$
��Left=�$��
415,58,0,0q�$	��Top=�$	��107���� -   $��TextBox6��$��16(�$��32��$��550,91,261,0q�$	��Top=�$��Center=�$��5���� -   $��Label7��$��ФИО умершего��$��27��$
��Left=�$��
550,58,0,0q�$	��Top=�$	��167���� -   $��TextBox7��$��16(�$��32��$
��Left=�$��
751,91,0,0q�$	��Top=�$��Center=�$	��151��$��6���� -   $��Button4��$��+��$��42��$��
918,81,0,0q�$	��Top=�$
��Left=�$��48��$��30��
�������$
 Hand��(  "System.Windows.Controls.DatePicker  -   $��DatePicker1��   StyleCalendar   CalendarStyle# [  $
 Hand��$��LeftToRight=�$��32��$
��Left=�$��Center=�$��	30,91,0,0q�   SelectedDateFormat$
 Long=�$	��Top=�$��Center=�$	��198���� -   $��Label8��$��землекоп��$��27��$
��Left=�$��
751,58,0,0q�$	��Top=�$	��111��