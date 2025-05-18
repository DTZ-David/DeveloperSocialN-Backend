using Developer.Application.UseCases.Posts.Dtos;
using Developer.Application.UseCases.Posts.Queries.GetUserPostForFeed;
using Developer.Domain.Common.Enums;
using Developer.Domain.Common.Exceptions;
using Developer.Domain.Common.Wrappers.CustomResponse;
using Developer.Domain.Ports;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Developer.Application.UseCases.Posts.Queries.GetUsersComments
{
    public class GetUserCommentCommandHandler : IRequestHandler<GetUserCommentsCommand, ActionResult<Response<IEnumerable<CommentDto>>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetUserCommentCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ActionResult<Response<IEnumerable<CommentDto>>>> Handle(GetUserCommentsCommand request, CancellationToken cancellationToken)
        {
            var claims = await _unitOfWork.ClaimsService.GetUserClaim();
            if (claims is null)
            {
                throw new BusinessException("Error de autenticidad", (int)MessageStatusCode.BadRequest);
            }

            var user = await _unitOfWork.UserService.GetUserById(claims.UserId);
            if (user is null)
            {
                throw new BusinessException("Usuario no encontrado", (int)MessageStatusCode.BadRequest);
            }

            // Obtener posts de usuarios seguidos + propios
            var allPosts = await _unitOfWork.PostService.GetUserPostForFeed(user.Social.Following, claims.UserId);

            var result = new List<CommentDto>();

            foreach (var post in allPosts)
            {
                if (post.Comments is null) continue;

                foreach (var comment in post.Comments)
                {
                    bool isOwn = comment.AuthorId == claims.UserId;
                    bool isReceived = post.AuthorId == claims.UserId && !isOwn;

                    if (isOwn || isReceived)
                    {
                        result.Add(new CommentDto(
                            Id : comment.Id,
                            PostId: post.Id,
                            CommentText: comment.Comment,
                            InteractionType: comment.InteractionType,
                            AuthorId: comment.AuthorId,
                            AuthorProfilePic: user.ProfilePicture!,
                            UserName: user.Username,
                            SentAt: comment.CreationDate.ToString(),
                            IsOwnComment: isOwn
                        ));
                    }
                }
            }

            return new OkObjectResult(
           new Response<IEnumerable<CommentDto>>((int)MessageStatusCode.Succes, result)
       );
        }

       
    }
}
