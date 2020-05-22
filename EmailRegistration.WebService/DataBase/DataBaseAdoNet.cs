using System.Data.SqlClient;

namespace EmailRegistration.WebService.DataBase
{
    public class DataBaseAdoNet
    {
        SqlConnection conn;
        SqlCommand comm;
        SqlConnectionStringBuilder connStringBuilder;

        public SqlCommand connectToDb()
        {
            connStringBuilder = new SqlConnectionStringBuilder();
            connStringBuilder.IntegratedSecurity = true;
            connStringBuilder.InitialCatalog = "EmailDb";
            connStringBuilder.DataSource = @"DESKTOP-RH4I9L4\SQLEXPRESS";

            conn = new SqlConnection(connStringBuilder.ToString());
            comm = conn.CreateCommand();
            return comm;
        }
    }
}