using Moq;
using ToDoWonderBitsBackend.Domain.Models;
using ToDoWonderBitsBackend.Domain.Ports;
using ToDoWonderBitsBackend.Domain.Services;
using Xunit;


namespace ToDoWonderBitsBackend.Tests
{
    public class TodoServiceTests
    {
        private readonly Mock<ITodoItemRepository> _mockRepo;
        private readonly TodoService _service;

        public TodoServiceTests()
        {
            // Mock del repositorio para ser utilizado en todas las pruebas
            _mockRepo = new Mock<ITodoItemRepository>();
            _service = new TodoService(_mockRepo.Object);
        }

        /// <summary>
        /// Test para asegurar que GetAllItemsAsync devuelve todos los items existentes.
        /// </summary>
        [Fact]
        public async Task GetAllItemsAsync_ReturnsAllItems()
        {
            // Arrange
            var todos = new List<TodoItem> { new TodoItem { Id = 1, Name = "Test", IsComplete = false } };
            _mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(todos);

            // Act
            var result = await _service.GetAllItemsAsync();

            // Assert
            Assert.Single(result);
            Assert.Equal("Test", result.First().Name);
        }

        /// <summary>
        /// Test para asegurar que GetItemByIdAsync devuelve el item correcto por su ID.
        /// </summary>
        [Fact]
        public async Task GetItemByIdAsync_ReturnsItem()
        {
            // Arrange
            var todo = new TodoItem { Id = 1, Name = "Test", IsComplete = false };
            _mockRepo.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(todo);

            // Act
            var result = await _service.GetItemByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Test", result.Name);
        }

        /// <summary>
        /// Test para verificar que AddItemAsync agrega correctamente un nuevo item.
        /// </summary>
        [Fact]
        public async Task AddItemAsync_AddsItem()
        {
            // Arrange
            var todo = new TodoItem { Name = "New Task", IsComplete = false };
            _mockRepo.Setup(repo => repo.AddAsync(It.IsAny<TodoItem>())).Returns(Task.CompletedTask).Verifiable();

            // Act
            await _service.AddItemAsync(todo);

            // Assert
            _mockRepo.Verify(repo => repo.AddAsync(It.IsAny<TodoItem>()), Times.Once);
        }

        /// <summary>
        /// Test para verificar que UpdateItemAsync actualiza correctamente un item existente.
        /// </summary>
        [Fact]
        public async Task UpdateItemAsync_UpdatesItem()
        {
            // Arrange
            var existingItem = new TodoItem { Id = 1, Name = "Test", IsComplete = false };
            _mockRepo.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(existingItem);
            _mockRepo.Setup(repo => repo.UpdateAsync(It.IsAny<TodoItem>())).Returns(Task.CompletedTask).Verifiable();

            // Act
            await _service.UpdateItemAsync(new TodoItem { Id = 1, Name = "Updated Test", IsComplete = true });

            // Assert
            _mockRepo.Verify(repo => repo.UpdateAsync(It.IsAny<TodoItem>()), Times.Once);
        }

        /// <summary>
        /// Test para verificar que DeleteItemAsync elimina correctamente un item.
        /// </summary>
        [Fact]
        public async Task DeleteItemAsync_DeletesItem()
        {
            // Arrange
            var existingItem = new TodoItem { Id = 1, Name = "Test", IsComplete = false };
            _mockRepo.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(existingItem);
            _mockRepo.Setup(repo => repo.DeleteAsync(1)).Returns(Task.CompletedTask).Verifiable();

            // Act
            await _service.DeleteItemAsync(1);

            // Assert
            _mockRepo.Verify(repo => repo.DeleteAsync(1), Times.Once);
        }
    }
}
