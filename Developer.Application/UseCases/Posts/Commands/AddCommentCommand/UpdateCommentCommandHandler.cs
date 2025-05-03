using AutoMapper;
using Developer.Application.UseCases.ActivityLog.Command;
using Developer.Application.UseCases.ActivityLog.Dtos;
using Developer.Domain.Common.Enums;
using Developer.Domain.Common.Exceptions;
using Developer.Domain.Common.Wrappers.CustomResponse;
using Developer.Domain.Entities.User;
using Developer.Domain.Ports;
using Developer.Domain.Ports.Configuration.Localization;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Developer.Application.UseCases.Posts.Commands
{
    public class UpdateCommentCommandHandler : IRequestHandler<UpdateCommentCommand, ActionResult<Response<bool>>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;
        private readonly ILocalizationService _localizationService;


        public UpdateCommentCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IMediator mediator)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _mediator = mediator;
        }

        public async Task<ActionResult<Response<bool>>> Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
        {
            var claims = await _unitOfWork.ClaimsService.GetUserClaim();
            if (claims is null)
            {
                throw new BusinessException("Error de autenticidad", (int)MessageStatusCode.BadRequest);
            }
            var result = await _unitOfWork.PostService.UpdateCommentAsync(request.PostId, request.CommentId, request.NewContent);

            if (!result)
            {
                throw new BusinessException("Comentario no encontrado o error al actualizar", (int)MessageStatusCode.NotFound);
            }

            await _mediator.Send(new RegisterInteractionCommand(
               TargetPostId: request.PostId,
               Type: InteractionType.Comentario,
               Content: request.NewContent
           ));

            return new OkObjectResult(new Response<bool>((int)MessageStatusCode.Succes, true));
        }
    }
}
