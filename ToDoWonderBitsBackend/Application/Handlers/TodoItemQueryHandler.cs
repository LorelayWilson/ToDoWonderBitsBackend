using ToDoWonderBitsBackend.Application.Services.Interfaces;
using ToDoWonderBitsBackend.Domain.Models;
using ToDoWonderBitsBackend.Domain.Ports;

namespace ToDoWonderBitsBackend.Application.Services
{
    /// <summary>
    /// Servicio de dominio para manejar operaciones relacionadas con TodoItems.
    /// </summary>
    public class TodoItemQueryHandler : ITodoItemQueryHandler
    {
        private readonly ITodoItemRepository _todoItemRepository;

        /// <summary>
        /// Constructor que inyecta la dependencia del repositorio.
        /// </summary>
        /// <param name="todoItemRepository">Repositorio para operaciones de TodoItem.</param>
        public TodoItemQueryHandler(ITodoItemRepository todoItemRepository)
        {
            _todoItemRepository = todoItemRepository;
        }

        /// <summary>
        /// Obtiene todos los ítems de esttados.
        /// </summary>
        /// <returns>Una colección de todos los Status.</returns>
        public async Task<IEnumerable<Status>> GetAllStatusAsync()
        {
            return await _todoItemRepository.GetAllStatusAsync();
        }

        /// <summary>
        /// Obtiene todos los ítems de categorias.
        /// </summary>
        /// <returns>Una colección de todos los Category.</returns>
        public async Task<IEnumerable<Category>> GetAllCategoryAsync()
        {
            return await _todoItemRepository.GetAllCategoryAsync();
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

    }
}
