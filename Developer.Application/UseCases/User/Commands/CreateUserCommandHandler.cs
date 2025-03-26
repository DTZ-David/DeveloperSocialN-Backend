using AutoMapper;
using Developer.Application.UseCases.User.Dtos;
using Developer.Domain.Common.Wrappers.CustomResponse;
using Developer.Domain.Ports.Configuration.Localization;
using Developer.Domain.Ports;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Developer.Domain.Common.Enums;


namespace Developer.Application.UseCases.User.Commands;

public record CreateUserCommandHandler : IRequestHandler<CreateUserCommand, ActionResult<Response<UserDto>>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILocalizationService _localizationService;

    public CreateUserCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, ILocalizationService localizationService)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _localizationService = localizationService;
    }

    public async Task<ActionResult<Response<UserDto>>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {

        var user = new Domain.Entities.User(
            email: request.Email,
            userName: request.UserName,
            password: request.Password,
            bio: request.Bio,
            profilePicture: request.ProfilePicture
        );


        await _unitOfWork.UserService.CreateUserAsync(user);

        var userDto = _mapper.Map<UserDto>(user);

        var response = new Response<UserDto>((int)MessageStatusCode.Create, userDto);

        return new CreatedResult(string.Empty, response);
    }
}