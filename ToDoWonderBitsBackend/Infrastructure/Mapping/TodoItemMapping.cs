using AutoMapper;
using ToDoWonderBitsBackend.Domain.Dtos;
using ToDoWonderBitsBackend.Domain.Models;

namespace ToDoWonderBitsBackend.Infrastructure.Mappings
{
    public class TodoItemMappings : Profile
    {
        public TodoItemMappings()
        {
            CreateMap<TodoItem, TodoItemCreateDto>().ReverseMap();
            CreateMap<TodoItem, TodoItemUpdateDto>().ReverseMap();
            //por si se necesita personalizar o modificar el mapeo de alguna propiedad en específico
            CreateMap<TodoItem, TodoItemReadDto>()
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.DueDate, opt => opt.MapFrom(src => src.DueDate))
                .ForMember(dest => dest.ClosedDate, opt => opt.MapFrom(src => src.ClosedDate))
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId))
                .ForMember(dest => dest.StatusId, opt => opt.MapFrom(src => src.StatusId))
                .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.Priority));
        }
    }
}

