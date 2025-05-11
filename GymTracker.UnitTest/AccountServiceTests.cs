using NUnit.Framework;
using GymTracker.Services;
using GymTracker.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Moq;
using GymTracker.Models.Command;
using GymTracker.Data.Models;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Threading.Tasks;
using GymTracker.Models;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace GymTracker.UnitTest
{
    [TestFixture]
    public class AccountServiceTests
    {
        private Mock<ILogger<AccountService>> _loggerMock;
        private Mock<IConfiguration> _configMock;
        private Mock<IHttpContextAccessor> _httpContextAccessorMock;
        private GymTrackerContext _context;
        private AccountService _accountService;

        [SetUp]
        public void Setup()
        {
            // Setup in-memory database for testing
            var options = new DbContextOptionsBuilder<GymTrackerContext>()
                .UseInMemoryDatabase(databaseName: "TestGymTracker" + System.Guid.NewGuid().ToString())
                .Options;
            _context = new GymTrackerContext(options);

            // Setup mocks
            _loggerMock = new Mock<ILogger<AccountService>>();
            _configMock = new Mock<IConfiguration>();
            _httpContextAccessorMock = new Mock<IHttpContextAccessor>();

            // Create the service to test
            _accountService = new AccountService(
                _context,
                _loggerMock.Object,
                _configMock.Object,
                _httpContextAccessorMock.Object
            );
        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
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

            // Act
            var result = await _accountService.RegisterUserAsync(command);

            // Assert
            Assert.IsTrue(result.Success);
            Assert.IsNull(result.ErrorMessage);

            // Verify user was added to the database
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == command.Email);
            Assert.IsNotNull(user);
        }

        [Test]
        public async Task RegisterUserAsync_WithExistingEmail_ReturnsSuccessFalseAndErrorMessage()
        {
            // Arrange
            var existingUser = new User
            {
                Email = "existing@example.com",
                PasswordHash = "somehash"
            };

            await _context.Users.AddAsync(existingUser);
            await _context.SaveChangesAsync();

            var command = new RegisterUserCommand
            {
                Email = "existing@example.com",
                Password = "password123"
            };

            // Act
            var result = await _accountService.RegisterUserAsync(command);

            // Assert
            Assert.IsFalse(result.Success);
            Assert.IsNotNull(result.ErrorMessage);
            Assert.That(result.ErrorMessage, Does.Contain("ju¿ istnieje"));
        }

        [Test]
        public async Task LoginUserAsync_WithCorrectCredentials_ReturnsSuccessTrue()
        {
            // Arrange
            var email = "test@example.com";
            var password = "password123";

            // Register a user first to get the hashed password
            await _accountService.RegisterUserAsync(new RegisterUserCommand
            {
                Email = email,
                Password = password
            });

            var loginCommand = new LoginCommand
            {
                Email = email,
                Password = password
            };

            // Mamy dwa podejœcia:

            // Opcja 1: Mock SignInAsync - preferowane podejœcie w testach jednostkowych
            var httpContext = new DefaultHttpContext();
            _httpContextAccessorMock.Setup(x => x.HttpContext).Returns(httpContext);

            // Tworzymy mock IAuthenticationService
            var authServiceMock = new Mock<IAuthenticationService>();
            authServiceMock
                .Setup(a => a.SignInAsync(It.IsAny<HttpContext>(), It.IsAny<string>(), It.IsAny<ClaimsPrincipal>(), It.IsAny<AuthenticationProperties>()))
                .Returns(Task.CompletedTask);

            // Dodajemy mock do kolekcji serwisów
            var serviceProviderMock = new Mock<IServiceProvider>();
            serviceProviderMock
                .Setup(sp => sp.GetService(typeof(IAuthenticationService)))
                .Returns(authServiceMock.Object);

            // Ustawiamy ServiceProvider dla HttpContext
            httpContext.RequestServices = serviceProviderMock.Object;

            // Act
            var result = await _accountService.LoginUserAsync(loginCommand);

            // Assert
            Assert.IsTrue(result.Success);
            Assert.IsNull(result.ErrorMessage);
        }
    }
}
