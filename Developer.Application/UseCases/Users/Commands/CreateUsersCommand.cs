using Developer.Application.UseCases.User.Dtos;
using Developer.Domain.Common.Wrappers.CustomResponse;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Developer.Application.UseCases.User.Commands;

public record CreateUsersCommand(
    string Email,
    string UserName,
    string Password,
    string Bio,
    string ProfilePicture,
    PreferencesCommand Preferences
) : IRequest<ActionResult<Response<UserDto>>>;

public record SocialInfoCommand(
    List<string> Followers,
    List<string> Following
 );

public record PreferencesCommand(
    List<string> ProgrammingLanguages
    );