using Developer.Domain.Common.Wrappers.CustomResponse;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Developer.Application.UseCases.Posts.Commands;

public record AddReactionCommand : IRequest<ActionResult<Response<bool>>>
{
    public string PostId { get; init; }
    public string UserId { get; init; }
    public string ReactionType { get; init; }
}