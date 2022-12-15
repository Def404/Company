using System;
using System.Collections.Generic;
using System.Windows;
using Company.Database.Models;
using Npgsql;

namespace Company.Database.Modules;

public class TaskReceiptDbModule{
    public List<TaskReceipt> GetTaskReceiptList(){
        List<TaskReceipt> taskReceipts = new List<TaskReceipt>();
        EmployeeSqlConnector sqlConnector = new EmployeeSqlConnector();

        const string sqlCommand = "SELECT * FROM task_receipt_type;";
        NpgsqlCommand command = new NpgsqlCommand(sqlCommand, sqlConnector.GetConnection());

        try{
            sqlConnector.OpenConnection();
            NpgsqlDataReader reader = command.ExecuteReader();

            while (reader.Read()){
                taskReceipts.Add(new TaskReceipt(
                    Int32.Parse(reader["receipt_id"].ToString()), 
                    reader["receipt_name"].ToString()
                    ));
            }
            sqlConnector.CloseConnection();
        }
        catch (Exception e){
            MessageBox.Show(e.Message);
        }

        return taskReceipts;
    }
}