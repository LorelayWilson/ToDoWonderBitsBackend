using System.Threading.Tasks;
using Moq;
using Xunit;
using AutoMapper;
using ToDoWonderBitsBackend.Application.Services;
using ToDoWonderBitsBackend.Domain.Dtos;
using ToDoWonderBitsBackend.Domain.Models;
using ToDoWonderBitsBackend.Domain.Ports;

public class TodoItemCommandHandlerTests
{
    private readonly Mock<ITodoItemRepository> _mockTodoItemRepository;
    private readonly Mock<IMapper> _mockMapper;
    private readonly TodoItemCommandHandler _handler;

    public TodoItemCommandHandlerTests()
    {
        _mockTodoItemRepository = new Mock<ITodoItemRepository>();
        _mockMapper = new Mock<IMapper>();
        _handler = new TodoItemCommandHandler(_mockTodoItemRepository.Object, _mockMapper.Object);
    }

    [Fact]
    public async Task CreateItemAsync_SavesItemCorrectly()
    {
        // Arrange
        var todoItemDto = new TodoItemCreateDto { Description = "Test Description" };
        var todoItem = new TodoItem { Description = "Test Description" };
        _mockMapper.Setup(m => m.Map<TodoItem>(todoItemDto)).Returns(todoItem);
        _mockTodoItemRepository.Setup(repo => repo.CreateAsync(todoItem)).Returns(Task.CompletedTask);

        // Act
        var result = await _handler.CreateItemAsync(todoItemDto);

        // Assert
        Assert.NotNull(result);
        _mockTodoItemRepository.Verify(repo => repo.CreateAsync(todoItem), Times.Once);
    }

    [Fact]
    public async Task UpdateItemAsync_ItemExists_UpdatesSuccessfully()
    {
        // Arrange
        var todoItemDto = new TodoItemUpdateDto { Id = 1, Description = "Updated Description" };
        var existingItem = new TodoItem { Id = 1, Description = "Old Description" };
        _mockTodoItemRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(existingItem);
        _mockMapper.Setup(m => m.Map(todoItemDto, existingItem));

        // Act
        await _handler.UpdateItemAsync(todoItemDto);

        // Assert
        _mockTodoItemRepository.Verify(repo => repo.UpdateAsync(existingItem), Times.Once);
    }

    [Fact]
    public async Task DeleteItemAsync_ItemExists_DeletesSuccessfully()
    {
        // Arrange
        var todoItem = new TodoItem { Id = 1 };
        _mockTodoItemRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(todoItem);

        // Act
        await _handler.DeleteItemAsync(1);

        // Assert
        _mockTodoItemRepository.Verify(repo => repo.DeleteAsync(1), Times.Once);
    }
}
