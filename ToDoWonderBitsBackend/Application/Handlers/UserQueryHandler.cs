using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ToDoWonderBitsBackend.Application.Handlers.Interfaces;
using ToDoWonderBitsBackend.Domain.Models;
using ToDoWonderBitsBackend.Domain.Ports;

namespace ToDoWonderBitsBackend.Application.Handlers
{
    public class UserQueryHandler : IUserQueryHandler
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;


        public UserQueryHandler(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;

        }

        public async Task<string> Login(string username, string password)
        {
            if(username == null || password == null) {
                throw new ArgumentException("User and password cannot be null");
            }

            User user = await GetUserByUsernameAsync(username);
            if(user == null || user.Password!=password) { throw new ArgumentException("Username or password does not exist"); }

            var token = GenerateJwtToken(user.Id);
            return token;
        }

        public string GenerateJwtToken(int id)
        {
            var userId = id.ToString();
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Audience = _configuration["Jwt:Audience"],
                Issuer = _configuration["Jwt:Issuer"],
                Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, userId) }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("User ID cannot be less than or equal to zero", nameof(id));
            }

            return await _userRepository.GetByIdAsync(id);
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                throw new ArgumentException("Username cannot be null or empty", nameof(username));
            }

            return await _userRepository.GetByUsernameAsync(username);
        }
    }
}
