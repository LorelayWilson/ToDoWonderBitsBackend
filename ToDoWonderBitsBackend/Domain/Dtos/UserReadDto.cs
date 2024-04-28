using System.ComponentModel.DataAnnotations;

namespace ToDoWonderBitsBackend.Domain.Dtos
{
    /// <summary>
    /// DTO para lectura de la información del usuario.
    /// </summary>
    public class UserReadDto
    {
        public int Id { get; set; }

        public string Username { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string? FullName { get; set; }
    }
}
