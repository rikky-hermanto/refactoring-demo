using LegacyApp;
using Xunit;

namespace LegacyApp.Tests
{
    public class CreditLimitValidatorTests
    {
        [Fact]
        public void Validate_ReturnsTrue_WhenUserHasNoCreditLimit()
        {
            // Arrange
            var user = new User { Firstname = "John", Surname = "Doe", EmailAddress = "john.doe@example.com", DateOfBirth = new DateTime(1990, 1, 1), HasCreditLimit = false };

            var validator = new CreditLimitValidator();

            // Act
            var result = validator.Validate(user);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Validate_ReturnsTrue_WhenUserHasCreditLimitGreaterThan500()
        {
            // Arrange
            var user = new User { Firstname = "John", Surname = "Doe", EmailAddress = "john.doe@example.com", DateOfBirth = new DateTime(1990, 1, 1), HasCreditLimit = true, CreditLimit = 1000 };

            var validator = new CreditLimitValidator();

            // Act
            var result = validator.Validate(user);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Validate_ReturnsFalse_WhenUserHasCreditLimitLessThan500()
        {
            // Arrange
            var user = new User { Firstname = "John", Surname = "Doe", EmailAddress = "john.doe@example.com", DateOfBirth = new DateTime(1990, 1, 1), HasCreditLimit = true, CreditLimit = 200 };

            var validator = new CreditLimitValidator();

            // Act
            var result = validator.Validate(user);

            // Assert
            Assert.False(result);
        }
    }
}