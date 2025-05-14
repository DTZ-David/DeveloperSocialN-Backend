using Developer.Application.UseCases.Posts.Dtos;
using Developer.Domain.Common.Wrappers.CustomResponse;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Developer.Application.UseCases.Posts.Commands.CreateUserPostCommand;

public record CreateUserPostsCommand(
    string CodeSnippet,
    string CodeLanguage,
    string Description,
    List<string>? Tags = null
) : IRequest<ActionResult<Response<UserPostsDto>>>;

public record CreateComment(
    string Comment
    );