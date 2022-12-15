using System;
using System.Collections.Generic;
using System.Windows;
using Company.Database.Models;
using Npgsql;

namespace Company.Database.Modules;

public class StatisticDbModule{
    public Statistic GetStatisticList(){
        Statistic statistics = null;
        EmployeeSqlConnector sqlConnector = new EmployeeSqlConnector();

        const string sqlCommand = "SELECT * FROM select_statistic;";
        NpgsqlCommand command = new NpgsqlCommand(sqlCommand, sqlConnector.GetConnection());

        try{
            sqlConnector.OpenConnection();
            NpgsqlDataReader reader = command.ExecuteReader();

            while (reader.Read()){
                statistics = new Statistic(
                    Int32.Parse(reader["all_tasks_cnt"].ToString()), 
                    Int32.Parse(reader["tasks_cmplt_on_time_cnt"].ToString()), 
                    Int32.Parse(reader["tasks_cmplt_out_time_cnt"].ToString()), 
                    Int32.Parse(reader["tasks_not_cmplt_out_time_cnt"].ToString()), 
                    Int32.Parse(reader["tasks_not_cmplt_on_time_cnt"].ToString()));
            }
            sqlConnector.CloseConnection();
        }
        catch (Exception e){
            MessageBox.Show(e.Message);
        }

        return statistics;
    }
}