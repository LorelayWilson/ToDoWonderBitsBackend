using System;
using Xunit;
using Moq;
using AutoMapper;
using ToDoWonderBitsBackend.Application.Handlers;
using ToDoWonderBitsBackend.Domain.Ports;
using ToDoWonderBitsBackend.Domain.Models;
using ToDoWonderBitsBackend.Domain.Dtos;
using System.Threading.Tasks;

public class UserCommandHandlerTests
{
    private readonly Mock<IUserRepository> _mockUserRepository;
    private readonly Mock<IMapper> _mockMapper;
    private readonly UserCommandHandler _handler;

    public UserCommandHandlerTests()
    {
        _mockUserRepository = new Mock<IUserRepository>();
        _mockMapper = new Mock<IMapper>();
        _handler = new UserCommandHandler(_mockUserRepository.Object, _mockMapper.Object);
    }

    [Fact]
    public async Task CreateUserAsync_CreatesUserSuccessfully()
    {
        // Arrange
        var userDto = new UserCreateDto { Username = "newuser", Password = "password123" };
        var user = new User { Username = "newuser", Password = "hashedpassword" };

        _mockMapper.Setup(m => m.Map<User>(It.IsAny<UserCreateDto>())).Returns(user);
        _mockUserRepository.Setup(repo => repo.CreateAsync(It.IsAny<User>())).Returns(Task.CompletedTask);

        // Act
        await _handler.CreateUserAsync(userDto);

        // Assert
        _mockUserRepository.Verify(repo => repo.CreateAsync(It.Is<User>(u => u.Username == user.Username && u.Password == user.Password)), Times.Once);
    }

    [Fact]
    public async Task UpdateUserAsync_UpdatesExistingUser()
    {
        // Arrange
        var userDto = new UserUpdateDto { Id = 1, Username = "existinguser", Password = "newpassword" };
        var existingUser = new User { Id = 1, Username = "existinguser", Password = "oldpassword" };

        _mockUserRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(existingUser);
        _mockMapper.Setup(m => m.Map(It.IsAny<UserUpdateDto>(), It.IsAny<User>())).Callback<UserUpdateDto, User>((dto, user) => user.Password = dto.Password);

        // Act
        await _handler.UpdateUserAsync(userDto);

        // Assert
        _mockUserRepository.Verify(repo => repo.UpdateAsync(It.Is<User>(u => u.Id == 1 && u.Password == "newpassword")), Times.Once);
    }

    [Fact]
    public async Task UpdateUserAsync_ThrowsWhenUserNotFound()
    {
        // Arrange
        var userDto = new UserUpdateDto { Id = 99, Username = "nonexisting", Password = "newpassword" };
        _mockUserRepository.Setup(repo => repo.GetByIdAsync(99)).ReturnsAsync((User)null);

        // Act & Assert
        await Assert.ThrowsAsync<KeyNotFoundException>(() => _handler.UpdateUserAsync(userDto));
    }
}
