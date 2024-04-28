using System.ComponentModel.DataAnnotations;

namespace ToDoWonderBitsBackend.Domain.Dtos
{
    /// <summary>
    /// Objeto de Transferencia de Datos (DTO) para crear un usuario.
    /// </summary>
    public class UserCreateDto
    {
        [Required(ErrorMessage = "Username es obligatorio.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password es obligatoria.")]
        public string Password { get; set; }

        [EmailAddress(ErrorMessage = "Dirección de correo electrónico inválida.")]
        public string Email { get; set; }

        public string? FullName { get; set; }
    }
}
