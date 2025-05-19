using AutoMapper;
using Developer.Application.UseCases.User.Dtos;
using Developer.Application.UseCases.Users.Queries.GetUserByUsername;
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

namespace Developer.Application.UseCases.Users.Queries.GetUserPostsByEmail
{
    public class GetUserPostsByEmailCommandHandler : IRequestHandler<GetUserPostsByEmailCommand, ActionResult<Response<UserPostsDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetUserPostsByEmailCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ActionResult<Response<UserPostsDto>>> Handle(GetUserPostsByEmailCommand request, CancellationToken cancellationToken)
        {
            var claims = await _unitOfWork.ClaimsService.GetUserClaim();
            if (claims is null)
            {
                throw new BusinessException("Error de autenticidad", (int)MessageStatusCode.BadRequest);
            }

            var user = await _unitOfWork.UserService.GetUserByEmail(request.Email);
            if (user is null)
            {
                throw new BusinessException("Usuario no encontrado", (int)MessageStatusCode.BadRequest);
            }

            var userPosts = await _unitOfWork.PostService.GetUserPostById(user.Id);

            var userPostsDto = userPosts.Select(post => new UserPostsDto(
                Id: post.Id,
                AuthorId: post.AuthorId,
                FechaPublicacion: post.CreationDate.ToString(),
                CodeLanguage: post.CodeLanguage,
                CodeSnippet: post.CodeSnippet,
                Description: post.Description,
                Tags: post.Tags,
                Likes: post.Likes,
                UserName: user.Username,
                ProfilePicture: user.ProfilePicture!,
                Reactions: post.Reactions
            )).ToList();


            return new OkObjectResult(
                new Response<List<UserPostsDto>>((int)MessageStatusCode.Succes, userPostsDto)
            );
        }
    }
}
