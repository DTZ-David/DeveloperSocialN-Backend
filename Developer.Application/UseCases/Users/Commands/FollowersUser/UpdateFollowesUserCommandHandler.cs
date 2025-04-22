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
        var user = await _unitOfWork.UserService.GetUserById(request.UserId);
        var follower = await _unitOfWork.UserService.GetUserById(request.FollowerId);

        if (user is null || follower is null)
        {
            throw new BusinessException($"Usuario no encontrado.",
                (int)MessageStatusCode.NotFound);
        }

        if (request.UserId == request.FollowerId)
        {
            throw new BusinessException("No puedes seguirte a ti mismo.",
                (int)MessageStatusCode.BadRequest);
        }

        var userFollowers = user.Social.Followers;
        var followerFollowing = follower.Social.Following;

        if (request.IsFollowAction)
        {
            if (!userFollowers.Contains(request.FollowerId))
                userFollowers.Add(request.FollowerId);

            if (!followerFollowing.Contains(request.UserId))
                followerFollowing.Add(request.UserId);
        }
        else
        {
            userFollowers.Remove(request.FollowerId);
            followerFollowing.Remove(request.UserId);
        }

        await _unitOfWork.UserService.UpdateUser(user);
        await _unitOfWork.UserService.UpdateUser(follower);

        var dto = new FollowersDto(
             user.Id,
             user.Social.Followers.Count,
             user.Social.Followers
         );


        var response = new Response<FollowersDto>((int)MessageStatusCode.Succes, dto);
        return new OkObjectResult(response);

    }

}
