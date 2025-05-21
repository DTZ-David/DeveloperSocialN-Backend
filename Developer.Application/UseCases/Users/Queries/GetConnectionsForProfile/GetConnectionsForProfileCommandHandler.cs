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

namespace Developer.Application.UseCases.Users.Queries.GetConnectionsForProfile
{
    class GetConnectionsForProfileCommandHandler : IRequestHandler<GetConnectionsForProfileCommand, ActionResult<Response<IEnumerable<UserDto>>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetConnectionsForProfileCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ActionResult<Response<IEnumerable<UserDto>>>> Handle(GetConnectionsForProfileCommand request, CancellationToken cancellationToken)
        {
            var claims = await _unitOfWork.ClaimsService.GetUserClaim();
            if (claims is null)
            {
                throw new BusinessException("Error de autenticidad", (int)MessageStatusCode.BadRequest);
            }

            var profileUser = await _unitOfWork.UserService.GetUserById(claims.UserId);

            // Set con los usuarios que el perfil sigue (para chequear si sigue a sus followers)
            var currentUserFollowingIds = profileUser.Social.Following.ToHashSet();

            var userDtos = new List<UserDto>();

            // FOLLOWING (usuarios a los que sigue)
            foreach (var userId in profileUser.Social.Following)
            {
                var user = await _unitOfWork.UserService.GetUserById(userId);
                if (user == null) continue;

                var dto = new UserDto(
                    user.Email,
                    user.Username,
                    user.ProfilePicture!,
                    user.Bio!,
                    0, // postsCount
                    user.Social.Followers.Count, // followersCount
                    true // porque sí lo está siguiendo
                );

                userDtos.Add(dto);
            }

            // FOLLOWERS (usuarios que lo siguen, pero puede que no los siga de vuelta)
            foreach (var userId in profileUser.Social.Followers)
            {
                var user = await _unitOfWork.UserService.GetUserById(userId);
                if (user == null) continue;

                // Evitamos duplicar si ya está en la lista desde el loop anterior
                if (userDtos.Any(u => u.Email == user.Email))
                    continue;

                var dto = new UserDto(
                    user.Email,
                    user.Username,
                    user.ProfilePicture!,
                    user.Bio!,
                    0,
                    user.Social.Followers.Count,
                    currentUserFollowingIds.Contains(user.Id) 
                );

                userDtos.Add(dto);
            }

            return new OkObjectResult(new Response<List<UserDto>>((int)MessageStatusCode.Succes, userDtos));
        }

    }
}
