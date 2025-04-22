using AutoMapper;
using Developer.Application.UseCases.ActivityLog.Dtos;
using Developer.Application.UseCases.Posts.Dtos;
using Developer.Application.UseCases.User.Dtos;
using Developer.Domain.Entities;
using Developer.Domain.Entities.Posts;
using Developer.Domain.Entities.User;
namespace Developer.Application;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<User, UserDto>().ReverseMap();
        CreateMap<UserPosts, UserPostsDto>().ReverseMap();
        CreateMap<InteractionLog, InteractionLogDto>().ReverseMap();
    }
}
