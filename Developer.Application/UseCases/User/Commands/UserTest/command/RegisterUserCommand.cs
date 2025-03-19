using Developer.Application.UseCases.User.Dtos;
using Developer.Domain.Common.Wrappers.CustomResponse;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Developer.Application.UseCases.User.Commands.UserTest.command;

public record RegisterUserCommand(
    string email,
    string userName,
    string password
    ) : IRequest<ActionResult<Response<string>>>;
