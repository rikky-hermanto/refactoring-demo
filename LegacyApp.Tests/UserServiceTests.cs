using System;
using Xunit;
using Moq;

namespace LegacyApp.Tests
{
    public class UserServiceTests
    {
        [Theory]
        [InlineData("John", "Doe", "john.doe@example.com", "2000-01-01", 1, true)]
        [InlineData("", "Doe", "john.doe@example.com", "2000-01-01", 1, false)]
        [InlineData("John", "", "john.doe@example.com", "2000-01-01", 1, false)]
        [InlineData("John", "Doe", "johndoeexample.com", "2000-01-01", 1, false)]
        [InlineData("John", "Doe", "john.doe@example.com", "2023-05-13", 1, false)]
        [InlineData("John", "Doe", "john.doe@example.com", "2000-01-01", 2, true)]
        [InlineData("John", "Doe", "john.doe@example.com", "2000-01-01", 3, false)]
        public void AddUser_ShouldReturnExpectedResult(string firstname, string surname, string email, string dateOfBirth, int clientId, bool expectedResult)
        {
            // Arrange
            var userService = new UserService();
            var parsedDateOfBirth = DateTime.Parse(dateOfBirth);

            // Act
            var result = userService.AddUser(firstname, surname, email, parsedDateOfBirth, clientId);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void AddUser_WithInvalidData_ReturnsFalse()
        {
            // Arrange
            var firstName = "John";
            var surname = "Doe";
            var email = "john.doe.example.com"; // Invalid email address
            var dateOfBirth = new DateTime(2005, 1, 1); // Under 21 years old
            var clientId = 1;

            var userService = new UserService();

            // Act
            var result = userService.AddUser(firstName, surname, email, dateOfBirth, clientId);

            // Assert
            Assert.False(result);
        }
    }
}
