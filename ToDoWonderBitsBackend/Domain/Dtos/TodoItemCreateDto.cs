using System.ComponentModel.DataAnnotations;

namespace ToDoWonderBitsBackend.Domain.Dtos
{
    public class TodoItemCreateDto
    {
        [Required(ErrorMessage = "A description is required")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "A due date is required")]
        public DateTime DueDate { get; set; }
        public DateTime? ClosedDate { get; set; }
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "A status ID is required")]
        public int StatusId { get; set; } = 1;
        public int? Priority { get; set; }
    }

}
