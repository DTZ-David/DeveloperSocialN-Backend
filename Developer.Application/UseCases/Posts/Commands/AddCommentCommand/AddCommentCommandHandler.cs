using AutoMapper;
using Developer.Application.UseCases.ActivityLog.Command;
using Developer.Application.UseCases.ActivityLog.Dtos;
using Developer.Domain.Common.Enums;
using Developer.Domain.Common.Exceptions;
using Developer.Domain.Common.Wrappers.CustomResponse;
using Developer.Domain.Entities.Posts;
using Developer.Domain.Entities.User;
using Developer.Domain.Ports;
using Developer.Domain.Ports.Configuration.Localization;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Developer.Application.UseCases.Posts.Commands
{
    public class AddCommentCommandHandler : IRequestHandler<AddCommentCommand, ActionResult<Response<bool>>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;


        public AddCommentCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IMediator mediator)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _mediator = mediator;
        }

        public async Task<ActionResult<Response<bool>>> Handle(AddCommentCommand request, CancellationToken cancellationToken)
        {
            var claims = await _unitOfWork.ClaimsService.GetUserClaim();
            if (claims is null)
            {
                throw new BusinessException("Error de autenticidad", (int)MessageStatusCode.BadRequest);
            }

            var post = await _unitOfWork.PostService.GetUserPostByPostId(request.PostId);
            if (post is null)
            {
                throw new BusinessException("Error, intente más tarde", (int)MessageStatusCode.BadRequest);
            }

            var comment = new Comments(
                claims.UserId,
                request.CommentText,
                request.InteractionType
            );
            
           

            post.Comments ??= new List<Comments>();
            post.Comments.Add(comment);

            var reactionKey = MapInteractionTypeToKey(request.InteractionType);
            post.Reactions ??= new Dictionary<string, int>();
            post.Reactions[reactionKey] = post.Reactions.GetValueOrDefault(reactionKey) + 1;

            await _unitOfWork.PostService.AddCommentAsync(comment);
            await _unitOfWork.PostService.UpdatePostAsync(post);

            return new OkObjectResult(new Response<bool>((int)MessageStatusCode.Succes, true));
        }


        private static string MapInteractionTypeToKey(InteractionType type)
        {
            return type switch
            {
                InteractionType.Comment => "comment",
                InteractionType.Verified => "verified",
                InteractionType.Warning => "warning",
                _ => throw new ArgumentOutOfRangeException(nameof(type), "Tipo de reacción desconocido")
            };
        }
    }
}
