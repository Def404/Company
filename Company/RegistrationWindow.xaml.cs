using System;
using System.Text.RegularExpressions;
using System.Windows;
using Company.Database;
using Company.Database.Models;
using Company.Database.Modules;

namespace Company;

public partial class RegistrationWindow : Window{
    private readonly AuthAndRegDbModule _authAndRegDbModule = new AuthAndRegDbModule(); 
    public RegistrationWindow(){
        InitializeComponent();
    }

    private void RegistrationBtn_OnClick(object sender, RoutedEventArgs e){
        var firstName = FirstNameTxtBox.Text.Trim();
        var secondName = SecondNameTxtBox.Text.Trim();
        var lastName = LastNameTxtBox.Text.Trim();
        var login = LoginTxtBox.Text.Trim();
        var email = EmailTxtBox.Text.Trim();
        var phone = PhoneTxtBox.Text.Trim();
        var password = PasswordBox.Password.Trim();
        var repeatPassword = RepeatPasswordBox.Password.Trim();

        if (firstName == "" || secondName == "" || lastName == "" || login == "" || email == "" || phone == "" ||
            password == "" || repeatPassword == "" || PositionComboBox.SelectedIndex == -1){
            
            MessageBox.Show("не все поля заполенены");
            return;
        }
        
        if (!Regex.IsMatch(email, @"^[-\w.]+@([A-z0-9][-A-z0-9]+\.)+[A-z]{2,4}$")){
            var errorStr = "Проверьте написание почты";

            MessageBox.Show(errorStr);
            return;
        }
        
        if (!Regex.IsMatch(phone, @"^((8|\+7)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{7,10}$")){
            var errorStr = "Проверьте написание телефона";

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

        if (password.Equals(repeatPassword) == false){
            MessageBox.Show("Пароли не совпадают");

            return;
        }

        var positionId = PositionComboBox.SelectedIndex + 1;
        var resultSetUser =
            _authAndRegDbModule.SetEmployee(login, firstName, secondName, lastName, password, email, phone, positionId);
        
        if (resultSetUser != null){
            MessageBox.Show(resultSetUser);

            return;
        }

        Employee employee = new Employee(login,
            $"{lastName} {firstName} {secondName}",
            email,
            phone,
            PositionComboBox.Text);

        try{
            EmployeeSqlConnector sqlConnector = new EmployeeSqlConnector();

            var res = sqlConnector.UpdateConnectionString(
                $"Server=localhost;Port=5432;User ID={login};Password={password};Database=company;");

            if (res == false){
                return;
            }

            MainWindow mainWindow = new MainWindow(employee);
            mainWindow.Show();
            
            Close();
        }
        catch (Exception exception){
            MessageBox.Show("");
        }
    }

    private void LoginBtn_OnClick(object sender, RoutedEventArgs e){
        AuthorizationWindow authorizationWindow = new AuthorizationWindow();
        authorizationWindow.Show();
        
        Close();
    }
}