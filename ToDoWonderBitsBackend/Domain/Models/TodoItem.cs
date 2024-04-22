using System.ComponentModel.DataAnnotations;

namespace ToDoWonderBitsBackend.Domain.Models
{
    /// <summary>
    /// Representa un ítem de la lista de tareas pendientes.
    /// </summary>
    public class TodoItem
    {
        /// <summary>
        /// Identificador único del ítem.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Nombre o descripción del ítem de la tarea.
        /// </summary>
        [Required]
        public string Name { get; set; } 

        /// <summary>
        /// Indica si el ítem de la tarea ha sido completado.
        /// </summary>
        public bool IsComplete { get; set; }

        /// <summary>
        /// Inicializa una nueva instancia de TodoItem asegurándose de que el nombre no sea nulo ni vacío.
        /// </summary>
        /// <param name="name">Nombre requerido para el ítem de la tarea.</param>
        /// <exception cref="ArgumentException">Se lanza si 'name' es nulo o vacío.</exception>
        public TodoItem(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Name cannot be null or empty.", nameof(name));
            }

            Name = name;
        }
    }
}
