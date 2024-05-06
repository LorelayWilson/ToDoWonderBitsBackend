using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using Xunit;
using ToDoWonderBitsBackend.Application.Services;
using ToDoWonderBitsBackend.Domain.Models;
using ToDoWonderBitsBackend.Domain.Ports;

public class TodoItemQueryHandlerTests
{
    private readonly Mock<ITodoItemRepository> _mockTodoItemRepository;
    private readonly TodoItemQueryHandler _handler;

    public TodoItemQueryHandlerTests()
    {
        _mockTodoItemRepository = new Mock<ITodoItemRepository>();
        _handler = new TodoItemQueryHandler(_mockTodoItemRepository.Object);
    }

    [Fact]
    public async Task GetAllItemsAsync_ReturnsAllItems()
    {
        // Arrange
        var todoItems = new List<TodoItem> { new TodoItem(), new TodoItem() };
        _mockTodoItemRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(todoItems);

        // Act
        var result = await _handler.GetAllItemsAsync();

        // Assert
        Assert.Equal(2, result.Count()); // Asegúrate de usar result.Count() si result es una colección
    }


    [Fact]
    public async Task GetItemByIdAsync_ItemExists_ReturnsItem()
    {
        // Arrange
        var todoItem = new TodoItem { Id = 1 };
        _mockTodoItemRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(todoItem);

        // Act
        var result = await _handler.GetItemByIdAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
    }

    [Fact]
    public async Task GetItemByIdAsync_ItemDoesNotExist_ThrowsKeyNotFoundException()
    {
        // Arrange
        _mockTodoItemRepository.Setup(repo => repo.GetByIdAsync(99)).ReturnsAsync((TodoItem)null);

        // Act & Assert
        await Assert.ThrowsAsync<KeyNotFoundException>(() => _handler.GetItemByIdAsync(99));
    }
}
