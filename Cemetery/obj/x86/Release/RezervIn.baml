   M S B A M L   `   `   `  ����   Cemetery    Cemetery.RezervIn   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     -     � Title$  Резервы ��$	��600��$
��1350�� � WindowStartupLocation$ CenterScreen=�$
��1350��$
��1350��$	��600��$	��600�� � Icon$( "/Cemetery;component/Images/App.ico�� � 
ResizeMode$ CanMinimize=� � DataContext ��  � 	Resources  �� Source�� $ Styles.xaml?�%    .��� 
�������.+��� -   $��TextBox2��$��32��$
��Left=�$��
245,91,0,0q�$	��Top=�$	��132��$��16(�$��Center=�$��2��&   System.Windows.Controls.DataGrid  -   $��	DataGrid1��   AutoGenerateColumns . ���	#FFF6F6F6
�������   CanUserAddRows . 	   CanUserResizeRows	 .  $��15(�
   GridLinesVisibility$	
 All=�$	��237��$
��Left=�$��Center=�   HorizontalGridLinesBrush
 �����   
IsReadOnly . $��
30,137,0,0q�$	��Top=�$��Center=�$
��1269��   MinColumnWidth$ 30Q�   HeadersVisibility$	 All=�   VerticalGridLinesBrush
 �����   AlternatingRowBackground
 ������� -   $��Label4��$��18(�$
��Bold"�
���   �$��48��$	��Top=�$��12,0,0,0q�$
��Left=�$
��1048�� �� �� -   $��Button1��$ ��Редактировать��$��52��$
��Left=�$��127,446,0,0q�$	��Top=�$	��174��
������� � Cursor$
 Hand���� -   $��Button2��$��Удалить��$��52��$
��Left=�$��605,446,0,0q�$	��Top=�$	��169��
�������$
 Hand���� -   $��Button3��$��Печать��$��52��$
��Left=�$��1118,446,0,0q�$	��Top=�$	��147��
�������$
 Hand���� -   $��TextBox5��$��23��$
��Left=�$��661,386,0,0q�$	��Top=�$	��120��$��14(�$��Right=�$��Center=��� -	   $��Label2��$��ИТОГО СУММА��$��28��$
��Left=�$��528,380,0,0q�$	��Top=�$	��128��$��14(�
���   �$
��Bold"��� -
   $��Label3��$��дата��$��27��$
��Left=�$��	30,58,0,0q�$	��Top=�$��93���� -   $��Label5��$��ФИО заказчика��$��27��$
��Left=�$��
245,58,0,0q�$	��Top=�$��97���� -   $��TextBox4��$��16(�$��32��$
��Left=�$��
497,91,0,0q�$	��Top=�$��Center=�$��65��$��4���� -   $��Label1��$��уч-к��$��27��$
��Left=�$��
497,58,0,0q�$	��Top=�$��65���� -   $��TextBox6��$��16(�$��32��$
��Left=�$��
568,91,0,0q�$	��Top=�$��Center=�$��50��$��5���� -   $��Label7��$��ряд��$��27��$
��Left=�$��
568,59,0,0q�$	��Top=�$��50���� -   $��TextBox7��$��16(�$��32��$��
624,91,0,0q�$	��Top=�$��Center=�$��6��$
��Left=�$��45���� -   $��Label8��$��
место��$��27��$
��Left=�$��
624,58,0,0q�$	��Top=�$��46���� -   $��Button4��$��+��$��42��$��1268,86,0,0q�$	��Top=�$
��Left=�$��48��$��30��
�������$
 Hand��(  "System.Windows.Controls.DatePicker  -   $��DatePicker1��   StyleCalendar   CalendarStyle# [  $
 Hand��$��LeftToRight=�$��32��$
��Left=�$��Center=�$��	30,91,0,0q�   SelectedDateFormat$
 Long=�$	��Top=�$��Center=�$	��209��$��1���� -   $��TextBox1��$��16(�$��32��$
��Left=�$��
383,91,0,0q�$��3��$	��Top=�$��Center=�$	��108���� -   $��Label9��$��телефон ��$��27��$
��Left=�$��
383,58,0,0q�$	��Top=�$��80���� -   $��TextBox8��$��16(�$��32��$
��Left=�$��
684,91,0,0q�$��7��$	��Top=�$��Center=�$	��106���� -   $��Label10��$��стоимость��$��27��$
��Left=�$��
684,58,0,0q�$	��Top=�$��78���� -   $��TextBox9��$��16(�$��32��$
��Left=�$��1011,91,0,0q�$��9��$	��Top=�$��Center=�$	��100���� -   $��Label11��$��
сумма��$��27��$
��Left=�$��1011,58,0,0q�$	��Top=�$��87���� -   $��	TextBox10��$��16(�$��32��$
��Left=�$��1117,91,0,0q�$��10��$	��Top=�$��Center=�$	��136���� -   $��Label12��$��примечание��$��27��$��1117,58,0,0q�$	��Top=�$
��Left=�$	��136���� -   $��Label6��$��оплачено��$��27��$
��Left=�$��
796,58,0,0q�$	��Top=�$��93��  -   $��DatePicker2��# [  $
 Hand��$��LeftToRight=�$��32��$
��Left=�$��Center=�$��
796,91,0,0q�$
 Long=�$��8��$	��Top=�$��Center=�$	��209��