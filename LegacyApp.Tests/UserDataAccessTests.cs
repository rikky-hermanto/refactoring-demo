using System;
using System.Configuration;
using System.Data.SqlClient;
using Xunit;

namespace LegacyApp.Tests
{
    public class UserDataAccessTests
    {
        [Fact]
        public void AddUser_InsertsUserIntoDatabase()
        {
            // Arrange
            var user = new User
            {
                Firstname = "John",
                Surname = "Doe",
                DateOfBirth = new DateTime(1985, 1, 1),
                EmailAddress = "johndoe@example.com",
                HasCreditLimit = true,
                CreditLimit = 5000,
                Client = new Client { Id = 1 }
            };

            var connectionString = ConfigurationManager.ConnectionStrings["appDatabase"].ConnectionString;
            var connection = new SqlConnection(connectionString);

            // Act
            var userDataAccess = new UserDataAccess();
            userDataAccess.AddUser(user);

            // Assert
            connection.Open();
            var command = new SqlCommand("SELECT COUNT(*) FROM Users", connection);
            var count = (int)command.ExecuteScalar();
            connection.Close();

            Assert.Equal(1, count);
        }
    }
}
