using System;
using System.Collections.Generic;
using System.Windows;
using Company.Database.Models;
using Npgsql;

namespace Company.Database.Modules;

public class EmployeeDbModule{
    public List<Employee> GetEmployeeList(){
        List<Employee> employees = new List<Employee>();
        EmployeeSqlConnector sqlConnector = new EmployeeSqlConnector();

        const string sqlCommand = "SELECT employee_id, full_name FROM employee;";
        NpgsqlCommand command = new NpgsqlCommand(sqlCommand, sqlConnector.GetConnection());

        try{
            sqlConnector.OpenConnection();
            NpgsqlDataReader reader = command.ExecuteReader();

            while (reader.Read()){
                employees.Add(new Employee(
                    Int32.Parse(reader["employee_id"].ToString()), 
                    reader["full_name"].ToString()
                ));
            }
            sqlConnector.CloseConnection();
        }
        catch (Exception e){
            MessageBox.Show(e.Message);
        }

        return employees;
    }
}