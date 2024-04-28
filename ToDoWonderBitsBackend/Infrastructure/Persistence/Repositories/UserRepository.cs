using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ToDoWonderBitsBackend.Domain.Models;
using ToDoWonderBitsBackend.Domain.Ports;

namespace ToDoWonderBitsBackend.Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly TodoContext _context;

        public UserRepository(TodoContext context)
        {
            _context = context;
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User> GetByUsernameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task CreateAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user)
        {
            var existingUser = await _context.Users.FindAsync(user.Id);
            if (existingUser == null)
            {
                throw new KeyNotFoundException("User not found.");
            }

            // Aquí deberías asegurarte de que todas las propiedades que necesitas actualizar estén siendo actualizadas
            // Esto es especialmente importante si no estás usando el tracking automático de Entity Framework
            _context.Entry(existingUser).CurrentValues.SetValues(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var existingUser = await _context.Users.FindAsync(id);
            if (existingUser == null)
            {
                throw new KeyNotFoundException("User not found.");
            }

            _context.Users.Remove(existingUser);
            await _context.SaveChangesAsync();
        }
    }
}
