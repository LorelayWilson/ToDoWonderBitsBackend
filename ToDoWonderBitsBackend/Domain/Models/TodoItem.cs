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
        public int Id { get; set; }

        /// <summary>
        /// Nombre o descripción del ítem de la tarea.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Indica si el ítem de la tarea ha sido completado.
        /// </summary>
        public bool IsComplete { get; set; }
    }
}
