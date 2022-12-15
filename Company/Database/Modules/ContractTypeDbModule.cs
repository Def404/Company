using System;
using System.Collections.Generic;
using System.Windows;
using Company.Database.Models;
using Npgsql;

namespace Company.Database.Modules;

public class ContractTypeDbModule{
    public List<ContractType> GetContractTypeList(){
        List<ContractType> contractTypes = new List<ContractType>();
        EmployeeSqlConnector sqlConnector = new EmployeeSqlConnector();

        const string sqlCommand = "SELECT * FROM contract_classifier;";
        NpgsqlCommand command = new NpgsqlCommand(sqlCommand, sqlConnector.GetConnection());

        try{
            sqlConnector.OpenConnection();
            NpgsqlDataReader reader = command.ExecuteReader();

            while (reader.Read()){
                contractTypes.Add(new ContractType(
                    Int32.Parse(reader["type_id"].ToString()), 
                    reader["type_name"].ToString()
                ));
            }
            sqlConnector.CloseConnection();
        }
        catch (Exception e){
            MessageBox.Show(e.Message);
        }

        return contractTypes;
    }
}