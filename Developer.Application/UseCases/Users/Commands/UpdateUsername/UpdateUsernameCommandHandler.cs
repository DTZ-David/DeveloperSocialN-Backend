using AutoMapper;
using Developer.Application.UseCases.User.Dtos;
using Developer.Application.UseCases.Users.Commands.UpdateProfilePicture;
using Developer.Domain.Common.Wrappers.CustomResponse;
using Developer.Domain.Ports.Configuration.Localization;
using Developer.Domain.Ports;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Developer.Domain.Common.Enums;
using Developer.Domain.Common.Exceptions;

namespace Developer.Application.UseCases.Users.Commands.UpdateUsername
{
    public class UpdateUsernameCommandHandler : IRequestHandler<UpdateUsernameCommand, ActionResult<Response<UserDto>>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILocalizationService _localizationService;

        public UpdateUsernameCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, ILocalizationService localizationService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _localizationService = localizationService;
        }

        public async Task<ActionResult<Response<UserDto>>> Handle(UpdateUsernameCommand request, CancellationToken cancellationToken)
        {
            var claims = await _unitOfWork.ClaimsService.GetUserClaim();
            if (claims is null)
            {
                throw new BusinessException("Error de autenticidad", (int)MessageStatusCode.BadRequest);
            }

            var user = await _unitOfWork.UserService.GetUserById(claims.UserId);
            if (user == null)
            {
                throw new BusinessException("Usuario no encontrado", (int)MessageStatusCode.NotFound);
            }

            if (request.Username == null || request.Username.Length == 0)
            {
                throw new BusinessException("No se recibió una imagen válida", (int)MessageStatusCode.BadRequest);
            }


            user.Username = request.Username;

            await _unitOfWork.UserService.UpdateUser(user);

            var userDto = new UserDto(
                   Email: user.Email,
                    UserName: request.Username,
                    ProfilePicture: user.ProfilePicture,
                    Bio: user.Bio,
                    PostsCount: 0,
                    FollowersCount: 0,
                    CurrentFollow: false
            );

            var response = new Response<UserDto>((int)MessageStatusCode.Succes, userDto);
            return new OkObjectResult(response);
        }
    }
}
