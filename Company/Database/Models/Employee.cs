namespace Company.Database.Models;

public class Employee{
    public int EmployeeId{ get; }
    public string EmployeeLogin{ get; }
    
    public string FullName{ get; }
    public string Email{ get; }
    public string PhoneNumber{ get; }
    public string PositionName{ get; }

    public Employee(int employeeId, string employeeLogin, string fullName, string email, string phoneNumber, string positionName){
        EmployeeId = employeeId;
        EmployeeLogin = employeeLogin;
        FullName = fullName;
        Email = email;
        PhoneNumber = phoneNumber;
        PositionName = positionName;
    }

    public Employee(int employeeId, string fullName){
        EmployeeId = employeeId;
        FullName = fullName;
    }

    public Employee(string employeeLogin, string fullName, string email, string phoneNumber, string positionName){
        EmployeeLogin = employeeLogin;
        FullName = fullName;
        Email = email;
        PhoneNumber = phoneNumber;
        PositionName = positionName;
    }
}