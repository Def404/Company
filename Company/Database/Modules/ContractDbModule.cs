using System;
using System.Collections.Generic;
using System.Windows;
using Company.Database.Models;
using Npgsql;

namespace Company.Database.Modules;

public class ContractDbModule{
    public List<Contract> GetContractList(){
        List<Contract> contracts = new List<Contract>();
        EmployeeSqlConnector sqlConnector = new EmployeeSqlConnector();

        const string sqlCommand = "SELECT contract_id, info, type_name  FROM contract JOIN contract_classifier cc on cc.type_id = contract.type_id;";
        NpgsqlCommand command = new NpgsqlCommand(sqlCommand, sqlConnector.GetConnection());

        try{
            sqlConnector.OpenConnection();
            NpgsqlDataReader reader = command.ExecuteReader();

            while (reader.Read()){
                contracts.Add(new Contract(
                    Int32.Parse(reader["contract_id"].ToString()), 
                    reader["info"].ToString(),
                    reader["type_name"].ToString()));
            }
            sqlConnector.CloseConnection();
        }
        catch (Exception e){
            MessageBox.Show(e.Message);
        }

        return contracts;
    }
}