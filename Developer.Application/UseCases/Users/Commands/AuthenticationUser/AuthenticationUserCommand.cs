using Developer.Application.UseCases.Users.Dtos;
using Developer.Domain.Common.Wrappers.CustomResponse;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Developer.Application.UseCases.Users.Commands.AuthenticationUser;

public record AuthenticationUserCommand(string Email, string Password) : IRequest<ActionResult<Response<AuthenticationUserDto>>>;


