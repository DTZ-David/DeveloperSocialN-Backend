using AutoMapper;
using Developer.Application.UseCases.User.Dtos;
using Developer.Application.UseCases.Users.Commands.CreateUsers;
using Developer.Application.UseCases.Users.Dtos;
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
using Developer.Domain.Entities.User;

namespace Developer.Application.UseCases.Users.Commands.FollowersUser;

public record UpdateFollowesUserCommandHandler : IRequestHandler<UpdateFollowersUserCommand, ActionResult<Response<FollowersDto>>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILocalizationService _localizationService;

    public UpdateFollowesUserCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, ILocalizationService localizationService)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _localizationService = localizationService;
    }
    public async Task<ActionResult<Response<FollowersDto>>> Handle(UpdateFollowersUserCommand request, CancellationToken cancellationToken)
    {
        var claims = await _unitOfWork.ClaimsService.GetUserClaim();
        if (claims is null)
        {
            throw new BusinessException("Error de autenticidad", (int)MessageStatusCode.BadRequest);
        }

        var user = await _unitOfWork.UserService.GetUserById(claims.UserId); // quien hace la acción
        var targetUser = await _unitOfWork.UserService.GetUserByEmail(request.followerEmail); // a quién quiere seguir/dejar de seguir

        if (user is null || targetUser is null)
        {
            throw new BusinessException("Usuario no encontrado.", (int)MessageStatusCode.NotFound);
        }

        if (user.Id == targetUser.Id)
        {
            throw new BusinessException("No puedes seguirte a ti mismo.", (int)MessageStatusCode.BadRequest);
        }

        var isFollowing = targetUser.Social.Followers.Contains(user.Id);

        if (isFollowing)
        {
            // Dejar de seguir
            targetUser.Social.Followers.Remove(user.Id);
            user.Social.Following.Remove(targetUser.Id);
        }
        else
        {
            // Seguir
            targetUser.Social.Followers.Add(user.Id);
            user.Social.Following.Add(targetUser.Id);
        }

        await _unitOfWork.UserService.UpdateUser(user);
        await _unitOfWork.UserService.UpdateUser(targetUser);

        var dto = new FollowersDto(
            targetUser.Id,
            targetUser.Social.Followers.Count,
            targetUser.Social.Followers
        );

        var response = new Response<FollowersDto>((int)MessageStatusCode.Succes, dto);
        return new OkObjectResult(response);
    }


}
