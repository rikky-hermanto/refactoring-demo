using System;
using Xunit;

namespace LegacyApp.Tests
{
    public class UserValidatorTests
    {
        [Fact]
        public void Validate_Should_Return_False_For_User_With_Empty_Firstname()
        {
            // Arrange
            var user = new User
            {
                Firstname = "",
                Surname = "Doe",
                EmailAddress = "john.doe@example.com",
                DateOfBirth = new DateTime(1990, 1, 1)
            };
            var validator = new UserValidator();

            // Act
            var result = validator.Validate(user);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Validate_Should_Return_False_For_User_With_Empty_Surname()
        {
            // Arrange
            var user = new User
            {
                Firstname = "John",
                Surname = "",
                EmailAddress = "john.doe@example.com",
                DateOfBirth = new DateTime(1990, 1, 1)
            };
            var validator = new UserValidator();

            // Act
            var result = validator.Validate(user);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Validate_Should_Return_False_For_User_With_Invalid_Email()
        {
            // Arrange
            var user = new User
            {
                Firstname = "John",
                Surname = "Doe",
                EmailAddress = "invalid-email",
                DateOfBirth = new DateTime(1990, 1, 1)
            };
            var validator = new UserValidator();

            // Act
            var result = validator.Validate(user);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Validate_Should_Return_False_For_User_Under_21_Years_Old()
        {
            // Arrange
            var user = new User
            {
                Firstname = "John",
                Surname = "Doe",
                EmailAddress = "john.doe@example.com",
                DateOfBirth = DateTime.Now.AddYears(-20)
            };
            var validator = new UserValidator();

            // Act
            var result = validator.Validate(user);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Validate_Should_Return_True_For_Valid_User()
        {
            // Arrange
            var user = new User
            {
                Firstname = "John",
                Surname = "Doe",
                EmailAddress = "john.doe@example.com",
                DateOfBirth = new DateTime(1990, 1, 1)
            };
            var validator = new UserValidator();

            // Act
            var result = validator.Validate(user);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Validate_Should_Return_False_For_Null_User()
        {
            // Arrange
            User user = null;
            var validator = new UserValidator();

            // Act
            var result = validator.Validate(user);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void CalculateAge_Should_Return_Correct_Age()
        {
            // Arrange
            var currentDate = new DateTime(2023, 5, 15);
            var validator = new UserValidator(currentDate);
            var dateOfBirth = new DateTime(1990, 1, 1);
            var expectedAge = 33;

            // Act
            var result = validator.CalculateAge(dateOfBirth);

            // Assert
            Assert.Equal(expectedAge, result);
        }
    }
}
