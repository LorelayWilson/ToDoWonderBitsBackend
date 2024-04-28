using System;

namespace ToDoWonderBitsBackend.Domain.Dtos
{
    public class TodoItemReadDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? ClosedDate { get; set; }
        public int? CategoryId { get; set; }
        public int StatusId { get; set; }
        public int? Priority { get; set; }
    }
}
