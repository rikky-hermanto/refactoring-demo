using LegacyApp;
using Moq;
using System;
using Xunit;

namespace LegacyApp.Tests
{
    public class UserServiceTests
    {
        private readonly Mock<IClientRepository> _clientRepositoryMock = new Mock<IClientRepository>();
        private readonly Mock<IUserRepository> _userRepositoryMock = new Mock<IUserRepository>();
        private readonly Mock<IUserValidator> _userValidatorMock = new Mock<IUserValidator>();
        private readonly Mock<IUserValidator> _creditLimitValidatorMock = new Mock<IUserValidator>();
        private readonly Mock<ICreditLimitProvider> _creditLimitProviderMock = new Mock<ICreditLimitProvider>();

        [Fact]
        public void AddUser_WithValidUser_ReturnsTrue()
        {
            // Arrange
            var userService = new UserService(_clientRepositoryMock.Object, _userRepositoryMock.Object, _creditLimitProviderMock.Object,
                _userValidatorMock.Object, _creditLimitValidatorMock.Object);
            var user = new User
            {
                DateOfBirth = new DateTime(1990, 1, 1),
                EmailAddress = "test@example.com",
                Firstname = "John",
                Surname = "Doe",
                Client = new Client()
            };
            _clientRepositoryMock.Setup(x => x.GetById(It.IsAny<int>())).Returns(user.Client);
            _userValidatorMock.Setup(x => x.Validate(It.IsAny<User>())).Returns(true);
            _creditLimitProviderMock.Setup(x => x.ApplyCreditLimit(It.IsAny<User>(), It.IsAny<Client>()));
            _creditLimitValidatorMock.Setup(x => x.Validate(It.IsAny<User>())).Returns(true);
            _userRepositoryMock.Setup(x => x.AddUser(It.IsAny<User>()));

            // Act
            var result = userService.AddUser(user.Firstname, user.Surname, user.EmailAddress, user.DateOfBirth, 1);

            // Assert
            Assert.True(result);
            _clientRepositoryMock.Verify(x => x.GetById(It.IsAny<int>()), Times.Once);
            _userValidatorMock.Verify(x => x.Validate(It.IsAny<User>()), Times.Once);
            _creditLimitProviderMock.Verify(x => x.ApplyCreditLimit(It.IsAny<User>(), It.IsAny<Client>()), Times.Once);
            _creditLimitValidatorMock.Verify(x => x.Validate(It.IsAny<User>()), Times.Once);
            _userRepositoryMock.Verify(x => x.AddUser(It.IsAny<User>()), Times.Once);
        }

        [Fact]
        public void AddUser_WithInvalidUser_ReturnsFalse()
        {
            // Arrange
            var userService = new UserService(_clientRepositoryMock.Object, _userRepositoryMock.Object, _creditLimitProviderMock.Object,
                _userValidatorMock.Object, _creditLimitValidatorMock.Object);
            var user = new User();
            _userValidatorMock.Setup(x => x.Validate(It.IsAny<User>())).Returns(false);

            // Act
            var result = userService.AddUser(user.Firstname, user.Surname, user.EmailAddress, user.DateOfBirth, 1);

            // Assert
            Assert.False(result);
            _clientRepositoryMock.Verify(x => x.GetById(It.IsAny<int>()), Times.Never);
            _userValidatorMock.Verify(x => x.Validate(It.IsAny<User>()), Times.Once);
            _creditLimitProviderMock.Verify(x => x.ApplyCreditLimit(It.IsAny<User>(), It.IsAny<Client>()), Times.Never);
            _creditLimitValidatorMock.Verify(x => x.Validate(It.IsAny<User>()), Times.Never);
            _userRepositoryMock.Verify(x => x.AddUser(It.IsAny<User>()), Times.Never);
        }

        [Fact]
        public void AddUser_WithInvalidCreditLimit_ReturnsFalse()
        {
            // Arrange
            var userService = new UserService(_clientRepositoryMock.Object, _userRepositoryMock.Object, _creditLimitProviderMock.Object, 
                _userValidatorMock.Object, _creditLimitValidatorMock.Object);
            var user = new User
            {
                DateOfBirth = new DateTime(1990, 1, 1),
                EmailAddress = "test@example.com",
                Firstname = "John",
                Surname = "Doe",
                Client = new Client()
            };
            _clientRepositoryMock.Setup(x => x.GetById(It.IsAny<int>())).Returns(user.Client);
            _userValidatorMock.Setup(x => x.Validate(It.IsAny<User>())).Returns(true);
            _creditLimitProviderMock.Setup(x => x.ApplyCreditLimit(It.IsAny<User>(), It.IsAny<Client>()));
            _creditLimitValidatorMock.Setup(x => x.Validate(It.IsAny<User>())).Returns(false);

            // Act
            var result = userService.AddUser(user.Firstname, user.Surname, user.EmailAddress, user.DateOfBirth, 1);

            // Assert
            Assert.False(result);
            _clientRepositoryMock.Verify(x => x.GetById(It.IsAny<int>()), Times.Once);
            _userValidatorMock.Verify(x => x.Validate(It.IsAny<User>()), Times.Once);
            _creditLimitProviderMock.Verify(x => x.ApplyCreditLimit(It.IsAny<User>(), It.IsAny<Client>()), Times.Once);
            _creditLimitValidatorMock.Verify(x => x.Validate(It.IsAny<User>()), Times.Once);
            _userRepositoryMock.Verify(x => x.AddUser(It.IsAny<User>()), Times.Never);
        }
    }
}