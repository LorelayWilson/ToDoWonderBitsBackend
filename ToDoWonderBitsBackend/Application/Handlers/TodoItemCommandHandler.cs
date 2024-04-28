using AutoMapper;
using ToDoWonderBitsBackend.Application.Services.Interfaces;
using ToDoWonderBitsBackend.Domain.Dtos;
using ToDoWonderBitsBackend.Domain.Models;
using ToDoWonderBitsBackend.Domain.Ports;

namespace ToDoWonderBitsBackend.Application.Services
{
    public class TodoItemCommandHandler : ITodoItemCommandHandler
    {
        private readonly ITodoItemRepository _todoItemRepository;
        private readonly IMapper _mapper;

        public TodoItemCommandHandler(ITodoItemRepository todoItemRepository, IMapper mapper)
        {
            _todoItemRepository = todoItemRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Añade un nuevo ítem a la lista de tareas.
        /// </summary>
        /// <param name="itemDto">Ítem a añadir.</param>
        public async Task<TodoItem> CreateItemAsync(TodoItemCreateDto itemDto)
        {
            if (itemDto == null)
                throw new ArgumentNullException(nameof(itemDto));

            var newItem = _mapper.Map<TodoItem>(itemDto);

            await _todoItemRepository.CreateAsync(newItem);
            return newItem; 
        }

        /// <summary>
        /// Actualiza un ítem existente.
        /// </summary>
        /// <param name="item">Ítem a actualizar con nuevos valores.</param>
        public async Task UpdateItemAsync(TodoItemUpdateDto itemDto)
        {
            if (itemDto == null)
                throw new ArgumentNullException(nameof(itemDto));

            var existingItem = await _todoItemRepository.GetByIdAsync(itemDto.Id);
            if (existingItem == null)
            {
                throw new KeyNotFoundException("Item not found.");
            }

            _mapper.Map(itemDto, existingItem);

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
