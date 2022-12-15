using System;
using System.Collections.Generic;
using System.Windows;
using Company.Database.Models;
using Npgsql;
using NpgsqlTypes;

namespace Company.Database.Modules;

public class TaskDbModule{
    public List<Task> GetTasks(){
        List<Task> tasks = new List<Task>();

        EmployeeSqlConnector sqlConnector = new EmployeeSqlConnector();
        const string sqlCommand =
            "SELECT task_id, create_date, execution_period  + create_date AS   execution_date, completion_date, cp.full_name AS contact_person_name, o.organization_name,pt.priority_name,trt.receipt_name,ts.status_name,ee.full_name AS executor_name FROM task t JOIN contact_person cp on cp.person_id = t.person_id JOIN organization o on o.organization_id = cp.organization_id JOIN priority_task pt on t.priority_id = pt.priority_id JOIN task_receipt_type trt on t.receipt_id = trt.receipt_id JOIN task_status ts on t.status_id = ts.status_id LEFT JOIN employee ee on t.executor_id = ee.employee_id WHERE contract_id IS NULL AND serial_number IS NULL ORDER BY t.status_id, t.priority_id DESC, execution_period;";

        NpgsqlCommand command = new NpgsqlCommand(sqlCommand, sqlConnector.GetConnection());

        try{
            sqlConnector.OpenConnection();
            NpgsqlDataReader reader = command.ExecuteReader();

            while (reader.Read()){
                DateTime? executeDate;
                if (reader["execution_date"].ToString() == ""){
                    executeDate = null;
                }
                else{
                    executeDate = DateTime.Parse(reader["execution_date"].ToString());
                }

                DateTime? completionDate;
                if (reader["completion_date"].ToString() == ""){
                    completionDate = null;
                }
                else{
                    completionDate = DateTime.Parse(reader["completion_date"].ToString());
                }

                tasks.Add(new Task(
                    Int32.Parse(reader["task_id"].ToString()),
                    DateTime.Parse(reader["create_date"].ToString()),
                    executeDate,
                    completionDate,
                    reader["contact_person_name"].ToString(),
                    reader["organization_name"].ToString(),
                    reader["priority_name"].ToString(),
                    reader["receipt_name"].ToString(),
                    reader["status_name"].ToString(),
                    reader["executor_name"].ToString()));
            }
        }
        catch (Exception e){
            MessageBox.Show(e.Message);
        }

        return tasks;
    }

    public void UpdateTaskStatus(int taskId, int status){
        EmployeeSqlConnector sqlConnector = new EmployeeSqlConnector();
        const string sqlCommand = "UPDATE task SET status_id = @status WHERE task_id = @tid;";
        NpgsqlCommand command = new NpgsqlCommand(sqlCommand, sqlConnector.GetConnection());
        command.Parameters.AddWithValue("status", NpgsqlDbType.Integer, status);
        command.Parameters.AddWithValue("tid", NpgsqlDbType.Integer, taskId);
       

        try{
            sqlConnector.OpenConnection();
            command.ExecuteNonQuery();
            sqlConnector.CloseConnection();
        }
        catch (Exception e){
            MessageBox.Show(e.Message);
        }
    }
}