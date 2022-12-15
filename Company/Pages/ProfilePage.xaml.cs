using System.Windows;
using System.Windows.Controls;

namespace Company.Pages;

public partial class ProfilePage : UserControl{
    public ProfilePage(){
        InitializeComponent();
    }

    private void ProfilePage_OnLoaded(object sender, RoutedEventArgs e){
        LoginTxtBlock.Text = MainWindow._employee.EmployeeLogin;
        FullNameTxtBlock.Text = MainWindow._employee.FullName;
        PositionTxtBlock.Text = MainWindow._employee.PositionName;
        EmailTxtBlock.Text = MainWindow._employee.Email;
        PhoneNumberTxtBlock.Text = MainWindow._employee.PhoneNumber;
    }
}