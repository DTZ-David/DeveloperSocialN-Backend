using AutoMapper;
using Developer.Application.UseCases.ActivityLog.Command;
using Developer.Domain.Common.Enums;
using Developer.Domain.Common.Exceptions;
using Developer.Domain.Common.Wrappers.CustomResponse;
using Developer.Domain.Entities.User;
using Developer.Domain.Ports;
using Developer.Domain.Ports.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Developer.Application.UseCases.Posts.Commands.ReacctionCommands;

    public class AddReactionCommandHandler : IRequestHandler<AddReactionCommand, Response<string>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMediator _mediator;

    public AddReactionCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IMediator mediator)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _mediator = mediator;
    }

    public async Task<Response<string>> Handle(AddReactionCommand request, CancellationToken cancellationToken)
    {
        var claims = await _unitOfWork.ClaimsService.GetUserClaim();
        if (claims is null)
        {
            throw new BusinessException("Error de autenticidad", (int)MessageStatusCode.BadRequest);
        }

        await _mediator.Send(new RegisterInteractionCommand(
               TargetPostId: request.PostId,
               Type: InteractionType.Comentario,
               Content: request.ReactionType
           ));

        await _unitOfWork.PostService.UpdateReactionAsync(request.PostId, request.UserId, request.ReactionType);
        
        return new Response<string>((int)MessageStatusCode.Succes, "Reacción agregada correctamente");
        
    }
}


