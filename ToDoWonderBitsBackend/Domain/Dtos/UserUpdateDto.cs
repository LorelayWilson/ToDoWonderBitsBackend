using System.ComponentModel.DataAnnotations;

namespace ToDoWonderBitsBackend.Domain.Dtos
{
    /// <summary>
    /// Objeto de Transferencia de Datos (DTO) para actualizar un usuario.
    /// </summary>
    public class UserUpdateDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Username es obligatorio.")]
        public string Username { get; set; }

        [EmailAddress(ErrorMessage = "Dirección de correo electrónico inválida.")]
        public string Email { get; set; }

        public string? FullName { get; set; }
    }
}
