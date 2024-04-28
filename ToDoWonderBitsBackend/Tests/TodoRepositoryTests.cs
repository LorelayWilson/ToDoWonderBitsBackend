using Microsoft.EntityFrameworkCore;
using ToDoWonderBitsBackend.Domain.Models;
using Xunit;

namespace ToDoWonderBitsBackend.Tests
{
    /*private readonly DbContextOptions<TodoContext> dbContextOptions = new DbContextOptionsBuilder<TodoContext>()
            .UseInMemoryDatabase(databaseName: "TodoDbTest").Options;

    // Prueba para agregar un ítem
    [Fact]
    public async Task AddAsync_AddsItemToDatabase()
    {
        using (var context = new TodoContext(dbContextOptions))
        {
            var repository = new TodoItemRepository(context);
            await repository.AddAsync(new TodoItem { Name = "Test", IsComplete = false });

            Assert.Equal(1, context.TodoItems.Count());
            Assert.Equal("Test", context.TodoItems.First().Name);
        }
    }

    // Prueba para obtener un ítem por ID
    [Fact]
    public async Task GetByIdAsync_GetsItemById()
    {
        using (var context = new TodoContext(dbContextOptions))
        {
            context.TodoItems.Add(new TodoItem { Name = "Test", IsComplete = false });
            context.SaveChanges();

            var repository = new TodoItemRepository(context);
            var item = await repository.GetByIdAsync(1);

            Assert.NotNull(item);
            Assert.Equal("Test", item.Name);
        }
    }

    // Prueba para obtener todos los ítems
    [Fact]
    public async Task GetAllAsync_GetsAllItems()
    {
        using (var context = new TodoContext(dbContextOptions))
        {
            context.TodoItems.Add(new TodoItem { Name = "Test", IsComplete = false });
            context.TodoItems.Add(new TodoItem { Name = "Test 2", IsComplete = true });
            context.SaveChanges();

            var repository = new TodoItemRepository(context);
            var items = await repository.GetAllAsync();

            Assert.Equal(2, items.Count());
        }
    }

    // Prueba para actualizar un ítem
    [Fact]
    public async Task UpdateAsync_UpdatesItem()
    {
        using (var context = new TodoContext(dbContextOptions))
        {
            var originalItem = new TodoItem { Name = "Test", IsComplete = false };
            context.TodoItems.Add(originalItem);
            context.SaveChanges();

            var repository = new TodoItemRepository(context);
            originalItem.Name = "Updated Test";
            originalItem.IsComplete = true;
            await repository.UpdateAsync(originalItem);

            var updatedItem = context.TodoItems.First();

            Assert.Equal("Updated Test", updatedItem.Name);
            Assert.True(updatedItem.IsComplete);
        }
    }

    // Prueba para eliminar un ítem
    [Fact]
    public async Task DeleteAsync_DeletesItem()
    {
        using (var context = new TodoContext(dbContextOptions))
        {
            var item = new TodoItem { Name = "Test", IsComplete = false };
            context.TodoItems.Add(item);
            context.SaveChanges();

            var repository = new TodoItemRepository(context);
            await repository.DeleteAsync(item.Id);

            Assert.Empty(context.TodoItems);
        }
    }*/
}
