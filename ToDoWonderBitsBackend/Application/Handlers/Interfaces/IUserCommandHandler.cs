using ToDoWonderBitsBackend.Domain.Dtos;

namespace ToDoWonderBitsBackend.Application.Handlers.Interfaces
{
    /// <summary>
    /// Interfaz para el manejador de comandos relacionados con usuarios.
    /// </summary>
    public interface IUserCommandHandler
    {
        Task CreateUserAsync(UserCreateDto command);
        Task UpdateUserAsync(UserUpdateDto command);
        Task DeleteUserAsync(int id);
    }
}
