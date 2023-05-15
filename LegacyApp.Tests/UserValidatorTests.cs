using System;
using Xunit;

namespace LegacyApp.Tests
{
    public class UserValidatorTests
    {
        [Fact]
        public void Validate_ValidUser_ReturnsTrue()
        {
            // Arrange
            var validator = new UserValidator();
            var user = new User
            {
                Firstname = "John",
                Surname = "Doe",
                EmailAddress = "john.doe@example.com",
                DateOfBirth = new DateTime(1990, 1, 1)
            };

            // Act
            bool result = validator.Validate(user);

            // Assert
            Assert.True(result);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Validate_InvalidName_ReturnsFalse(string name)
        {
            // Arrange
            var validator = new UserValidator();
            var user = new User
            {
                Firstname = name,
                Surname = "Doe",
                EmailAddress = "john.doe@example.com",
                DateOfBirth = new DateTime(1990, 1, 1)
            };

            // Act
            bool result = validator.Validate(user);

            // Assert
            Assert.False(result);
        }

        [Theory]
        [InlineData("john.doe")]
        [InlineData("johndoe@")]
        [InlineData("johndoe@examplecom")]
        public void Validate_InvalidEmail_ReturnsFalse(string email)
        {
            // Arrange
            var validator = new UserValidator();
            var user = new User
            {
                Firstname = "John",
                Surname = "Doe",
                EmailAddress = email,
                DateOfBirth = new DateTime(1990, 1, 1)
            };

            // Act
            bool result = validator.Validate(user);

            // Assert
            Assert.False(result);
        }

        [Theory]
        [InlineData("2010-01-01", false)]
        [InlineData("2000-01-01", true)]
        public void Validate_InvalidAge_ReturnsFalse(string dob, bool expected)
        {
            // Arrange
            var validator = new UserValidator();
            var user = new User
            {
                Firstname = "John",
                Surname = "Doe",
                EmailAddress = "john.doe@example.com",
                DateOfBirth = DateTime.Parse(dob)
            };

            // Act
            bool result = validator.Validate(user);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}
