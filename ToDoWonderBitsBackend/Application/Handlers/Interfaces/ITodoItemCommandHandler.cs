using ToDoWonderBitsBackend.Domain.Dtos;
using ToDoWonderBitsBackend.Domain.Models;

namespace ToDoWonderBitsBackend.Application.Services.Interfaces
{
    public interface ITodoItemCommandHandler
    {
        Task<TodoItem> CreateItemAsync(TodoItemCreateDto item);
        Task UpdateItemAsync(TodoItemUpdateDto item);
        Task DeleteItemAsync(int id);
    }
}
