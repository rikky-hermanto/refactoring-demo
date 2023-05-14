using System;
using Xunit;

namespace LegacyApp.Tests
{
    public class UserCreditServiceClientTests
    {
        [Fact]
        public void GetCreditLimit_ReturnsCreditLimit()
        {
            // Arrange
            var client = new UserCreditServiceClient();

            // Act
            var creditLimit = client.GetCreditLimit("John", "Doe", new DateTime(1980, 1, 1));

            // Assert
            Assert.Equal(500, creditLimit);
        }
    }
}
