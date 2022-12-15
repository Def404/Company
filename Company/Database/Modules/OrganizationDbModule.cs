using System;
using System.Collections.Generic;
using System.Windows;
using Company.Database.Models;
using Npgsql;

namespace Company.Database.Modules;

public class OrganizationDbModule{
    public List<Organization> GetOrganizationList(){
        List<Organization> organizaitions = new List<Organization>();
        EmployeeSqlConnector sqlConnector = new EmployeeSqlConnector();

        const string sqlCommand = "SELECT * FROM organization WHERE type_id = 1";
        NpgsqlCommand command = new NpgsqlCommand(sqlCommand, sqlConnector.GetConnection());

        try{
            sqlConnector.OpenConnection();
            NpgsqlDataReader reader = command.ExecuteReader();

            while (reader.Read()){
                organizaitions.Add(new Organization(
                    Int32.Parse(reader["organization_id"].ToString()), 
                    reader["organization_name"].ToString()));
            }
            sqlConnector.CloseConnection();
        }
        catch (Exception e){
            MessageBox.Show(e.Message);
        }

        return organizaitions;
    }
}