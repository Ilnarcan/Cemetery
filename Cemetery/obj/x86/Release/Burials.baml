   M S B A M L   `   `   `  ����   Cemetery    Cemetery.Burials   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     -     � Title$\  VСписок захоронений жителей пос. Кузьмоловский ��$	��600��$
��1200�� � WindowStartupLocation$ CenterScreen=�$
��1200��$
��1200��$	��600��$	��600�� � Icon$( "/Cemetery;component/Images/App.ico�� � 
ResizeMode$ CanMinimize=� � DataContext ��  � 	Resources  �� Source�� $ Styles.xaml?�%    .��� 
�������.+��� -   $��TextBox2��$��32��$
��Left=�$��	30,91,0,0q�$	��Top=�$	��332��$��16(�$��Center=�$��1���� -   $��TextBox3��$��32��$
��Left=�$��
624,91,0,0q�$	��Top=�$	��138��$��16(�$��Center=�$��3��&   System.Windows.Controls.DataGrid  -   $��	DataGrid1��   AutoGenerateColumns . ���	#FFF6F6F6
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
 Hand���� -	   $��Label3��$%��дата захоронения��$��27��$
��Left=�$��
390,58,0,0q�$	��Top=�$	��198���� -
   $��Label5��$��ФИО умершего��$��27��$
��Left=�$��	30,58,0,0q�$	��Top=�$	��143���� -   $��Label6��$'��!место захоронения��$��27��$
��Left=�$��
624,58,0,0q�$	��Top=�$	��138���� -   $��TextBox4��$��16(�$��32��$
��Left=�$��
790,91,0,0q�$	��Top=�$��Center=�$	��123��$��4���� -   $��Label1��$��резерв��$��27��$
��Left=�$��
790,58,0,0q�$	��Top=�$	��123���� -   $��TextBox6��$��16(�$��32��$
��Left=�$��
933,91,0,0q�$	��Top=�$��Center=�$	��157��$��5���� -   $��Label7��$��примечание��$��27��$
��Left=�$��
933,58,0,0q�$	��Top=�$	��147���� -   $��Button4��$��+��$��42��$��1118,81,0,0q�$	��Top=�$
��Left=�$��48��$��30��
�������$
 Hand��(  "System.Windows.Controls.DatePicker  -   $��DatePicker1��   StyleCalendar   CalendarStyle# [  $
 Hand��$��LeftToRight=�$��32��$
��Left=�$��Center=�$��
390,91,0,0q�   SelectedDateFormat$
 Long=�$	��Top=�$��Center=�$	��198��$��2��