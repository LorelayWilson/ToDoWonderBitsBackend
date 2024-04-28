using ToDoWonderBitsBackend.Domain.Models;

namespace ToDoWonderBitsBackend.Domain.Ports
{

    /// <summary>
    /// Interfaz para el repositorio de TodoItem que define métodos para operaciones de persistencia.
    /// </summary>
    public interface ITodoItemRepository
    {
        Task<TodoItem> GetByIdAsync(int id);
        Task<IEnumerable<TodoItem>> GetAllAsync();
        Task CreateAsync(TodoItem item);
        Task UpdateAsync(TodoItem item);
        Task DeleteAsync(int id);
    }
}
