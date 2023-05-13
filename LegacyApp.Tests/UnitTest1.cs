using Moq;

namespace LegacyApp.Tests
{
    public class ClientRepositoryTests
    {
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