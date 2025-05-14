using AutoMapper;
using Developer.Application.UseCases.Posts.Dtos;
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

namespace Developer.Application.UseCases.Posts.Queries.GetUserPostForFeed;

class GetUserPostsForFeedCommandHandler : IRequestHandler<GetUserPostsForFeedCommand, ActionResult<Response<IEnumerable<UserPostsDto>>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetUserPostsForFeedCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ActionResult<Response<IEnumerable<UserPostsDto>>>> Handle(GetUserPostsForFeedCommand request, CancellationToken cancellationToken)
    {
        // Obtener claims del usuario autenticado
        var claims = await _unitOfWork.ClaimsService.GetUserClaim();
        if (claims is null)
        {
            throw new BusinessException("Error de autenticidad", (int)MessageStatusCode.BadRequest);
        }

        // Obtener lista de usuarios que sigue el usuario autenticado
        var user = await _unitOfWork.UserService.GetUserById(claims.UserId);
        if (user is null)
        {
            throw new BusinessException("Usuario no encontrado", (int)MessageStatusCode.BadRequest);
        }

        // Obtener posts de usuarios seguidos + propios
        var userPosts = await _unitOfWork.PostService.GetUserPostForFeed(user.Social.Following, claims.UserId);

        // Mapear los posts a DTO
        var userPostsDto = new List<UserPostsDto>();

        foreach (var post in userPosts)
        {
            // Obtener información del usuario asociado a cada post
            var postAuthor = await _unitOfWork.UserService.GetUserById(post.AuthorId);
            if (postAuthor != null)
            {
                // Crear el DTO con los datos del usuario y del post
                var postDto = new UserPostsDto(
                    post.AuthorId,
                    post.CreationDate.ToString(),
                    post.CodeLanguage,
                    post.CodeSnippet,
                    post.Comments,
                    post.Description,
                    post.Tags,
                    post.Likes,
                    postAuthor.Username,
                    postAuthor.ProfilePicture!,
                    post.Reactions
                );

                userPostsDto.Add(postDto);
            }
        }

        // Devolver respuesta exitosa
        return new OkObjectResult(
            new Response<IEnumerable<UserPostsDto>>((int)MessageStatusCode.Succes, userPostsDto)
        );
    }


}
