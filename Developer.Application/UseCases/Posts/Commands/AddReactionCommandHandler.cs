using Developer.Domain.Common.Enums;
using Developer.Domain.Common.Exceptions;
using Developer.Domain.Common.Wrappers.CustomResponse;
using Developer.Domain.Ports;
using Developer.Domain.Ports.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Developer.Application.UseCases.Posts.Commands;

    public class AddReactionCommandHandler : IRequestHandler<AddReactionCommand, Response<string>>
{
    private readonly IUserPostsService _service;

    public AddReactionCommandHandler(IUserPostsService service)
    {
        _service = service;
    }

    public async Task<Response<string>> Handle(AddReactionCommand request, CancellationToken cancellationToken)
    {
        await _service.UpdateReactionAsync(request.PostId, request.UserId, request.ReactionType);
        
        return new Response<string>((int)MessageStatusCode.Succes, "Reacción agregada correctamente");
        
    }
}


