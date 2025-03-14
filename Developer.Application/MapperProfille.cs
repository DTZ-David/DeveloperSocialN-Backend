using AutoMapper;
using Developer.Application.UseCases.User.Dtos;
using Developer.Domain.Entities;
namespace Developer.Application;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<User, UserDto>().ReverseMap();
    }
}
