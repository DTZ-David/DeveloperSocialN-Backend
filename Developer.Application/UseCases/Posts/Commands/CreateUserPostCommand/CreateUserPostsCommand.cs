using Developer.Application.UseCases.Posts.Dtos;
using Developer.Domain.Common.Wrappers.CustomResponse;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Developer.Application.UseCases.Posts.Commands.CreateUserPostCommand;

public record CreateUserPostsCommand(
    string AuthorId,
    string CodeSnippet,
    List<CreateComment> Comments,
    string Description,
    int likes,
    List<string>? Tags = null
) : IRequest<ActionResult<Response<UserPostsDto>>>;

public record CreateComment(
    string AuthorId, 
    string Comment
    );