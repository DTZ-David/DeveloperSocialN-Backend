using Developer.Application.UseCases.Posts.Dtos;
using Developer.Domain.Common.Wrappers.CustomResponse;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Developer.Application.UseCases.Posts.Commands;

public record CreateUserPostsCommand(
    string AuthorId,
    string CodeSnippet,
    string Description,
    string Language,
    List<string>? Tags = null
) : IRequest<ActionResult<Response<UserPostsDto>>>;

