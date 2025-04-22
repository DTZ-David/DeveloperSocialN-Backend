using Developer.Domain.Common.Enums;
using Developer.Domain.Common.Exceptions;
using Developer.Domain.Common.Wrappers.CustomResponse;
using Developer.Domain.Ports;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Developer.Application.UseCases.Posts.Commands
{
    public class AddReactionCommandHandler : IRequestHandler<AddReactionCommand, ActionResult<Response<bool>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddReactionCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ActionResult<Response<bool>>> Handle(AddReactionCommand request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.PostService.AddReactionAsync(request.PostId, request.UserId, request.ReactionType);

            if (!result)
            {
                throw new BusinessException("Error al agregar reacción", (int)MessageStatusCode.BadRequest);
            }

            return new OkObjectResult(new Response<bool>((int)MessageStatusCode.Succes, true));
        }
    }
}
