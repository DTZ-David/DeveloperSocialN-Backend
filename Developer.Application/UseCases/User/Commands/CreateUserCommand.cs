using Developer.Application.UseCases.User.Dtos;
using Developer.Domain.Common.Wrappers.CustomResponse;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Developer.Application.UseCases.User.Commands;

public record CreateUserCommand(
    string Email,
    string UserName,
    string Password,
    string Bio = "",
    string ProfilePicture = ""
) : IRequest<ActionResult<Response<UserDto>>>;
