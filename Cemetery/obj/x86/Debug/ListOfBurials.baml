   M S B A M L   `   `   `  ����  Cemetery    Cemetery.ListOfBurials   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5          � Title$7  1Список захоронений, оплата��5   	   $	��600��6,   $
��1000��69    � WindowStartupLocation$ CenterScreen=�6F   $
��1000��6k   $
��1000��5   	   $	��600��6   $	��600��6)    � Icon$( "/Cemetery;component/Images/App.ico��5   	    � 
ResizeMode$ CanMinimize=�63    � DataContext 5   9   ��  � 	Resources 5       �� Source�� 5   
   $ Styles.xaml?�6   %    64   .��5
      � 
�������.+�5   
   �� -   5        $��TextBox2��5   M   $��32��6   $
��Left=�6   $��
287,91,0,0q�69   $	��Top=�6]   $��80��5      $��16(�6   $��Center=�6+   $��2��6M   �� 5   
   -   5        $��TextBox3��5   M   $��32��6   $
��Left=�6   $��
373,91,0,0q�69   $	��Top=�6]   $��87��5      $��16(�6   $��Center=�6+   $��3��6M   &   System.Windows.Controls.DataGrid  5   
   -   5        $��	DataGrid1��5   9      AutoGenerateColumns . ���	#FFF6F6F6
�������   CanUserAddRows . 	   CanUserResizeRows	 .  $��15(�5   -   
   GridLinesVisibility$	
 All=�6;   $	��237��6U   $
��Left=�5      $��Center=�6.      HorizontalGridLinesBrush
 �����   
IsReadOnly . $��
72,137,0,0q�5   %   $	��Top=�6J   $��Center=�5      $	��863��65      MinColumnWidth$ 30Q�6o      HeadersVisibility$	 All=�6�      VerticalGridLinesBrush
 �����   AlternatingRowBackground
 �����6�   �� 5   
   -   5        $��Label4��5   7   $��18(�5   (   $
��Bold"�66   
���   �$��48��5      $
��Left=�6   $	��Top=�6E   $	��816��5      $��12,0,0,0q�6    5      �� 5      �� 5   
   -   5        $��Button1��5   e   $ ��Редактировать��6   $��52��6)   $
��Left=�65   $��127,446,0,0q�6P   $	��Top=�5      $	��174��6)   
������� � Cursor$
 Hand��6^   �� 5   
   -   5        $��Button2��5   _   $��Удалить��6   $��52��6#   $
��Left=�6/   $��457,446,0,0q�6J   $	��Top=�5      $	��169��6)   
�������$
 Hand��6H   6V   �� 5   
   -   5        $��Button3��5   ^   $��Печать��6   $��52��6"   $
��Left=�6.   $��773,446,0,0q�6I   $	��Top=�5      $	��147��6)   
�������$
 Hand��6^   �� 5   
   -	   5        $��TextBox5��5   N   $��23��6   $
��Left=�6   $��814,395,0,0q�69   $	��Top=�6^   $	��120��6v   $��14(�6�   $��Right=�6�   $��Center=�6�   �� 5   
   -
   5        $��Label2��5   \   $��
ИТОГО��6   $��28��6    $
��Left=�6,   $��708,390,0,0q�6G   $	��Top=�6j   $	��100��6�   $��14(�6�   
���   �$
��Bold"�6�   �� 5   
   -   5        $��Label3��5   `   $��дата оплаты��6   $��27��6&   $
��Left=�62   $��	72,58,0,0q�6M   $	��Top=�6n   $��93��6�   �� 5    
   -   5        $��Label5��5    ^   $��№ заказа��6   $��27��6#   $
��Left=�6/   $��
287,58,0,0q�6J   $	��Top=�6l   $��80��6�   �� 5!   
   -   5        $��Label6��5!   a   $��№ квитанции��6   $��27��6&   $
��Left=�62   $��
373,58,0,0q�6M   $	��Top=�6o   $��87��6�   �� 5"   
   -   5        $��TextBox4��5"   [   $��16(�6   $��32��6    $
��Left=�6,   $��
468,91,0,0q�6G   $	��Top=�6k   $��Center=�6�   $	��107��6�   $��4��6�   6�   �� 5#   
   -   5        $��Label1��5#   f   $%��стоимость услуги��6   $��27��6+   $
��Left=�67   $��
468,58,0,0q�6R   $	��Top=�6t   $	��107��6�   �� 5$   
   -   5        $��TextBox6��5$   B   $��16(�6   $��32��6    $��592,91,219,0q�6,   $	��Top=�6R   $��Center=�6j   $��5��6�   �� 5%   
   -   5        $��Label7��5%   b   $��ФИО умершего��6   $��27��6'   $
��Left=�63   $��
592,58,0,0q�6N   $	��Top=�6p   $	��167��6�   �� 5&   
   -   5        $��TextBox7��5&   [   $��16(�6   $��32��6    $
��Left=�6,   $��
782,91,0,0q�6G   $	��Top=�6k   $��Center=�6�   $	��120��6�   $��6��6�   �� 5'   
   -   5        $��Button4��5'   =   $��+��6   $��42��6   $��
918,81,0,0q�6)   $	��Top=�6L   $
��Left=�6d   $��48��6   $��30��6�   
�������$
 Hand��6�   6�   (  "System.Windows.Controls.DatePicker  5(   
   -   5        $��DatePicker1��5(   �      StyleCalendar   CalendarStyle# [  6   $
 Hand��6D   $��LeftToRight=�6R   $��32��6n   $
��Left=�6z   $��Center=�6�   $��	72,91,0,0q�6�      SelectedDateFormat$
 Long=�6�   $	��Top=�6�   $��Center=�6  $	��198��63  �� 5)   
   -   5        $��label8��5)   ^   $��землекоп��6   $��27��6#   $
��Left=�6/   $��
782,58,0,0q�6J   $	��Top=�6l   $	��111��6�   5*      5+      