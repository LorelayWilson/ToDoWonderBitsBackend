using ToDoWonderBitsBackend.Domain.Models;

namespace ToDoWonderBitsBackend.Domain.Ports
{
    /// <summary>
    /// Interfaz para el repositorio de usuarios que define métodos para operaciones de persistencia.
    /// </summary>
    public interface IUserRepository
    {
        Task<User> GetByIdAsync(int id);
        Task<User> GetByUsernameAsync(string username);
        Task CreateAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(int id);
    }
}
