namespace Company.Database.Models;

public class Employee{
    public string EmployeeLogin{ get; }
    
    public string FullName{ get; }
    public string Email{ get; }
    public string PhoneNumber{ get; }
    public string PositionName{ get; }

    public Employee(string employeeLogin, string fullName, string email, string phoneNumber, string positionName){
        EmployeeLogin = employeeLogin;
        FullName = fullName;
        Email = email;
        PhoneNumber = phoneNumber;
        PositionName = positionName;
    }
}