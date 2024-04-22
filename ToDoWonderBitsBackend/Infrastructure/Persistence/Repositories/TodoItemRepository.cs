using ToDoWonderBitsBackend.Domain.Models;
using ToDoWonderBitsBackend.Infrastructure.Persistence.Repositories.Interface;

namespace ToDoWonderBitsBackend.Infrastructure.Persistence.Repositories
{
    public class TodoItemRepository : ITodoItemRepository
    {
        private readonly DbContext _context;

        public TodoItemRepository(DbContext context)
        {
            _context = context;
        }

        public async Task<TodoItem> GetTodoItemAsync(int id)
        {
            return await _context.Set<TodoItem>().FindAsync(id);
        }

        public async Task<IEnumerable<TodoItem>> GetTodoItemsAsync()
        {
            return await _context.Set<TodoItem>().ToListAsync();
        }

        public async Task AddTodoItemAsync(TodoItem item)
        {
            await _context.Set<TodoItem>().AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTodoItemAsync(TodoItem item)
        {
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTodoItemAsync(int id)
        {
            var item = await GetTodoItemAsync(id);
            _context.Set<TodoItem>().Remove(item);
            await _context.SaveChangesAsync();
        }

        Task<TodoItem> ITodoItemRepository.GetTodoItemAsync(int id)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<TodoItem>> ITodoItemRepository.GetTodoItemsAsync()
        {
            throw new NotImplementedException();
        }

        public Task AddTodoItemAsync(TodoItem item)
        {
            throw new NotImplementedException();
        }

        public Task UpdateTodoItemAsync(TodoItem item)
        {
            throw new NotImplementedException();
        }
    }
}
}
