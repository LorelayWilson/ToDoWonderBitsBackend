using ToDoWonderBitsBackend.Domain.Models;
using ToDoWonderBitsBackend.Domain.Ports;


namespace ToDoWonderBitsBackend.Infrastructure.Persistence.Repositories
{
    public class TodoItemRepository : ITodoItemRepository
    {
        Task ITodoItemRepository.AddAsync(TodoItem item)
        {
            throw new NotImplementedException();
        }

        Task ITodoItemRepository.DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<TodoItem>> ITodoItemRepository.GetAllAsync()
        {
            throw new NotImplementedException();
        }

        Task<TodoItem> ITodoItemRepository.GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        Task ITodoItemRepository.UpdateAsync(TodoItem item)
        {
            throw new NotImplementedException();
        }
    }

}
