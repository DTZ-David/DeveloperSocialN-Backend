using Developer.Domain.Common.Wrappers.CustomResponse;
using Developer.Domain.Entities.User;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Developer.Application.UseCases.Posts.Commands;

public record AddCommentCommand(
    string PostId,
    string CommentText,
    InteractionType InteractionType
) : IRequest<ActionResult<Response<bool>>>;
