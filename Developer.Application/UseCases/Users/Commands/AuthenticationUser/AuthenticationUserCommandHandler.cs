using Developer.Application.UseCases.Users.Dtos;
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

namespace Developer.Application.UseCases.Users.Commands.AuthenticationUser
{
    class AuthenticationUserCommandHandler : IRequestHandler<AuthenticationUserCommand, ActionResult<Response<AuthenticationUserDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AuthenticationUserCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ActionResult<Response<AuthenticationUserDto>>> Handle(AuthenticationUserCommand request, CancellationToken cancellationToken)
        {
            var token = await _unitOfWork.AccountService.ValidateMobileApp(request.Email, request.Password);
            var user = await _unitOfWork.UserService.GetUserByEmail(request.Email); 
            if (user is null)
            {
                throw new BusinessException($"Usuario no encontrado.",
                    (int)MessageStatusCode.NotFound);
            }

            var userPosts = await _unitOfWork.PostService.GetUserPostById(user.Id);


            var userDetails = new AuthenticationUserDto(
                    Token: token,
                    Username: user.Username,
                    ProfilePicture: user.ProfilePicture!,
                    Bio: user.Bio!,
                    PostsCount: userPosts.Count,
                    FollowersCount: user.Social.Followers.Count);
             

            return new Response<AuthenticationUserDto>((int)MessageStatusCode.Succes, userDetails);
        }
    }
}
