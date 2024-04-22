using ToDoWonderBitsBackend.Domain.Models;

namespace ToDoWonderBitsBackend.Domain.Ports
{
    /// <summary>
    /// Interfaz para el servicio de manejo de TodoItems, definiendo operaciones de alto nivel.
    /// </summary>
    public interface ITodoService
    {
        Task<IEnumerable<TodoItem>> GetAllItemsAsync();
        Task<TodoItem> GetItemByIdAsync(int id);
        Task AddItemAsync(TodoItem item);
        Task UpdateItemAsync(TodoItem item);
        Task DeleteItemAsync(int id);
    }
}
