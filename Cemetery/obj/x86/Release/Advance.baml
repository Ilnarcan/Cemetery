   M S B A M L   `   `   `  ����   Cemetery    Cemetery.Advance   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     -     � Title$;  5Платежная ведомость на аванс��$	��550��$	��850�� � WindowStartupLocation$ CenterScreen=�$	��850��$	��850��$	��550��$	��550�� � Icon$( "/Cemetery;component/Images/App.ico�� � 
ResizeMode$ CanMinimize=� � DataContext ��  � 	Resources  �� Source�� $ Styles.xaml?�%    .��� 
�������.+��� -   $��TextBox1��$��32��$
��Left=�$��	72,91,0,0q�$	��Top=�$	��196��$�� ��$��16(�$��Center=��� -   $��TextBox2��$��32��$
��Left=�$��
295,91,0,0q�$	��Top=�$	��222��$��16(�$��Center=��� -   $��TextBox3��$��32��$
��Left=�$��
546,91,0,0q�$	��Top=�$	��173��$��16(�$��Center=�&   System.Windows.Controls.DataGrid  -   $��	DataGrid1��   AutoGenerateColumns . ���	#FFF6F6F6
�������   CanUserAddRows . 	   CanUserResizeRows	 .  $��15(�
   GridLinesVisibility$	
 All=�$	��229��$
��Left=�$��Center=�   HorizontalGridLinesBrush
 �����   
IsReadOnly . $��
72,137,0,0q�$	��Top=�$��Center=�$	��726��   MinColumnWidth$ 30Q�   HeadersVisibility$	 All=�   VerticalGridLinesBrush
 �����   AlternatingRowBackground
 ������� -   $��Label4��$��18(�$
��Bold"�
���   �$��48��$
��Left=�$	��Top=�$	��816��$��12,0,0,0q� �� �� -   $��Button1��$ ��Редактировать��$��52��$
��Left=�$��
94,447,0,0q�$	��Top=�$	��174��
������� � Cursor$
 Hand���� -   $��Button2��$��Удалить��$��52��$
��Left=�$��382,447,0,0q�$	��Top=�$	��169��
�������$
 Hand���� -	   $��Button3��$��Печать��$��52��$
��Left=�$��637,447,0,0q�$	��Top=�$	��147��
�������$
 Hand���� -
   $��TextBox5��$��23��$
��Left=�$��274,391,0,0q�$	��Top=�$	��120��$��14(�$��Right=�$��Center=��� -   $��Label2��$��
ИТОГО��$��28��$
��Left=�$��168,386,0,0q�$	��Top=�$	��100��$��14(�
���   �$
��Bold"��� -   $��Label3��$2��,введите табельный номер��$��27��$
��Left=�$��	72,58,0,0q�$	��Top=�$	��196���� -   $��Label5��$��введите ФИО��$��27��$
��Left=�$��
295,58,0,0q�$	��Top=�$	��173���� -   $��Label6��$��введите сумму��$��27��$
��Left=�$��
546,58,0,0q�$	��Top=�$	��173���� -   $��Button4��$��+��$
 Hand��
�������$��42��$
��Left=�$��
750,81,0,0q�$��30��$	��Top=�$��48��