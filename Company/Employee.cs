namespace Company;

public class Employee{
    public string EmployeeLogin{ get; }
    public string Password{ get; set; }
    public string FullName{ get; }
    public string Email{ get; }
    public string PhoneNumber{ get; }
    public int PositionId{ get; }

    public Employee(string employeeLogin, string password, string fullName, string email, string phoneNumber, int positionId){
        EmployeeLogin = employeeLogin;
        Password = password;
        FullName = fullName;
        Email = email;
        PhoneNumber = phoneNumber;
        PositionId = positionId;
    }
}