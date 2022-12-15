using System;
using System.Windows;
using Company.Database.Models;
using Company.Pages;


namespace Company{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window{

        private readonly ProfilePage _profilePage = new ProfilePage();
        private readonly TaskPage _taskPage = new TaskPage();
        private readonly StatisticPage _statisticPage = new StatisticPage();
        public static Employee _employee;
        public MainWindow(Employee employee){
            InitializeComponent();
            _employee = employee;
        }

        private void ProfilePageBtn_OnClick(object sender, RoutedEventArgs e){
            RenderPage.Children.Clear();
            RenderPage.Children.Add(_profilePage);
        }

        private void TaskPageBtn_OnClick(object sender, RoutedEventArgs e){
            RenderPage.Children.Clear();
            RenderPage.Children.Add(_taskPage);
        }

        private void StatisticPageBtn_OnClick(object sender, RoutedEventArgs e){
            RenderPage.Children.Clear();
            RenderPage.Children.Add(_statisticPage);
        }
    }
}