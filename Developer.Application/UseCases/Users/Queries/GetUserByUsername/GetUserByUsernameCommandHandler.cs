using AutoMapper;
using Developer.Application.UseCases.User.Dtos;
using Developer.Domain.Common.Enums;
using Developer.Domain.Common.Exceptions;
using Developer.Domain.Common.Wrappers.CustomResponse;
using Developer.Domain.Entities.Posts;
using Developer.Domain.Entities.User;
using Developer.Domain.Ports;
using Developer.Domain.Settings.Claims;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Developer.Application.UseCases.Users.Queries.GetUserByUsername
{
    class GetUserByUsernameCommandHandler : IRequestHandler<GetUserByUsernameCommand, ActionResult<Response<UserDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetUserByUsernameCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ActionResult<Response<UserDto>>> Handle(GetUserByUsernameCommand request, CancellationToken cancellationToken)
        {
            var claims = await _unitOfWork.ClaimsService.GetUserClaim();
            if (claims is null)
            {
                throw new BusinessException("Error de autenticidad", (int)MessageStatusCode.BadRequest);
            }

            var user = await _unitOfWork.UserService.GetUserByUsername(request.Username);
            if (user is null)
            {
                throw new BusinessException("Error", (int)MessageStatusCode.BadRequest);
            }

            var myUser = await _unitOfWork.UserService.GetUserById(claims.UserId);

            var allPost = await _unitOfWork.PostService.GetUserPostById(claims.UserId);

            var isFollowing = false;
            foreach (var followerId in myUser.Social.Following)
            {
                if (followerId == user.Id)
                {
                    isFollowing = true;
                    break; // si quieres salir del ciclo una vez encontrado
                }
            }
            var userDto = new UserDto(user.Email, user.Username, user.ProfilePicture!, user.Bio!, allPost.Count, user.Social.Followers.Count, isFollowing);

            return new OkObjectResult(new Response<UserDto>((int)MessageStatusCode.Succes, userDto));

        }
    }
}
