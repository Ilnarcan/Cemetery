   M S B A M L   `   `   `  ����   Cemetery    Cemetery.Nimda   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     -     � Title$:  4«Visual-IT cemetery» - «Камея-Ритуал»��$	��700��$
��1350��{�.  � WindowStartupLocation$ CenterScreen=� � Icon$( "/Cemetery;component/Images/App.ico��$
��1350��$
��1350��$	��700��$	��700�� � 
ResizeMode$ CanMinimize=� � 	Resources  �� Source�� $ Styles.xaml?�%    .��� $
��1345��$	��672�� J� OpacityMask
 ����� � ColumnDefinitions �� $��0* ��� $��1345* �.+��� -   $��TabControl1��$��4q�$��Center=�$��Center=�$��LeftToRight=�
�������
��� � � �	#FF69D869
�������$��0,13,0,1q�$��2��.@��� -   $��TabItem5�� J� Opacity$ 0Q�$	��250��$	��250��	 �� 
IsSelected	 .  {�.  .��� �� -   $��TabItem1��
�������
�������$��Center=�$��Center=�$��-2,-2,-2,15q�$��Center=�$��Center=�{�. $
��35,5q�	 . ���	#FF71BCE6$��15(�
 � ForceCursor
 .   � Cursor$
 Hand������ -   $	��205��$��38��$(��"/Cemetery;component/Images/doc.png��.��� -   $��Grid2��$	��595��$
��1337��.+��� -   $��Button3��$��Дешифр��$
 Hand��$��43��$
��Left=�$��1232,160,0,0q�$	��Top=�$y�Hidden=�$��75���� -   $��Button4��$��Шифр��$
 Hand��$��48��$
��Left=�$��1229,106,0,0q�$	��Top=�$y�Hidden=�$��78���� -	   $��	ComboBox1��$��38��$
��Left=�$��478,190,0,0q�$	��Top=�$	��159��$��Segoe UI)�$��Center=�$��Center=�$��16(�.@��� $��I декада���� $��II декада���� $��III декада���� -
   $��	ComboBox2��$��38��$
��Left=�$��478,278,0,0q�$	��Top=�$	��159��$��Segoe UI)�$��Center=�$��Center=�$��16(�.@��� $��Январь���� $��Февраль���� $��Март���� $��Апрель���� $��Май���� $��Июнь���� $��Июль���� $��Август���� $��Сентябрь���� $��Октябрь���� $��Ноябрь���� $��Декабрь��(  "System.Windows.Controls.DatePicker  -   $��DatePicker1��$��38��$
��Left=�$��1061,209,0,0q�$	��Top=�$	��218��   SelectedDateFormat$
 Long=�$��Center=�$��Center=�   StyleCalendar   CalendarStyle# [  $��LeftToRight=�$
 Hand���� -   $��	ComboBox3��$��38��$
��Left=�$��478,363,0,0q�$	��Top=�$	��159��$��Segoe UI)�$��Center=�$��Center=�$��16(�.@��� $
��2013���� $
��2014���� $
��2015���� $
��2016���� $
��2017���� $
��2018���� $
��2019���� $
��2020���� $
��2021���� $
��2022���� $
��2023���� -   $��	ComboBox4��$��38��$
��Left=�$��803,278,0,0q�$	��Top=�$	��165��$��Segoe UI)�$��Center=�$��Center=�$��16(�.@��� $��Январь���� $��Февраль���� $��Март���� $��Апрель���� $��Май���� $��Июнь���� $��Июль���� $��Август���� $��Сентябрь���� $��Октябрь���� $��Ноябрь���� $��Декабрь���� -   $��	ComboBox5��$��38��$
��Left=�$��803,363,0,0q�$	��Top=�$	��165��$��Segoe UI)�$��Center=�$��Center=�$��16(�.@��� $
��2013���� $
��2014���� $
��2015���� $
��2016���� $
��2017���� $
��2018���� $
��2019���� $
��2020���� $
��2021���� $
��2022���� $
��2023��  -   $��DatePicker2��$
 Hand��$��LeftToRight=�$��38��$
��Left=�$��Center=�$��1061,316,0,0q�$
 Long=�$	��Top=�$��Center=�# [  $	��218���� -   $��Label9��$��декада:��$��49��$
��Left=�$��372,190,0,0q�$	��Top=�$	��100��$��18(�$��Normal"�
���iii�$ Arrow���� -   $��Label10��$��месяц:��$��38��$
��Left=�$��372,278,0,0q�$	��Top=�$��18(�$��Normal"�
���iii�$	��100��$ Arrow���� -   $��Label11��$��год:��$��42��$
��Left=�$��372,363,0,0q�$	��Top=�$��18(�$	��100��$��Normal"�
���iii�$ Arrow���� -   $��Label12��$��месяц:��$��18(�$��38��$
��Left=�$��697,278,0,0q�$	��Top=�
���iii�$��Normal"�$	��100��$ Arrow���� -   $��Label13��$��год:��$��18(�$��42��$
��Left=�$��697,363,0,0q�$	��Top=�$	��100��
���iii�$��Normal"�$ Arrow���� -   $��Label14��$��дата начала:��$��18(�$��38��$
��Left=�$��1098,165,0,0q�$	��Top=�$	��152��
���iii�$��Normal"�$ Arrow���� -   $��Label15��$"��дата окончания:��$��18(�$��38��$
��Left=�$��1098,272,0,0q�$	��Top=�$	��152��
���iii�$��Normal"�$ Arrow���� -   $*��$/Cemetery;component/Images/open1.png��$��46��$	��193��$��83,489,1061,60q�$
 Hand���� -   $+��%/Cemetery;component/Images/greate.png��$��46��$	��193��$��83,256,1061,293q�$
 Hand���� -   $
 Hand��$��46��$��445,490,699,60q�$*��$/Cemetery;component/Images/open1.png��$	��193���� -   $
 Hand��$��46��$��757,489,388,60q�$*��$/Cemetery;component/Images/open1.png��$	��193���� -   $
 Hand��$��46��$��1084,489,60,60q�$*��$/Cemetery;component/Images/open1.png��$	��193���� $��10,10,939,580q�
�������$��3��$	��386��	 �� X2$ 9000000��	 �� Y1$	 100��	 �� Y2$	 350���� -   $��
TextBlock1��$	��112��$
��Left=�$��	-2,80,0,0q�$�� ��$	��Top=�$	��366�� �� TextAlignment$ Center=�
���   �$
��Wrap=�$��24(�$��Normal"�$ Arrow���� -   $��Image1��$��1��$
��Left=�$��	1,343,0,0q�$
��Fill=�$	��Top=�$	��363��$*��$/Cemetery;component/Images/lineg.png���� -   $��Image2��$	��520��$
��Left=�$��	365,0,0,4q�$
��Fill=�$��Bottom=�$��1��$)��#/Cemetery;component/Images/line.png���� -   $��Image3��$	��520��$
��Left=�$��	690,0,0,4q�$)��#/Cemetery;component/Images/line.png��$
��Fill=�$��Bottom=�$��1���� -    $��Image4��$	��520��$
��Left=�$��
1010,0,0,4q�$)��#/Cemetery;component/Images/line.png��$
��Fill=�$��Bottom=�$��1���� -!   $��
TextBlock2��$��24(�$
��Bold"�
���   �$��43��$
��Left=�$��	1,363,0,0q�$��Текущий день��$ Center=�$
��Wrap=�$	��Top=�$	��363���� -"   $��
TextBlock3��$��24(�$
��Bold"�
���   �$��47��$
��Left=�$��
362,88,0,0q�$��Декада��$ Center=�$
��Wrap=�$	��Top=�$	��319��$ Arrow���� -#   $��
TextBlock4��$��24(�$
��Bold"�
���   �$��47��$
��Left=�$��
687,88,0,0q�$��
Месяц��$ Center=�$
��Wrap=�$	��Top=�$	��319��$ Arrow���� -$   $��
TextBlock5��$��24(�$
��Bold"�
���   �$��37��$
��Left=�$��1012,88,0,0q�$��Период��$ Center=�$
��Wrap=�$	��Top=�$	��319��$ Arrow���� -%   $��TabItem6��$ 0Q�$	��350��$��0��$	��350��{�.  .��� $	��535�� �� $	��86* ��� $
��756* ��� -&   $��TabItem2��
�������
�������$
��35,5q�$��Center=�$��Center=�$��-2,-2,-2,15q�$��Center=�$��Center=�$��15(�$
 Hand������ -'   $	��205��$��38��$*��$/Cemetery;component/Images/admin.png��.��� $	��448��$
��1302��.+��� -(   $��Label1��$��
Логин��$��36��$
��Left=�$��
273,66,0,0q�$	��Top=�$��59���� -)   $��Label2��$��Пароль��$��31��$��262,110,970,0q�$	��Top=��� -*   $��Label3��$-��'Права администратора��$��49��$
��Left=�$��162,167,0,0q�$	��Top=��� -+   $��Button1��$-��'Создать пользователя��$��73��$
��Left=�$��208,267,0,0q�$	��Top=�$	��277���� -,   $��
Rectangle1��$	��100��$
��Left=�$��	6,336,0,0q�
�������$	��Top=�$	��200���� --   $��Button2��$-��'Удалить пользователя��$��73��$
��Left=�$��863,267,0,0q�$	��Top=�$	��277���� -.   $��TextBox1��
�������$��16(�$��31��$��Center=�$��338,0,769,346q� �� 	MaxLength$ 16��$��
2147483645�� �� TextWrapping$ NoWrap=�$��Bottom=�$��Center=�G� -/   $��PasswordBox1��
�������$��16(�$��31��$��Center=���. $��338,0,769,301q� G� 	MaxLength$ 16��$��
2147483646��$��Bottom=�$��Center=��� -0   $��	CheckBox1�� J� 	AllowDrop .  $��RightToLeft=�$��14(�$��15��$
��Left=�$��Center=� d� 	IsChecked$ FalseY�{�.  J� IsHitTestVisible .  d� IsThreeState .  $��338,177,0,0q�$��4,0,0,0q�$��18��$	��Top=�$��Center=�$��19��&   System.Windows.Controls.DataGrid  -1   $��	DataGrid1��   AreRowDetailsFrozen .    AutoGenerateColumns . ���	#FFF6F6F6
�������   CanUserAddRows .    CanUserResizeRows .  $��15(�   GridLinesVisibility$	 All=�   HeadersVisibility$	 All=�$	��151��$
��Left=�$��Center=�   HorizontalGridLinesBrush
 �   �    
IsReadOnly  . $��
891,55,0,0q�!   MinColumnWidth$! 30Q�$	��Top=�$��Center=�$	��249���� -2   $��Image5��$	��444��$
��Left=�$��	689,0,0,4q�$)��#/Cemetery;component/Images/line.png��$
��Fill=�$��Bottom=�$��1���� -3   $��TabItem7��$ 0Q�$	��100��$	��100��{�.  .���  �� $
��440* ��� $
��640* ��� -4   $��TabItem8��$ 0Q�$	��100��$	��100��{�.  .��� � -5   $��Grid1��$��1��$��0,75,-1,521q����	#69D3D3D3.+��� -6   $��	ComboBox6��$��Segoe UI)�$��38��$
��Left=�$��
272,17,0,0q�$	��Top=�$��Center=�$��18(�$
��Left=�$	��477��.@��� $,��&Захоронения в резерв���� $F��@Отчет по захоронениям и памятникам���� $6��0Отчет по приходу и расходу���� $��Памятники���� $7��1Платежная ведомость(аванс)���� $=��7Платежная ведомость(зарплата)���� $#��Премия мастерам���� $:��4Приход материальных средств���� $5��/Приход, ритуальные услуги���� $��Расход���� $B��<Расход по зарплате и премиальных���� $��Резервы���� $>��8Списание канцелярских товаров���� $G��AСписание материалов в производство���� $[��UСписок захоронений жителей пос. Кузьмоловский���� $7��1Список захоронений, оплата���� -7   $��Label16��$0��*Выберите тип документа��$��20(�$��Normal"�
���iii�$��49��$
��Left=�$��	18,17,0,0q�$	��Top=�$	��248������ " �� ImageSource$+" %/Cemetery;component/Images/line21.png��