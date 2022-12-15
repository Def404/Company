using System;
using System.Collections.Generic;
using System.Windows;
using Company.Database.Models;
using Npgsql;

namespace Company.Database.Modules;

public class TaskPriorityDbModule{
    public List<TaskPriority> GetTaskPriorityList(){
        List<TaskPriority> taskPriorities = new List<TaskPriority>();
        EmployeeSqlConnector sqlConnector = new EmployeeSqlConnector();

        const string sqlCommand = "SELECT * FROM priority_task;";
        NpgsqlCommand command = new NpgsqlCommand(sqlCommand, sqlConnector.GetConnection());

        try{
            sqlConnector.OpenConnection();
            NpgsqlDataReader reader = command.ExecuteReader();

            while (reader.Read()){
                taskPriorities.Add(new TaskPriority(
                    Int32.Parse(reader["priority_id"].ToString()), 
                    reader["priority_name"].ToString()
                ));
            }
            sqlConnector.CloseConnection();
        }
        catch (Exception e){
            MessageBox.Show(e.Message);
        }

        return taskPriorities;
    }
}