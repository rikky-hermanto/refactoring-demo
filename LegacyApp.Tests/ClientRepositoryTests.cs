
using System.Configuration;
using System.Data.SqlClient;

namespace LegacyApp.Tests
{
    public class ClientRepositoryTests
    {
        [Fact]
        public void Can_Connect_To_Database()
        {
            var connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=LegacyApp;User ID=legacyuser;Password=pass123";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                Assert.True(connection.State == System.Data.ConnectionState.Open);
            }
        }


        [Fact]
        public void Can_Read_AppConfig_and_ConnectToDb()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["appDatabase"].ConnectionString;

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                Assert.True(connection.State == System.Data.ConnectionState.Open);
            }
        }
 
    }
}