using ToDoWonderBitsBackend.Domain.Models;
using ToDoWonderBitsBackend.Domain.Ports;

namespace ToDoWonderBitsBackend.Domain.Services
{
    /// <summary>
    /// Servicio de dominio para manejar operaciones relacionadas con TodoItems.
    /// </summary>
    public class TodoService : ITodoService
    {
        private readonly ITodoItemRepository _todoItemRepository;

        /// <summary>
        /// Constructor que inyecta la dependencia del repositorio.
        /// </summary>
        /// <param name="todoItemRepository">Repositorio para operaciones de TodoItem.</param>
        public TodoService(ITodoItemRepository todoItemRepository)
        {
            _todoItemRepository = todoItemRepository;
        }

        /// <summary>
        /// Obtiene todos los ítems de tareas pendientes.
        /// </summary>
        /// <returns>Una colección de todos los TodoItems.</returns>
        public async Task<IEnumerable<TodoItem>> GetAllItemsAsync()
        {
            return await _todoItemRepository.GetAllAsync();
        }

        /// <summary>
        /// Obtiene un ítem específico por ID.
        /// </summary>
        /// <param name="id">El ID del ítem a buscar.</param>
        /// <returns>El ítem encontrado.</returns>
        public async Task<TodoItem> GetItemByIdAsync(int id)
        {
            var item = await _todoItemRepository.GetByIdAsync(id);
            if (item == null)
            {
                throw new KeyNotFoundException("Item not found.");
            }
            return item;
        }

        /// <summary>
        /// Añade un nuevo ítem a la lista de tareas.
        /// </summary>
        /// <param name="item">Ítem a añadir.</param>
        public async Task AddItemAsync(TodoItem item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            await _todoItemRepository.AddAsync(item);
        }

        /// <summary>
        /// Actualiza un ítem existente.
        /// </summary>
        /// <param name="item">Ítem a actualizar con nuevos valores.</param>
        public async Task UpdateItemAsync(TodoItem item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            var existingItem = await _todoItemRepository.GetByIdAsync(item.Id);
            if (existingItem == null)
            {
                throw new KeyNotFoundException("Item not found.");
            }

            existingItem.Name = item.Name;
            existingItem.IsComplete = item.IsComplete;

            await _todoItemRepository.UpdateAsync(existingItem);
        }

        /// <summary>
        /// Elimina un ítem por su ID.
        /// </summary>
        /// <param name="id">ID del ítem a eliminar.</param>
        public async Task DeleteItemAsync(int id)
        {
            var item = await _todoItemRepository.GetByIdAsync(id);
            if (item == null)
            {
                throw new KeyNotFoundException("Item not found.");
            }

            await _todoItemRepository.DeleteAsync(id);
        }
    }
}
