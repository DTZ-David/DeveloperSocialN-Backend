using Developer.Application.UseCases.Posts.Dtos;
using Developer.Application.UseCases.Posts.Queries.GetUsersComments;
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

namespace Developer.Application.UseCases.Posts.Queries.GetUsersCommentsByPostId
{
    public class GetUsersCommentsByPostIdCommandHandler : IRequestHandler<GetUsersCommentsByPostIdCommand, ActionResult<Response<IEnumerable<CommentDto>>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetUsersCommentsByPostIdCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ActionResult<Response<IEnumerable<CommentDto>>>> Handle(GetUsersCommentsByPostIdCommand request, CancellationToken cancellationToken)
        {
            var claims = await _unitOfWork.ClaimsService.GetUserClaim();
            if (claims is null)
            {
                throw new BusinessException("Error de autenticidad", (int)MessageStatusCode.BadRequest);
            }

            var currentUser = await _unitOfWork.UserService.GetUserById(claims.UserId);
            if (currentUser is null)
            {
                throw new BusinessException("Usuario no encontrado", (int)MessageStatusCode.BadRequest);
            }

            var post = await _unitOfWork.PostService.GetUserPostByPostId(request.postId); // o GetCommentsByPostId si devuelve UserPosts
            if (post is null)
            {
                throw new BusinessException("Post no encontrado", (int)MessageStatusCode.NotFound);
            }

            var result = new List<CommentDto>();

            if (post.Comments != null)
            {
                foreach (var comment in post.Comments)
                {
                    var commentAuthor = await _unitOfWork.UserService.GetUserById(comment.AuthorId);
                    if (commentAuthor is null) continue; // opcional: puedes decidir cómo manejar usuarios eliminados

                    var dto = new CommentDto(
                        Id: comment.Id,
                        PostId: post.Id,
                        CommentText: comment.Comment,
                        InteractionType: comment.InteractionType,
                        AuthorId: comment.AuthorId,
                        AuthorProfilePic: commentAuthor.ProfilePicture ?? "",
                        UserName: commentAuthor.Username ?? "",
                        SentAt: comment.CreationDate.ToString("yyyy-MM-dd HH:mm:ss"), // Ajusta formato si lo necesitas
                        IsOwnComment: comment.AuthorId == claims.UserId
                    );

                    result.Add(dto);
                }
            }

            return new OkObjectResult(
                new Response<IEnumerable<CommentDto>>((int)MessageStatusCode.Succes, result)
            );
        }
    }
}
