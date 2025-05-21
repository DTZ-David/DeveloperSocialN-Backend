using AutoMapper;
using Developer.Application.UseCases.User.Dtos;
using Developer.Domain.Common.Wrappers.CustomResponse;
using Developer.Domain.Ports.Configuration.Localization;
using Developer.Domain.Ports;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Developer.Domain.Common.Enums;
using Developer.Application.UseCases.Users.Dtos;
using Developer.Domain.Entities.Posts;
using Newtonsoft.Json.Linq;
using Developer.Domain.Common.Exceptions;



namespace Developer.Application.UseCases.Users.Commands.CreateUsers;

public record CreateUsersCommandHandler : IRequestHandler<CreateUsersCommand, ActionResult<Response<UserDto>>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILocalizationService _localizationService;

    public CreateUsersCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, ILocalizationService localizationService)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _localizationService = localizationService;
    }

    public async Task<ActionResult<Response<UserDto>>> Handle(CreateUsersCommand request, CancellationToken cancellationToken)
    {

        var userToCreate = await _unitOfWork.UserService.GetUserByEmail(request.Email);
        if (userToCreate is not null)
        {
            throw new BusinessException($"Usuario ya se encuentra registrado.", (int)MessageStatusCode.Conflict);

        }

        var user = new Domain.Entities.User.User(
            email: request.Email,
            username: request.UserName,
            password: request.Password,
            bio: request.Bio,
            profilePicture: request.ProfilePicture,
            preferences : request.Preferences.ProgrammingLanguages
        );
        
        await _unitOfWork.UserService.CreateUserAsync(user);

        var userDto = new UserDto(
                   Email: request.Email,
                    UserName : request.UserName,
                    ProfilePicture: request.ProfilePicture,
                    Bio: request.Bio,
                    PostsCount: 0,
                    FollowersCount: 0,
                    CurrentFollow: false
            );

        var response = new Response<UserDto>((int)MessageStatusCode.Create, userDto);

        return new CreatedResult(string.Empty, response);
    }
}