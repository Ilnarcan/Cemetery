   M S B A M L   `   `   `  ����   Cemetery    Cemetery.Szp   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     -     � Title$C  =Расход по зарплате и премиальных ��$	��600��$
��1200�� � WindowStartupLocation$ CenterScreen=�$
��1200��$
��1200��$	��600��$	��600�� � Icon$( "/Cemetery;component/Images/App.ico�� � 
ResizeMode$ CanMinimize=� � DataContext ��  � 	Resources  �� Source�� $ Styles.xaml?�%    .��� 
�������.+��� -   $��TextBox2��$��32��$
��Left=�$��
234,91,0,0q�$	��Top=�$	��121��$��16(�$��Center=�$��2���� -   $��TextBox3��$��32��$��
374,91,0,0q�$	��Top=�$��16(�$��Center=�$��3��$
��Left=�$	��152��&   System.Windows.Controls.DataGrid  -   $��	DataGrid1��   AutoGenerateColumns . ���	#FFF6F6F6
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
��Left=�$��126,497,0,0q�$	��Top=�$	��174��
������� � Cursor$
 Hand���� -   $��Button2��$��Удалить��$��52��$
��Left=�$��522,497,0,0q�$	��Top=�$	��169��
�������$
 Hand���� -   $��Button3��$��Печать��$��52��$
��Left=�$��946,497,0,0q�$	��Top=�$	��147��
�������$
 Hand���� -	   $��TextBox5��$��23��$
��Left=�$��108,429,0,0q�$	��Top=�$	��140��$��14(�$��Right=�$��Center=��� -
   $��Label2��$��ИТОГО Аванс��$��28��$
��Left=�$��108,394,0,0q�$	��Top=�$	��140��$��14(�
���   �$
��Bold"��� -   $��Label3��$��дата��$��27��$
��Left=�$��	30,58,0,0q�$	��Top=�$��93���� -   $��Label5��$��наименование��$��27��$
��Left=�$��
234,58,0,0q�$	��Top=�$	��121���� -   $��Label6��$��
аванс��$��27��$
��Left=�$��
374,58,0,0q�$	��Top=�$	��152���� -   $��TextBox4��$��16(�$��32��$
��Left=�$��
559,91,0,0q�$	��Top=�$��Center=�$	��149��$��4���� -   $��Label1��$��зарплата ��$��27��$
��Left=�$��
559,58,0,0q�$	��Top=�$	��149���� -   $��TextBox7��$��16(�$��32��$��
734,91,0,0q�$	��Top=�$��Center=�$��6��$
��Left=�$	��157���� -   $��Label8��$#��премия мастерам��$��27��$
��Left=�$��
734,58,0,0q�$	��Top=�$	��157���� -   $��Button4��$��+��$��42��$��1118,81,0,0q�$	��Top=�$
��Left=�$��48��$��30��
�������$
 Hand��(  "System.Windows.Controls.DatePicker  -   $��DatePicker1��   StyleCalendar   CalendarStyle# [  $
 Hand��$��LeftToRight=�$��32��$
��Left=�$��Center=�$��	30,91,0,0q�   SelectedDateFormat$
 Long=�$	��Top=�$��Center=�$	��198��$��1���� -   $��TextBox8��$��16(�$��32��$
��Left=�$��
924,91,0,0q�$��8��$	��Top=�$��Center=�$	��156���� -   $��Label10��$��примечание��$
��Left=�$��
924,58,0,0q�$	��148��$��27��$	��Top=��� -   $��TextBox9��$��14(�$��23��$
��Left=�$��Right=�$��273,429,0,0q�$	��Top=�$��Center=�$	��146���� -   $��Label11��$!��ИТОГО Зарплата��$��14(�$
��Bold"�
���   �$��28��$
��Left=�$��273,394,0,0q�$	��Top=�$	��146���� -   $��	TextBox10��$��14(�$��23��$
��Left=�$��Right=�$��438,429,0,0q�$	��Top=�$��Center=�$	��143���� -   $��Label12��$��ИТОГО Ав + Зп��$��14(�$
��Bold"�
���   �$��28��$
��Left=�$��438,394,0,0q�$	��Top=�$	��143���� -   $��	TextBox11��$��14(�$��23��$
��Left=�$��Right=�$��602,429,0,0q�$	��Top=�$��Center=�$	��185���� -   $��Label13��$.��(ИТОГО Премия мастерам��$��14(�$
��Bold"�
���   �$��28��$
��Left=�$��602,394,0,0q�$	��Top=�$	��185���� -   $��	TextBox12��$��14(�$��23��$
��Left=�$��Right=�$��822,429,0,0q�$	��Top=�$��Center=�$	��147���� -   $��Label14��$��ИТОГО Расход��$��14(�$
��Bold"�
���   �$��28��$
��Left=�$��822,394,0,0q�$	��Top=�$	��147��