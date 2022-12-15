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
        const string sqlCommand = "SELECT task_id, create_date, execution_period  + create_date AS   execution_date, completion_date, cp.full_name AS contact_person_name, o.organization_name, c.contract_id, hd.serial_number, pt.priority_name, trt.receipt_name, ts.status_name, ee.full_name AS executor_name FROM task t JOIN contact_person cp on cp.person_id = t.person_id JOIN organization o on o.organization_id = cp.organization_id LEFT JOIN contract c on c.contract_id = t.contract_id LEFT JOIN hard_drive hd on hd.serial_number = t.serial_number JOIN priority_task pt on t.priority_id = pt.priority_id JOIN task_receipt_type trt on t.receipt_id = trt.receipt_id JOIN task_status ts on t.status_id = ts.status_id LEFT JOIN employee ee on t.executor_id = ee.employee_id ORDER BY t.status_id, t.priority_id DESC, execution_period;";

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
                    reader["contract_id"].ToString(),
                    reader["serial_number"].ToString(),
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

    public void SetTask(int executeInt, int personId,  int?  contractId,  int? serialNumber, int priorityId, int receiptId, int statusId, int? executorId){
        EmployeeSqlConnector sqlConnector = new EmployeeSqlConnector();

        NpgsqlInterval interval= new NpgsqlInterval(0, executeInt, 0);
        
        
        const string sqlCommand = "INSERT INTO task(create_date, execution_period, person_id, contract_id, serial_number, priority_id, receipt_id, status_id, author_id, executor_id) VALUES (now(),@int , @personId, @contractId, @serialNum,@priorityId, @receiptId, @statusId, @aId, @eId);";
        NpgsqlCommand command = new NpgsqlCommand(sqlCommand, sqlConnector.GetConnection());
        command.Parameters.AddWithValue("int", NpgsqlDbType.Interval, interval);
        command.Parameters.AddWithValue("personId", NpgsqlDbType.Integer, personId);
        command.Parameters.AddWithValue("contractId", NpgsqlDbType.Integer, (object)contractId ?? DBNull.Value);
        command.Parameters.AddWithValue("serialNum", NpgsqlDbType.Bigint, (object)serialNumber ?? DBNull.Value);
        command.Parameters.AddWithValue("priorityId", NpgsqlDbType.Integer, priorityId);
        command.Parameters.AddWithValue("receiptId", NpgsqlDbType.Integer, receiptId);
        command.Parameters.AddWithValue("statusId", NpgsqlDbType.Integer, statusId);
        command.Parameters.AddWithValue("aId", NpgsqlDbType.Bigint, MainWindow._employee.EmployeeId);
        command.Parameters.AddWithValue("eId", NpgsqlDbType.Bigint, (object) executorId ?? DBNull.Value);
       

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