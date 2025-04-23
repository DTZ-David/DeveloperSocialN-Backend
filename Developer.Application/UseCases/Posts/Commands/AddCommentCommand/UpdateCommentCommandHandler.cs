using AutoMapper;
using Developer.Domain.Common.Enums;
using Developer.Domain.Common.Exceptions;
using Developer.Domain.Common.Wrappers.CustomResponse;
using Developer.Domain.Ports;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Developer.Application.UseCases.Posts.Commands
{
    public class UpdateCommentCommandHandler : IRequestHandler<UpdateCommentCommand, ActionResult<Response<bool>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCommentCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ActionResult<Response<bool>>> Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.PostService.UpdateCommentAsync(request.PostId, request.CommentId, request.NewContent);

            if (!result)
            {
                throw new BusinessException("Comentario no encontrado o error al actualizar", (int)MessageStatusCode.NotFound);
            }

            return new OkObjectResult(new Response<bool>((int)MessageStatusCode.Succes, true));
        }
    }
}
