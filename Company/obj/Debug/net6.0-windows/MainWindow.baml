   M S B A M L   `   `   `  ????B  >Company, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:CompanyCompany  5   	       Company.MainWindow   5   	   W SPresentationCore, Version=6.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:Company     5   	     ? Title$  
MainWindow??5   	   $	??550??6   $	??800??6)    ? WindowStartupLocation$ CenterScreen=?5	   	   .??5
      ? 
??????? ? RowDefinitions 5   
   ?? 5      $??50 ?6   ?? 5      .+?5   
   ?? $??0??5       ?? Orientation$ 
Horizontal=?5      $??Center=?5      $??Center=?5      .+?5      ?? -   5         ??Name$ StatisticPageBtn??5      $??Статистика??5      
?????? $
??25,0q?5      $??16(?5      $??DemiBold"?5      
?????? 5      ?? 5      -   5        $ TaskPageBtn??5      $??Задания??5       
?????? $
??25,0q?5"      $??16(?5#      $??DemiBold"?5$      
?????? 5&      ?? 5'      -   5        $ ProfilePageBtn??5(      $??Профиль??5)      
?????? $
??25,0q?5+      $??16(?5,      $??DemiBold"?5-      
?????? 5/      50      ? 51   
   -   5         ?Name$ 
RenderPage??52      $??1??53      55      56      57      