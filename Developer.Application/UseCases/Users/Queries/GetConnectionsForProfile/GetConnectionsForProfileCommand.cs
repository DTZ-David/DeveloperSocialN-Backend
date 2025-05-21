using Developer.Application.UseCases.User.Dtos;
using Developer.Domain.Common.Wrappers.CustomResponse;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Developer.Application.UseCases.Users.Queries.GetConnectionsForProfile
{
    public record GetConnectionsForProfileCommand : IRequest<ActionResult<Response<IEnumerable<UserDto>>>>;

}
