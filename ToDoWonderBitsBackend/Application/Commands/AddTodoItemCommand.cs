using MediatR;
using ToDoWonderBitsBackend.Domain.Models;
using ToDoWonderBitsBackend.Domain.Ports;

namespace ToDoWonderBitsBackend.Application.Commands
{
    public class AddTodoItemCommand : IRequest<int>
    {
        public TodoItem TodoItem { get; set; }

        public class AddTodoItemCommandHandler : IRequestHandler<AddTodoItemCommand, int>
        {
            private readonly ITodoItemRepository _repository;

            public AddTodoItemCommandHandler(ITodoItemRepository repository)
            {
                _repository = repository;
            }

            public async Task<int> Handle(AddTodoItemCommand request, CancellationToken cancellationToken)
            {
                var item = new TodoItem
                {
                    Name = request.TodoItem.Name,
                    IsComplete = request.TodoItem.IsComplete
                };
                await _repository.AddTodoItemAsync(item);
                return item.Id; // Return the ID of the newly added item.
            }
        }
    }
}
