using AutoMapper;
using Developer.Application.UseCases.User.Dtos;
using Developer.Domain.Common.Enums;
using Developer.Domain.Common.Exceptions;
using Developer.Domain.Common.Wrappers.CustomResponse;
using Developer.Domain.Entities.Posts;
using Developer.Domain.Ports;
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
                throw new BusinessException("Usuario no encontrado", (int)MessageStatusCode.BadRequest);
            }

            var userDto = new UserDto(user.Email, user.Username);

            return new OkObjectResult(
                new Response<UserDto>((int)MessageStatusCode.Succes, userDto)
            );
        }
    }
}
