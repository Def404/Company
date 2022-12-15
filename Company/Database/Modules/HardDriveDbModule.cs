using System;
using System.Collections.Generic;
using System.Windows;
using Company.Database.Models;
using Npgsql;

namespace Company.Database.Modules;

public class HardDriveDbModule{
    public List<HardDrive> GetHardDriveList(){
        List<HardDrive> hardDrives = new List<HardDrive>();
        EmployeeSqlConnector sqlConnector = new EmployeeSqlConnector();

        const string sqlCommand = "SELECT serial_number, drive_name,  drive_size, dt.drive_type_name, cit.interface_name FROM hard_drive hd join connection_interface_type cit on cit.connection_interface_id = hd.connection_interface_id join drive_type dt on dt.drive_type_id = hd.drive_type_id;";
        NpgsqlCommand command = new NpgsqlCommand(sqlCommand, sqlConnector.GetConnection());

        try{
            sqlConnector.OpenConnection();
            NpgsqlDataReader reader = command.ExecuteReader();

            while (reader.Read()){
                hardDrives.Add(new HardDrive(
                    Int32.Parse(reader["serial_number"].ToString()), 
                    reader["drive_name"].ToString(),
                    Int32.Parse(reader["drive_size"].ToString()), 
                    reader["drive_type_name"].ToString(), 
                    reader["interface_name"].ToString()));
            }
            sqlConnector.CloseConnection();
        }
        catch (Exception e){
            MessageBox.Show(e.Message);
        }

        return hardDrives;
    }
}