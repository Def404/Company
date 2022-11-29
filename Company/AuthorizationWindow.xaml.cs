using System.Configuration;
using System.Text.RegularExpressions;
using System.Windows;

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

        if (login.Length < 3){
            var errorStr = "Логин должен быть от 5 символов";
            MessageBox.Show(errorStr);

            return;
        }

        if (password.Length < 3){
            var errorStr = "Пароль должен быть от 5 символов";
            MessageBox.Show(errorStr);

            return;
        }

        if (Regex.IsMatch(login, "[а-яА-Я]") ||
            Regex.IsMatch(password, "[а-яА-Я]")){
            var errorStr = "Проверьте написание логина или папроля";
            MessageBox.Show(errorStr);

            return;
        }

        Employee employee = _authAndRegDbModule.GetEmployeeByLoginAndPassword(login, password);

        if (employee == null){
            var errorStr = "Неправильный логин или пароль";
            MessageBox.Show(errorStr);

            return;
        }

        employee.Password = password;

        try{
            var confFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var consSet = confFile.ConnectionStrings.ConnectionStrings;
            var connectionsString = consSet["UserConnection"].ConnectionString;
            var newCntStr = connectionsString.Replace("usr", employee.EmployeeLogin).Replace("pswd", employee.Password);

            consSet["UserConnection"].ConnectionString = newCntStr;
            confFile.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection(confFile.ConnectionStrings.SectionInformation.Name);
			
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