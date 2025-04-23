using Developer.Domain.Common.Wrappers.CustomResponse;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Developer.Application.UseCases.Posts.Commands;

public record UpdateCommentCommand : IRequest<ActionResult<Response<bool>>>
{
    public string PostId { get; init; }
    public string CommentId { get; init; }
    public string NewContent { get; init; }
}
