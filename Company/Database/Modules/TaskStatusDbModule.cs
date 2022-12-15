using System;
using System.Collections.Generic;
using System.Windows;
using Company.Database.Models;
using Npgsql;

namespace Company.Database.Modules;

public class TaskStatusDbModule{
    public List<TaskStatus> GetTaskStatusList(){
        List<TaskStatus> taskStatusList = new List<TaskStatus>();
        EmployeeSqlConnector sqlConnector = new EmployeeSqlConnector();

        const string sqlCommand = "SELECT * FROM task_status WHERE status_id < 3";
        NpgsqlCommand command = new NpgsqlCommand(sqlCommand, sqlConnector.GetConnection());

        try{
            sqlConnector.OpenConnection();
            NpgsqlDataReader reader = command.ExecuteReader();

            while (reader.Read()){
                taskStatusList.Add(new TaskStatus(
                    Int32.Parse(reader["status_id"].ToString()), 
                    reader["status_name"].ToString()));
            }
            sqlConnector.CloseConnection();
        }
        catch (Exception e){
            MessageBox.Show(e.Message);
        }

        return taskStatusList;
    }
}