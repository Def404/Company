using System;
using System.Windows;
using Company.Database.Models;
using Npgsql;
using NpgsqlTypes;

namespace Company.Database.Modules;

public class AuthAndRegDbModule{
    public Employee GetEmployeeByLoginAndPassword(string login, string password){
        Employee employee = null;

        AuthConnector connector = new AuthConnector();

        const string sqlCommandCount =
            "SELECT count(*) AS count FROM employee WHERE employee_login=@l AND password=crypt(@pas, password)";

        NpgsqlCommand commandCount = new NpgsqlCommand(sqlCommandCount, connector.GetConnection());
        commandCount.Parameters.AddWithValue("l", NpgsqlDbType.Varchar, login);
        commandCount.Parameters.AddWithValue("pas", NpgsqlDbType.Text, password);

        const string sqlCommandGetUser = "SELECT employee_id, employee_login, full_name, email, phone_number, position_name FROM employee e JOIN employees_position ep on ep.position_id = e.position_id WHERE employee_login=@l AND password=crypt(@pas, password)";
        NpgsqlCommand commandGetUser = new NpgsqlCommand(sqlCommandGetUser, connector.GetConnection());
        commandGetUser.Parameters.AddWithValue("l", NpgsqlDbType.Varchar, login);
        commandGetUser.Parameters.AddWithValue("pas", NpgsqlDbType.Text, password);


        int count = -1;

        try{
            connector.OpenConnection();
            NpgsqlDataReader reader = commandCount.ExecuteReader();

            while (reader.Read()){
                count = Int32.Parse(reader["count"].ToString());
            }

            connector.CloseConnection();

            if (count <= 0){
                return employee = null;
            }

            connector.OpenConnection();
            NpgsqlDataReader readers = commandGetUser.ExecuteReader();

            while (readers.Read()){
                employee = new Employee(
                    Int32.Parse(readers["employee_id"].ToString()), 
                    readers["employee_login"].ToString(),
                    readers["full_name"].ToString(),
                    readers["email"].ToString(),
                    readers["phone_number"].ToString(),
                    readers["position_name"].ToString());
            }

            connector.CloseConnection();

            return employee;
        }
        catch (Exception e){
            MessageBox.Show(e.Message);

            return employee = null;
        }
    }
    
    public string SetEmployee(string login, string firstName, string secondName, string lastName, string password, string email,
        string phone, int positionId){

        AuthConnector connector = new AuthConnector();

        const string sqlCommand = "CALL create_employee(@fullName, @email, @phone, @positionId, @login, @password);";
        NpgsqlCommand command = new NpgsqlCommand(sqlCommand, connector.GetConnection());
        command.Parameters.AddWithValue("fullName", NpgsqlDbType.Text, $"{lastName} {firstName} {secondName}");
        command.Parameters.AddWithValue("email", NpgsqlDbType.Varchar, email);
        command.Parameters.AddWithValue("phone", NpgsqlDbType.Varchar, phone);
        command.Parameters.AddWithValue("positionId", NpgsqlDbType.Integer, positionId);
        command.Parameters.AddWithValue("login", NpgsqlDbType.Text, login);
        command.Parameters.AddWithValue("password", NpgsqlDbType.Text, password);
       

        string result = null;

        try{
            connector.OpenConnection();
            command.ExecuteNonQuery();
            connector.CloseConnection();
        }
        catch (NpgsqlException e){
            result = e.Message;
        }

        return result;
    }
}