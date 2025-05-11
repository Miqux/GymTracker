using NUnit.Framework;
using GymTracker.Services;
using GymTracker.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using GymTracker.Models.Command;
using GymTracker.Models;
using System.Threading.Tasks;
using GymTracker.Services.Repositories;
using GymTracker.Services.Security;
using GymTracker.Services.Authentication;
using GymTracker.Data.Models;

namespace GymTracker.UnitTest
{
    [TestFixture]
    public class AccountServiceTests
    {
        private Mock<IUserRepository> _userRepositoryMock;
        private Mock<ILogger<AccountService>> _loggerMock;
        private Mock<IPasswordHasher> _passwordHasherMock;
        private Mock<IAuthenticationService> _authenticationServiceMock;
        private AccountService _accountService;

        [SetUp]
        public void Setup()
        {
            // Setup mocks
            _userRepositoryMock = new Mock<IUserRepository>();
            _loggerMock = new Mock<ILogger<AccountService>>();
            _passwordHasherMock = new Mock<IPasswordHasher>();
            _authenticationServiceMock = new Mock<IAuthenticationService>();

            // Create the service to test
            _accountService = new AccountService(
                _userRepositoryMock.Object,
                _loggerMock.Object,
                _passwordHasherMock.Object,
                _authenticationServiceMock.Object
            );
        }

        [Test]
        public async Task RegisterUserAsync_WithNewEmail_ReturnsSuccessTrue()
        {
            // Arrange
            var command = new RegisterUserCommand
            {
                Email = "newuser@example.com",
                Password = "password123"
            };

            // Setup mocks
            _userRepositoryMock.Setup(repo => repo.UserExistsByEmailAsync(command.Email))
                .ReturnsAsync(false);

            _passwordHasherMock.Setup(hasher => hasher.HashPassword(command.Password))
                .Returns("hashedpassword123");

            _userRepositoryMock.Setup(repo => repo.AddUserAsync(It.IsAny<User>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _accountService.RegisterUserAsync(command);

            // Assert
            Assert.IsTrue(result.Success);
            Assert.IsNull(result.ErrorMessage);

            // Verify interactions with dependencies
            _userRepositoryMock.Verify(repo => repo.UserExistsByEmailAsync(command.Email), Times.Once);
            _passwordHasherMock.Verify(hasher => hasher.HashPassword(command.Password), Times.Once);
            _userRepositoryMock.Verify(repo => repo.AddUserAsync(
                It.Is<User>(u => u.Email == command.Email && u.PasswordHash == "hashedpassword123")),
                Times.Once);
        }

        [Test]
        public async Task RegisterUserAsync_WithExistingEmail_ReturnsSuccessFalseAndErrorMessage()
        {
            // Arrange
            var command = new RegisterUserCommand
            {
                Email = "existing@example.com",
                Password = "password123"
            };

            // Setup mocks
            _userRepositoryMock.Setup(repo => repo.UserExistsByEmailAsync(command.Email))
                .ReturnsAsync(true);

            // Act
            var result = await _accountService.RegisterUserAsync(command);

            // Assert
            Assert.IsFalse(result.Success);
            Assert.IsNotNull(result.ErrorMessage);
            Assert.That(result.ErrorMessage, Does.Contain("ju¿ istnieje"));

            // Verify repository was called but user wasn't added
            _userRepositoryMock.Verify(repo => repo.UserExistsByEmailAsync(command.Email), Times.Once);
            _userRepositoryMock.Verify(repo => repo.AddUserAsync(It.IsAny<User>()), Times.Never);
        }

        [Test]
        public async Task RegisterUserAsync_WithEmptyEmail_ReturnsSuccessFalse()
        {
            // Arrange
            var command = new RegisterUserCommand
            {
                Email = "",
                Password = "password123"
            };

            // Act
            var result = await _accountService.RegisterUserAsync(command);

            // Assert
            Assert.IsFalse(result.Success);
            Assert.IsNotNull(result.ErrorMessage);

            // Repository should never be called
            _userRepositoryMock.Verify(repo => repo.UserExistsByEmailAsync(It.IsAny<string>()), Times.Never);
            _userRepositoryMock.Verify(repo => repo.AddUserAsync(It.IsAny<User>()), Times.Never);
        }

        [Test]
        public async Task LoginUserAsync_WithCorrectCredentials_ReturnsSuccessTrue()
        {
            // Arrange
            var email = "test@example.com";
            var password = "password123";
            var hashedPassword = "hashedpassword123";

            var user = new User
            {
                Id = 1,
                Email = email,
                PasswordHash = hashedPassword
            };

            var loginCommand = new LoginCommand
            {
                Email = email,
                Password = password
            };

            // Setup mocks
            _userRepositoryMock.Setup(repo => repo.GetUserByEmailAsync(email))
                .ReturnsAsync(user);

            _passwordHasherMock.Setup(hasher => hasher.VerifyPassword(password, hashedPassword))
                .Returns(true);

            _authenticationServiceMock.Setup(auth => auth.SignInUserAsync(user))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _accountService.LoginUserAsync(loginCommand);

            // Assert
            Assert.IsTrue(result.Success);
            Assert.IsNull(result.ErrorMessage);

            // Verify interactions
            _userRepositoryMock.Verify(repo => repo.GetUserByEmailAsync(email), Times.Once);
            _passwordHasherMock.Verify(hasher => hasher.VerifyPassword(password, hashedPassword), Times.Once);
            _authenticationServiceMock.Verify(auth => auth.SignInUserAsync(user), Times.Once);
        }

        [Test]
        public async Task LoginUserAsync_WithIncorrectPassword_ReturnsSuccessFalse()
        {
            // Arrange
            var email = "test@example.com";
            var password = "wrongpassword";
            var hashedPassword = "hashedpassword123";

            var user = new User
            {
                Id = 1,
                Email = email,
                PasswordHash = hashedPassword
            };

            var loginCommand = new LoginCommand
            {
                Email = email,
                Password = password
            };

            // Setup mocks
            _userRepositoryMock.Setup(repo => repo.GetUserByEmailAsync(email))
                .ReturnsAsync(user);

            _passwordHasherMock.Setup(hasher => hasher.VerifyPassword(password, hashedPassword))
                .Returns(false);

            // Act
            var result = await _accountService.LoginUserAsync(loginCommand);

            // Assert
            Assert.IsFalse(result.Success);
            Assert.IsNotNull(result.ErrorMessage);
            Assert.That(result.ErrorMessage, Does.Contain("Nieprawid³owy email lub has³o"));

            // Verify interactions
            _userRepositoryMock.Verify(repo => repo.GetUserByEmailAsync(email), Times.Once);
            _passwordHasherMock.Verify(hasher => hasher.VerifyPassword(password, hashedPassword), Times.Once);
            // Authentication service should not be called for failed login
            _authenticationServiceMock.Verify(auth => auth.SignInUserAsync(It.IsAny<User>()), Times.Never);
        }

        [Test]
        public async Task LoginUserAsync_WithNonexistentUser_ReturnsSuccessFalse()
        {
            // Arrange
            var loginCommand = new LoginCommand
            {
                Email = "nonexistent@example.com",
                Password = "password123"
            };

            // Setup mocks - user doesn't exist
            _userRepositoryMock.Setup(repo => repo.GetUserByEmailAsync(loginCommand.Email))
                .ReturnsAsync((User)null);

            // Act
            var result = await _accountService.LoginUserAsync(loginCommand);

            // Assert
            Assert.IsFalse(result.Success);
            Assert.IsNotNull(result.ErrorMessage);
            Assert.That(result.ErrorMessage, Does.Contain("Nieprawid³owy email lub has³o"));

            // Verify repository was called but no password verification or authentication
            _userRepositoryMock.Verify(repo => repo.GetUserByEmailAsync(loginCommand.Email), Times.Once);
            _passwordHasherMock.Verify(hasher => hasher.VerifyPassword(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
            _authenticationServiceMock.Verify(auth => auth.SignInUserAsync(It.IsAny<User>()), Times.Never);
        }
    }
}
