using Developer.Application.UseCases.User.Commands;
using Developer.Application.UseCases.User.Dtos;
using Developer.Application.UseCases.User.Commands.UserTest.command;
using Developer.Domain.Common.Wrappers.CustomResponse;
using Developer.WebApi.Common.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Developer.WebApi.Controllers.User;

/// <summary>
/// Controller for managing areas related operations.
/// </summary>
[ApiController]
[Route(BaseRoute.BaseRouteUrl)]
public class UserController : BaseController
{
    
    /// <summary>
    /// Retrieves client headquarters by ID.
    /// </summary>
    /// <remarks>
    /// Get details of a client's headquarters registered in the database.
    /// </remarks>
    /// <param name="language">The language for the response (e.g., "en", "es").</param>
    /// <response code="200">Successful query.</response>
    /// <response code="404">Query error, client's headquarters not found.</response>
    [HttpPost]
    public async Task<ActionResult<Response<UserDto>>> CreateUser(string language, [FromBody] CreateUserCommand command)
    {
        return await Mediator.Send(command);
    }
    
    
}
