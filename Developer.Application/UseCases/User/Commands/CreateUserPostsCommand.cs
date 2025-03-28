using Developer.Application.UseCases.User.Dtos;
using Developer.Domain.Common.Wrappers.CustomResponse;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Developer.Application.UseCases.User.Commands;

public record CreateUserPostsCommand(
    string AuthorId,
    string CodeSnippet,
    string Description,
    string Language,
    List<string>? Tags = null
) : IRequest<ActionResult<Response<UserPostsDto>>>;

