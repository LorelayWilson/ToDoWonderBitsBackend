using System.ComponentModel.DataAnnotations;

namespace ToDoWonderBitsBackend.Domain.Dtos
{
    /// <summary>
    /// Objeto de Transferencia de Datos (DTO) para actualizar un usuario.
    /// </summary>
    public class UserUpdateDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        public string? FullName { get; set; }
    }
}
