using System.ComponentModel.DataAnnotations;

namespace ToDoWonderBitsBackend.Domain.Models
{
    /// <summary>
    /// Representa un usuario en el sistema.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Identificador único del usuario.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Nombre de usuario del usuario.
        /// </summary>
        [Required(ErrorMessage = "Username is required.")]
        public string Username { get; set; }

        /// <summary>
        /// Contraseña del usuario.
        /// </summary>
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }

        /// <summary>
        /// Correo electrónico del usuario.
        /// </summary>
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        /// <summary>
        /// Nombre del usuario.
        /// </summary>
        public string? FullName { get; set; }

    }
}
