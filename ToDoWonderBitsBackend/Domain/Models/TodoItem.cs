using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace ToDoWonderBitsBackend.Domain.Models
{
    /// <summary>
    /// Representa un ítem de la lista de tareas pendientes.
    /// </summary>
    public class TodoItem
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Introduce a description")]
        public string Description { get; set; } = string.Empty;
        [Required(ErrorMessage = "Introduce a due date")]
        public DateTime? DueDate { get; set; }
        [Required(ErrorMessage = "Introduce a closed date")]
        public DateTime? ClosedDate { get; set; }
        public int? CategoryId { get; set; }
        [ValidateNever]
        public Category Category { get; set; } = null!;
        [Required(ErrorMessage = "Select a status")]
        public int StatusId { get; set; }
        [ValidateNever]
        public Status Status { get; set; } = null!;
        public bool Overdue => StatusId == 1 && DueDate < DateTime.Today;
        public int Priority { get; set; }

        /// <summary>
        /// Actualiza el estado del item.
        /// </summary>
        /// <param name="newStatus">Nuevo estado para el ítem.</param>
        public void UpdateStatus(int newStatus)
        {
            if (StatusId != newStatus && (newStatus == 1|| newStatus == 2))
            {
                StatusId = newStatus;

                if (newStatus == 2)
                {
                    ClosedDate = DateTime.Now;  
                }
            }
        }

        /// <summary>
        /// Actualiza la categoría del ítem.
        /// </summary>
        /// <param name="newCategoryId">Nuevo ID de categoría para el ítem.</param>
        public void UpdateCategory(int newCategoryId)
        {
             if (newCategoryId != this.CategoryId)
            {
                this.CategoryId = newCategoryId;
            }
        }

    }
}
