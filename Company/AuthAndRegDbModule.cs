using System;
using System.Windows;
using Npgsql;
using NpgsqlTypes;

namespace Company;

public class AuthAndRegDbModule{
    public Employee GetEmployeeByLoginAndPassword(string login, string password){
        Employee employee = null;

        AuthConnector connector = new AuthConnector();

        const string sqlCommandCount =
            "SELECT count(*) AS count FROM employee WHERE employee_login=@l AND password=crypt(@pas, password)";

        NpgsqlCommand commandCount = new NpgsqlCommand(sqlCommandCount, connector.GetConnection());
        commandCount.Parameters.AddWithValue("l", NpgsqlDbType.Varchar, login);
        commandCount.Parameters.AddWithValue("pas", NpgsqlDbType.Text, password);

        const string sqlCommandGetUser = "SELECT * FROM employee WHERE employee_login=@l AND password=crypt(@pas, password)";
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
                    readers["employee_login"].ToString(),
                    "",
                    readers["full_name"].ToString(),
                    readers["email"].ToString(),
                    readers["phone_number"].ToString(),
                    Int32.Parse(readers["position_id"].ToString()));
            }

            connector.CloseConnection();

            return employee;
        }
        catch (Exception e){
            MessageBox.Show(e.Message);

            return employee = null;
        }
    }
}