
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

        [Fact]
        public void GetById_ShouldReturnClient_WhenClientExists()
        {
            // Arrange
            var client = new Client
            {
                Id = 1,
                Name = "Test Client",
                ClientStatus = ClientStatus.none
            };

            var repository = new ClientRepository();

            // Act
            var result = repository.GetById(client.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(client.Id, result.Id);
            Assert.Equal(client.Name, result.Name);
            Assert.Equal(client.ClientStatus, result.ClientStatus);
        }
    }
}