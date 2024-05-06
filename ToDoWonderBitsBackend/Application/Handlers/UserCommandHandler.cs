using AutoMapper;
using System;
using System.Threading.Tasks;
using ToDoWonderBitsBackend.Application.Handlers.Interfaces;
using ToDoWonderBitsBackend.Domain.Dtos;
using ToDoWonderBitsBackend.Domain.Models;
using ToDoWonderBitsBackend.Domain.Ports;

namespace ToDoWonderBitsBackend.Application.Handlers
{
    public class UserCommandHandler : IUserCommandHandler
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserCommandHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task CreateUserAsync(UserCreateDto userDto)
        {
            if (userDto == null)
            {
                throw new ArgumentNullException(nameof(userDto));
            }
            var user = _mapper.Map<User>(userDto);
            // Cifrar la contraseña antes de guardar
            user.Password = BCrypt.Net.BCrypt.HashPassword(userDto.Password);


            await _userRepository.CreateAsync(user);
        }

        public async Task UpdateUserAsync(UserUpdateDto userDto)
        {
            if (userDto == null)
            {
                throw new ArgumentNullException(nameof(userDto));
            }

            var user = await _userRepository.GetByIdAsync(userDto.Id);
            if (user == null)
            {
                throw new KeyNotFoundException("User not found.");
            }

            _mapper.Map(userDto, user);
            // Cifrar la nueva contraseña antes de actualizar
            if (!string.IsNullOrEmpty(userDto.Password))
            {
                user.Password = BCrypt.Net.BCrypt.HashPassword(userDto.Password);
            }

            await _userRepository.UpdateAsync(user);
        }

        public async Task DeleteUserAsync(int id)
        {
            if (id<=0)
            {
                throw new ArgumentException("User ID cannot be null or empty", nameof(id));
            }

            await _userRepository.DeleteAsync(id);
        }
    }
}
