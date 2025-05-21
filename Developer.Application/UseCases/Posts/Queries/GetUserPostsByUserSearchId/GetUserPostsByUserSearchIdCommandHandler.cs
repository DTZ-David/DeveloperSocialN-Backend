using AutoMapper;
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

namespace Developer.Application.UseCases.Posts.Queries.GetUserPostsByUserSearchId
{
    public class GetUserPostsByUserSearchIdCommandHandler : IRequestHandler<GetUserPostsByUserSearchIdCommand, ActionResult<Response<IEnumerable<UserPostsDto>>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetUserPostsByUserSearchIdCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ActionResult<Response<IEnumerable<UserPostsDto>>>> Handle(GetUserPostsByUserSearchIdCommand request, CancellationToken cancellationToken)
        {
            var claims = await _unitOfWork.ClaimsService.GetUserClaim();
            if (claims is null)
            {
                throw new BusinessException("Error de autenticidad", (int)MessageStatusCode.BadRequest);
            }

            // Obtener lista de usuarios que sigue el usuario autenticado
            var user = await _unitOfWork.UserService.GetUserByEmail(request.email);
            if (user is null)
            {
                throw new BusinessException("Usuario no encontrado", (int)MessageStatusCode.BadRequest);
            }

            var userPosts = await _unitOfWork.PostService.GetUserPostById(user.Id);

            // Mapear los posts a DTO
            var userPostsDto = new List<UserPostsDto>();

            foreach (var post in userPosts)
            {
               
                var postAuthor = await _unitOfWork.UserService.GetUserById(post.AuthorId);
                if (postAuthor != null)
                {
                    // Crear el DTO con los datos del usuario y del post
                    try
                    {
                        var postDto = new UserPostsDto(
                            post.Id,
                            post.AuthorId,
                            post.CreationDate.ToString(),
                            post.CodeLanguage,
                            post.CodeSnippet,
                            post.Description,
                            post.Tags ?? new List<string>(),
                            post.Likes,
                            postAuthor.Username,
                            postAuthor.ProfilePicture ?? string.Empty,
                            post.Reactions ?? new Dictionary<string, int>()
                        );

                        userPostsDto.Add(postDto);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error al mapear post {post.Id}: {ex.Message}");
                    }

                }
            }

            // Devolver respuesta exitosa
            return new OkObjectResult(
                new Response<IEnumerable<UserPostsDto>>((int)MessageStatusCode.Succes, userPostsDto)
            );

        }
    }
}
