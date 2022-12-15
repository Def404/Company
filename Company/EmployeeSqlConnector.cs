using System.Configuration;
using System.Windows;
using Npgsql;

namespace Company;

public class EmployeeSqlConnector{
    private static string _connectionStr = ConfigurationManager.ConnectionStrings["UserConnection"].ConnectionString;
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

    public bool UpdateConnectionString(string connectionString){
        bool result = false;
        try{
            var confFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var consSet = confFile.ConnectionStrings.ConnectionStrings;
            var newCntStr = connectionString;
            consSet["UserConnection"].ConnectionString = newCntStr;
            confFile.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection(confFile.ConnectionStrings.SectionInformation.Name);
            result = true;
        }
        catch (ConfigurationErrorsException e){
            result = false;
        }

        return  result;
    }
}