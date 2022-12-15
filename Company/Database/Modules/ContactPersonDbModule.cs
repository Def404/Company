using System;
using System.Collections.Generic;
using System.Windows;
using Company.Database.Models;
using Npgsql;
using NpgsqlTypes;

namespace Company.Database.Modules;

public class ContactPersonDbModule{
    public List<ContactPerson> GetContactPersonList(int organizationId){
        List<ContactPerson> contactPersons = new List<ContactPerson>();
        EmployeeSqlConnector sqlConnector = new EmployeeSqlConnector();

        const string sqlCommand = "SELECT person_id,  full_name FROM contact_person WHERE organization_id  = @oid;";
        NpgsqlCommand command = new NpgsqlCommand(sqlCommand, sqlConnector.GetConnection());
        command.Parameters.AddWithValue("oid", NpgsqlDbType.Integer, organizationId);

        try{
            sqlConnector.OpenConnection();
            NpgsqlDataReader reader = command.ExecuteReader();

            while (reader.Read()){
                contactPersons.Add(new ContactPerson(
                    Int32.Parse(reader["person_id"].ToString()), 
                    reader["full_name"].ToString()
                    ));
            }
            sqlConnector.CloseConnection();
        }
        catch (Exception e){
            MessageBox.Show(e.Message);
        }

        return contactPersons;
    }
}