using System.Data.SqlClient;

namespace EmailRegistration.WebService.DbServices
{
    public class MySqlDb
    {
        SqlConnectionStringBuilder _connStringBuilder;
        public string SetConnectionString()
        {
            SettingsService settingsService = new SettingsService();

            _connStringBuilder = new SqlConnectionStringBuilder();
            _connStringBuilder.IntegratedSecurity = true;
            _connStringBuilder.InitialCatalog = "EmailDb";
            _connStringBuilder.DataSource = @"DESKTOP-RH4I9L4\SQLEXPRESS";

            settingsService.ConnectionString = _connStringBuilder.ToString();
            return settingsService.ConnectionString;
        } 
    }
}