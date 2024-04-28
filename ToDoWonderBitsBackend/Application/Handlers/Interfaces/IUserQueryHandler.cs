using ToDoWonderBitsBackend.Domain.Models;

namespace ToDoWonderBitsBackend.Application.Handlers.Interfaces
{
    /// <summary>
    /// Interfaz para el manejador de consultas relacionadas con usuarios.
    /// </summary>
    public interface IUserQueryHandler
    {
        Task<string> Login(string username, string password);
        Task<User> GetUserByIdAsync(int id);
        Task<User> GetUserByUsernameAsync(string username);
    }
}
