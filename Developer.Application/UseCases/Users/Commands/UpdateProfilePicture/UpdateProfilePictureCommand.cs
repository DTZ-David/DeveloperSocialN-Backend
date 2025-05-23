using Developer.Application.UseCases.User.Dtos;
using Developer.Application.UseCases.Users.Dtos;
using Developer.Domain.Common.Wrappers.CustomResponse;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Developer.Application.UseCases.Users.Commands.UpdateProfilePicture;

public record UpdateProfilePictureCommand(string ProfilePicture) : IRequest<ActionResult<Response<UserDto>>>;
