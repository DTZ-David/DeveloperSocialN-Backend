using AutoMapper;
using Developer.Application.UseCases.User.Dtos;
using Developer.Domain.Common.Wrappers.CustomResponse;
using Developer.Domain.Ports.Configuration.Localization;
using Developer.Domain.Ports;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Developer.Domain.Common.Enums;



namespace Developer.Application.UseCases.User.Commands.UserTest.command;

public record RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, ActionResult<Response<string>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILocalizationService _localizationService;
    private readonly IMapper _mapper;

    public RegisterUserCommandHandler(IUnitOfWork unitOfWork, ILocalizationService localizationService, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _localizationService = localizationService;
        _mapper = mapper;
    }

    public async Task<ActionResult<Response<string>>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        
        Domain.Entities.RegisterUser registerUser = new(
            request.email,
            request.userName,
            request.password
        );

        await _unitOfWork.UserRegisterServices.CreateUserAsync(registerUser);
        var response = new Response<string>((int)MessageStatusCode.Create, registerUser.Id);
        return new CreatedResult(string.Empty, response);
    }
}