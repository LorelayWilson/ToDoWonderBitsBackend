using System.Threading.Tasks;
using Moq;
using Xunit;
using ToDoWonderBitsBackend.Application.Handlers;
using ToDoWonderBitsBackend.Domain.Ports;
using ToDoWonderBitsBackend.Domain.Models;
using Microsoft.Extensions.Configuration;
using BCrypt.Net;

public class UserQueryHandlerTests
{
    private readonly Mock<IUserRepository> _mockUserRepository;
    private readonly Mock<IConfiguration> _mockConfiguration;
    private readonly UserQueryHandler _handler;

    public UserQueryHandlerTests()
    {
        _mockUserRepository = new Mock<IUserRepository>();
        _mockConfiguration = new Mock<IConfiguration>();
        _handler = new UserQueryHandler(_mockUserRepository.Object, _mockConfiguration.Object);

        _mockConfiguration.Setup(c => c["Jwt:Key"]).Returns("super_secret_key_here");
        _mockConfiguration.Setup(c => c["Jwt:Issuer"]).Returns("TestIssuer");
        _mockConfiguration.Setup(c => c["Jwt:Audience"]).Returns("TestAudience");
    }

    [Fact]
    public async Task Login_ValidCredentials_ReturnsToken()
    {
        // Arrange
        var user = new User { Id = 1, Username = "test", Password = BCrypt.Net.BCrypt.HashPassword("password") };
        _mockUserRepository.Setup(repo => repo.GetByUsernameAsync("test")).ReturnsAsync(user);

        // Act
        var result = await _handler.Login("test", "password");

        // Assert
        Assert.NotNull(result);
        Assert.IsType<string>(result);
    }

    [Fact]
    public async Task Login_InvalidUsername_ThrowsArgumentException()
    {
        // Arrange
        _mockUserRepository.Setup(repo => repo.GetByUsernameAsync("unknown")).ReturnsAsync((User)null);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => _handler.Login("unknown", "password"));
    }

    [Fact]
    public async Task Login_InvalidPassword_ThrowsArgumentException()
    {
        // Arrange
        var user = new User { Id = 1, Username = "test", Password = BCrypt.Net.BCrypt.HashPassword("password") };
        _mockUserRepository.Setup(repo => repo.GetByUsernameAsync("test")).ReturnsAsync(user);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => _handler.Login("test", "wrong_password"));
    }
}
