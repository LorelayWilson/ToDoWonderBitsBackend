using AutoMapper;
using ToDoWonderBitsBackend.Domain.Dtos;
using ToDoWonderBitsBackend.Domain.Models;

namespace ToDoWonderBitsBackend.Infrastructure.Mappings
{
    public class UserMappings : Profile
    {
        public UserMappings()
        {
            CreateMap<User, UserCreateDto>().ReverseMap();
            CreateMap<User, UserUpdateDto>().ReverseMap();
            CreateMap<User, UserReadDto>();
        }
    }
}
