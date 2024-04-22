using ToDoWonderBitsBackend.Domain.Models;

namespace ToDoWonderBitsBackend.Infrastructure.Persistence.Repositories.Interface
{
    public interface ITodoItemRepository
    {
        Task<TodoItem> GetTodoItemAsync(int id);
        Task<IEnumerable<TodoItem>> GetTodoItemsAsync();
        Task AddTodoItemAsync(TodoItem item);
        Task UpdateTodoItemAsync(TodoItem item);
        Task DeleteTodoItemAsync(int id);
    }
}
