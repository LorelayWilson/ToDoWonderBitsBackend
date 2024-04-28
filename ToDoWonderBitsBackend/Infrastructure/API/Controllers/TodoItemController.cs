using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ToDoWonderBitsBackend.Application.Services.Interfaces;
using ToDoWonderBitsBackend.Domain.Dtos;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace ToDoWonderBitsBackend.Infrastructure.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class TodoItemController : ControllerBase
    {
        private readonly ITodoItemCommandHandler _commandService;
        private readonly ITodoItemQueryHandler _queryService;
        private readonly IMapper _mapper;
        private readonly ILogger<TodoItemController> _logger;

        public TodoItemController(
            ITodoItemCommandHandler commandService,
            ITodoItemQueryHandler queryService,
            IMapper mapper,
            ILogger<TodoItemController> logger)
        {
            _commandService = commandService;
            _queryService = queryService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<TodoItemReadDto>>> GetTodoItems()
        {
            try
            {
                var items = await _queryService.GetAllItemsAsync();
                var itemsDto = _mapper.Map<IEnumerable<TodoItemReadDto>>(items);
                return Ok(itemsDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving todo items");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpGet("Get/{id}")]
        public async Task<ActionResult<TodoItemReadDto>> GetTodoItem(int id)
        {
            try
            {
                var item = await _queryService.GetItemByIdAsync(id);
                if (item == null)
                {
                    return NotFound($"Item with Id = {id} not found.");
                }
                var itemDto = _mapper.Map<TodoItemReadDto>(item);
                return Ok(itemDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving todo item with ID: {Id}", id);
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateTodoItem([FromBody] TodoItemCreateDto todoItemDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var createdItem = await _commandService.CreateItemAsync(todoItemDto);
                var createdItemDto = _mapper.Map<TodoItemReadDto>(createdItem);
                return CreatedAtAction(nameof(GetTodoItem), new { id = createdItem.Id }, createdItemDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating todo item");
                return StatusCode(500, "An error occurred while creating the item.");
            }
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdateTodoItem(int id, [FromBody] TodoItemUpdateDto todoItemDto)
        {
            if (id != todoItemDto.Id || !ModelState.IsValid)
            {
                return BadRequest("Invalid data provided.");
            }

            try
            {
                await _commandService.UpdateItemAsync(todoItemDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating todo item with ID: {Id}", id);
                return StatusCode(500, "An error occurred while updating the item.");
            }
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteTodoItem(int id)
        {
            try
            {
                var item = await _queryService.GetItemByIdAsync(id);
                if (item == null)
                {
                    return NotFound($"Item with Id = {id} not found.");
                }

                await _commandService.DeleteItemAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting todo item with ID: {Id}", id);
                return StatusCode(500, "An error occurred while deleting the item.");
            }
        }
    }
}
