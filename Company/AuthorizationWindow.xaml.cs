using System.Configuration;
using System.Text.RegularExpressions;
using System.Windows;
using Company.Database;
using Company.Database.Models;
using Company.Database.Modules;

namespace Company;

public partial class AuthorizationWindow : Window{
    private readonly AuthAndRegDbModule _authAndRegDbModule = new AuthAndRegDbModule();
    public AuthorizationWindow(){
        InitializeComponent();
    }

    private void AuthBtn_OnClick(object sender, RoutedEventArgs e){
        var login = LoginTxtBox.Text.Trim();
        var password = PasswordBox.Password.Trim();

        if (login == "" || password == ""){
            var errorStr = "Не все значения введены";
            MessageBox.Show(errorStr);

            return;
        }

        if (!Regex.IsMatch(login, @"[a-zA-Z0-9-_\.]{4,20}$")){
            var errorStr = "Проверьте написание логина\n" +
                           "В логине могут быть буквы, цифры и символы(-, _), первый символ обязательно буква, от 5 до 20 символов";
            MessageBox.Show(errorStr);

            return;
        }

        if (!Regex.IsMatch(password, @"(?=^.{8,}$)((?=.*\d)|(?=.*\W+))(?![.\n])(?=.*[A-Z])(?=.*[a-z]).*$")){
            var errorStr = "Проверьте написание папроля \n" +
                           "В пароле могуть быть строчные и прописные латинские буквы, цифры, спецсимволы. Минимум 8 символов\n" +
                           "Обязатльно хотя бы одна строчная и прописаня буква и одна цифра";

            MessageBox.Show(errorStr);

            return;
        }

        Employee employee = _authAndRegDbModule.GetEmployeeByLoginAndPassword(login, password);

        if (employee == null){
            var errorStr = "Неправильный логин или пароль";
            MessageBox.Show(errorStr);

            return;
        }

        //employee.Password = password;

        try{
            EmployeeSqlConnector sqlConnector = new EmployeeSqlConnector();
            var res = sqlConnector.UpdateConnectionString(
                $"Server=localhost;Port=5432;User ID={login};Password={password};Database=company;");
            
            if (res == false){
                return;
            }
			
            var mainWindow = new MainWindow(employee);
            mainWindow.Show();

            Close();
        }
        catch (ConfigurationErrorsException){
            MessageBox.Show("");
        }
    }

    private void RegBtn_OnClick(object sender, RoutedEventArgs e){
        RegistrationWindow registrationWindow = new RegistrationWindow();
        registrationWindow.Show();
        
        Close();
    }
}