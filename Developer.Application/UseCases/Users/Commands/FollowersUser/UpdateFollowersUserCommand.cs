using Developer.Application.UseCases.User.Dtos;
using Developer.Application.UseCases.Users.Dtos;
using Developer.Domain.Common.Wrappers.CustomResponse;
using Developer.Domain.Entities.User;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Developer.Application.UseCases.Users.Commands.FollowersUser;

public record UpdateFollowersUserCommand(
 string followerEmail) : IRequest<ActionResult<Response<FollowersDto>>>;
