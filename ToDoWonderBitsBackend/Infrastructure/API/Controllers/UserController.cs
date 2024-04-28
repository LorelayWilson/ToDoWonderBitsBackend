using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ToDoWonderBitsBackend.Application.Services.Interfaces;
using ToDoWonderBitsBackend.Domain.Dtos;
using AutoMapper;
using System;
using ToDoWonderBitsBackend.Application.Handlers.Interfaces;
using ToDoWonderBitsBackend.Domain.Models;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;

namespace ToDoWonderBitsBackend.Infrastructure.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    
    public class UserController : ControllerBase
    {
        private readonly IUserCommandHandler _commandHandler;
        private readonly IUserQueryHandler _queryHandler;
        private readonly IMapper _mapper;
        private readonly ILogger<UserController> _logger;

        public UserController(
            IUserCommandHandler commandHandler,
            IUserQueryHandler queryHandler,
            IMapper mapper,
            ILogger<UserController> logger)
        {
            _commandHandler = commandHandler;
            _queryHandler = queryHandler;
            _mapper = mapper;
            _logger = logger;
        } 


        [HttpGet("Login")]
        public async Task<ActionResult<string>> Login(string username, string password)
        {
            try
            {
               var token = await _queryHandler.Login(username, password);
               return Ok(token);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving user: {user}", username);
                return StatusCode(500, "An error occurred while processing your request.");
            }

        }

        [HttpGet("GetUser/{id}")]
        [Authorize]
        public async Task<ActionResult<UserReadDto>> GetUser(int id)
        {
            try
            {
                if (id <= 0)
                {
                    _logger.LogWarning("Invalid ID passed to GetUser: {Id}", id);
                    return BadRequest("Invalid ID.");
                }

                var user = await _queryHandler.GetUserByIdAsync(id);
                if (user == null)
                {
                    _logger.LogInformation("User not found: {Id}", id);
                    return NotFound($"User with Id = {id} not found.");
                }
                var userDto = _mapper.Map<UserReadDto>(user);
                return Ok(userDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving user with ID: {Id}", id);
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser([FromBody] UserCreateDto userDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _commandHandler.CreateUserAsync(userDto); 
                var user = await _queryHandler.GetUserByUsernameAsync(userDto.Username); 

                if (user == null)
                {
                    _logger.LogError("User was not created successfully.");
                    return StatusCode(500, "User creation failed.");
                }

                var readDto = _mapper.Map<UserReadDto>(user);
                return CreatedAtAction(nameof(GetUser), new { id = user.Id }, readDto); 
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating user");
                return StatusCode(500, "An error occurred while creating the user.");
            }
        }

        [HttpPut("UpdateUser/{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserUpdateDto userDto)
        {
            if (id != userDto.Id || !ModelState.IsValid)
            {
                return BadRequest("Invalid data provided.");
            }

            try
            {
                await _commandHandler.UpdateUserAsync(userDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating user with ID: {Id}", id);
                return StatusCode(500, "An error occurred while updating the user.");
            }
        }

        [HttpDelete("DeleteUser/{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                await _commandHandler.DeleteUserAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting user with ID: {Id}", id);
                return StatusCode(500, "An error occurred while deleting the user.");
            }
        }
    }
}
