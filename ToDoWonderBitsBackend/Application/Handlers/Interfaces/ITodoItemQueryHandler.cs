﻿using ToDoWonderBitsBackend.Domain.Models;

namespace ToDoWonderBitsBackend.Application.Services.Interfaces
{
    /// <summary>
    /// Interfaz para el servicio de manejo de TodoItems
    /// </summary>
    public interface ITodoItemQueryHandler
    {
        Task<IEnumerable<TodoItem>> GetAllItemsAsync();
        Task<IEnumerable<Status>> GetAllStatusAsync();
        Task<IEnumerable<Category>> GetAllCategoryAsync();
        Task<TodoItem> GetItemByIdAsync(int id);
    }
}
