   M S B A M L   `   `   `  ����D  @NfcClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:NfcClient	NfcClient  5      e aMicrosoft.Web.WebView2.Wpf, Version=1.0.2365.46, Culture=neutral, PublicKeyToken=2a8ab48044d2601ekLclr-namespace:Microsoft.Web.WebView2.Wpf;assembly=Microsoft.Web.WebView2.WpfMicrosoft.Web.WebView2.Wpf 5          NfcClient.MainWindow   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3i+http://schemas.microsoft.com/xaml/behaviors  5      #localclr-namespace:NfcClient  5      Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  5      _	 [Hardcodet.NotifyIcon.Wpf, Version=1.1.0.0, Culture=neutral, PublicKeyToken=682384a853a08aad)tb http://www.hardcodet.net/taskbar 	 5	      A
 =Wpf.Ui, Version=3.0.0.0, Culture=neutral, PublicKeyToken=null/ui&http://schemas.lepo.co/wpfui/2022/xaml 
 5
      Vwv2Lclr-namespace:Microsoft.Web.WebView2.Wpf;assembly=Microsoft.Web.WebView2.Wpf 
 5      -   5        " 
 Wpf.Ui.Controls.FluentWindow   Name$  window��5      $	��270��5      $	��400��5      $	��270��5      $	��435��5      !   ExtendsContentIntoTitleBar$
 True��5       � ShowInTaskbar$ False��5         WindowBackdropType$
 Mica=�5         WindowCornerPreference$ Default=�5       � WindowStyle$
 None=�5      .��5      �  � RowDefinitions 5   
   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      �� 5      $
��auto �6   �� 5      $
��auto �6   .+�5!   
   3 	 -Hardcodet.Wpf.TaskbarNotification.TaskbarIcon  -   5          Name$ MyNotifyIcon��5"         
IconSource$ /Assets/music.ico��5%      	   DoubleClickCommand	 5#      �� *MinWindowCommand+"
   DoubleClickCommandParameter
 5$      ��  �� ElementName$ window��5%       
 Wpf.Ui.Controls.TitleBar  5'   
   -   5          Name$ TitleBar��5(         Title$	 NFC��5)      $��0��5*      %   CloseWindowByDoubleClickOnIcon$
 True��5+      5/      � 51   
   $��1��6   $
��10,0q�6   .+�52       
 Wpf.Ui.Controls.TextBox  -   5          Name$
 text��52         PlaceholderText$ Search��6'   .��6@   *  $Microsoft.Xaml.Behaviors.Interaction   Triggers 53      +  %Microsoft.Xaml.Behaviors.EventTrigger  54         	EventName$ TextChanged��6%   *  $Microsoft.Xaml.Behaviors.TriggerBase   Actions. 55      2  ,Microsoft.Xaml.Behaviors.InvokeCommandAction     Command 60   �� *SearchMusicCommand+   CommandParameter 6W   �� $
 text�� �� Path$
 Text�56      .��57   *   58      59      � 5;   
   $��2��6   $��10q�6   .+�5<      �� -   5         ��Name$
 list��5=      ��5?      �� *
ListBars+��5@      �� 5A      .R�5B      ��  �� Orientation$ 
Horizontal=�6%   .+�5C      �� $��10,0,0,0q�5D   !   $��16(�5E   !   ��5F   !   �� *Id+ �� StringFormat$
 {0}.���� 5G      $��10,0,0,0q�5H   !   $��16(�5I   !   ��5J   !   �� *Name+5K      5L      5N      5O      � 5Q   
   $��3��6   $��10q�6   .+�5R      )	  #Microsoft.Web.WebView2.Wpf.WebView2	  -   5         	 Name$ webView��5S      $��90��5T      5U      5V      � 5X   
   $��4��6   $��10q�6   .+�5Y      ]� $��False��6   .+�5Z      
 
 Wpf.Ui.Controls.Button
  $��theme��5\       � ToolTip$ Toggle theme��5^      ��5[      �� *ThemeChangeCommand+ 
  Icon 5]      ' 
 !Wpf.Ui.Markup.SymbolIconExtension  *WeatherSunny24+5^      d� 5_      -   5         d�Name$
 send��5`      $��Right=�5a       d� 	IsChecked 5b      �� $ window��$ IsSend� �� Mode$ TwoWay=���5c      �� 5d        �� 
TargetType"  d�6    .�5e      �� ! �� Property! � ��" �� Value" 5f   "   �� 5g   &   # �� 
TargetType"# d�66   .R�5h   *   �� #��z��5i   -   #��z��5j   -   $ �� Padding$$ 3q�5k   -   $��Center=�5l   -   $��Center=�5m   -   $��1q�5n   -   % �� CornerRadius$% 0��5o   -   ._�5p   .   �� & ��Name$& ContentPresenterElement��5q   1   $��Center=�5r   1   $��Center=�5s   1   5t   +   ' �� Triggers' 5u   *   P� 5v   .   ( P� Property( �@d�	IsChecked) P� Value$
) TrueY�6K   . �5w   2   �� ! � ��" 5x   6     
 Wpf.Ui.Controls.SymbolIcon  5y   :   *   Symbol$* Circle20=�6H   5{   3   + N� ExitActions+ 5|   2   �� 5}   6   .��5~   :   �� .��5   >   W� , g� 	BeginTime	, 0:0:05�   A   - g� FillBehavior$
- Stop=�5�   A   . �� 
TargetName$. ContentPresenterElement��5�   A   / �� TargetProperty$/ (UIElement.Opacity)�5�   A   	0 W� To0 05�   A   1 g� Duration$1 0:0:0.3D�5�   A   5�   ;   5�   7   5�   /   P� 5�   .   ( �@d�	IsChecked$) FalseY�6K   . �5�   2   �� ! � ��" 5�   6     5�   :   $* Record48=�6H   5�   3   + 5�   2   �� 5�   6   .��5�   :   �� .��5�   >   W� 	, 0:0:05�   A   $
- Stop=�5�   A   $. ContentPresenterElement��5�   A   $/ (UIElement.Opacity)�5�   A   0 05�   A   $1 0:0:0.3D�5�   A   5�   ;   5�   7   5�   /   5�   '   5�      5�      5�      
  5�      $��0,0,5,0q�5�      $��Right=�5�      ��5�      �� *DeleteMusicCommand+��5�      �� $
 list��$ SelectedItem� 5�        *
Delete20+
  5�      -   5        2 
 Name$2 Update��5�      $��0,0,5,0q�5�      $��Right=�5�       5�        *CloudArrowUp48+
  5�      -	   5        $	2 Add��5�      $��0,0,5,0q�5�      $��Right=�5�       5�        *Add48+5�      5�      5�      5�      