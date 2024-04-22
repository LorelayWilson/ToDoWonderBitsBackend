using MediatR;
using Microsoft.AspNetCore.Mvc;
using ToDoWonderBitsBackend.Domain.Models;

namespace ToDoWonderBitsBackend.Infrastructure.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoItemsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TodoItemsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/TodoItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems()
        {
            var query = new GetAllTodoItemsQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        // GET: api/TodoItems/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>> GetTodoItem(int id)
        {
            var query = new GetTodoItemQuery { Id = id };
            var result = await _mediator.Send(query);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        // POST: api/TodoItems
        [HttpPost]
        public async Task<ActionResult<TodoItem>> PostTodoItem(TodoItem todoItemDto)
        {
            var command = new AddTodoItemCommand { TodoItem = todoItemDto };
            var itemId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetTodoItem), new { id = itemId }, todoItemDto);
        }

        // PUT: api/TodoItems/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(int id, TodoItem todoItemDto)
        {
            if (id != todoItemDto.Id)
            {
                return BadRequest();
            }

            var command = new UpdateTodoItemCommand { TodoItem = todoItemDto };
            await _mediator.Send(command);
            return NoContent();
        }

        // DELETE: api/TodoItems/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(int id)
        {
            var command = new DeleteTodoItemCommand { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
