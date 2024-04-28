using System.ComponentModel.DataAnnotations;

namespace ToDoWonderBitsBackend.Domain.Dtos
{
    public class TodoItemUpdateDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "A description is required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "A due date is required")]
        public DateTime DueDate { get; set; }

        public DateTime? ClosedDate { get; set; }

        public int CategoryId { get; set; }

        [Required(ErrorMessage = "A status ID is required")]
        public int StatusId { get; set; }
        public int? Priority { get; set; }
    }

}
