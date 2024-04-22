using ToDoWonderBitsBackend.Domain.Models;

namespace ToDoWonderBitsBackend.Application.Services.Interfaces
{
    /// <summary>
    /// Interfaz para el servicio de manejo de TodoItems, definiendo operaciones de alto nivel.
    /// </summary>
    public interface ITodoItemService
    {
        Task<IEnumerable<TodoItem>> GetAllItemsAsync();
        Task<TodoItem> GetItemByIdAsync(int id);
        Task AddItemAsync(TodoItem item);
        Task UpdateItemAsync(TodoItem item);
        Task DeleteItemAsync(int id);
    }
}
