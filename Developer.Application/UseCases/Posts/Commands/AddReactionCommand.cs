using Developer.Domain.Common.Wrappers.CustomResponse;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Developer.Application.UseCases.Posts.Commands;

public class AddReactionCommand : IRequest<Response<string>>

{
    public string PostId { get; set; }
    public string UserId { get; set; }
    public string ReactionType { get; set; }
}