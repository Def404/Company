using System.Configuration;
using System.Windows;
using Npgsql;

namespace Company;

public class AuthConnector{
    private static string _connectionStr = ConfigurationManager.ConnectionStrings["AuthConnection"].ConnectionString;
    private readonly NpgsqlConnection _connection = new NpgsqlConnection(_connectionStr);

    public NpgsqlConnection GetConnection(){

        return _connection;
    }

    public void OpenConnection(){
        try{
            _connection.Open();
        }
        catch (NpgsqlException e){

            MessageBox.Show(e.Message);
        }
    }

    public void CloseConnection(){
        try{
            _connection.Close();
        }
        catch (NpgsqlException e){
            MessageBox.Show(e.Message);
        }
    }
}