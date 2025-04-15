using Developer.Application.UseCases.Posts.Commands;
using Developer.Application.UseCases.Posts.Dtos;
using Developer.Application.UseCases.Posts.Queries.GetUserPostForFeed;
using Developer.Domain.Common.Wrappers.CustomResponse;
using Developer.WebApi.Common.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Developer.WebApi.Controllers.Posts;

/// <summary>
/// Controller for managing areas related operations.
/// </summary>
[ApiController]
[Route(BaseRoute.BaseRouteUrl)]
public class UserPostsController : BaseController
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
    public async Task<ActionResult<Response<UserPostsDto>>> CreateUser(string language, [FromBody] CreateUserPostsCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpGet]
    [Route("GetFeed")]
    public async Task<ActionResult<Response<IEnumerable<UserPostsDto>>>> GetUserPostsForFeed()
    {
        return await Mediator.Send(new GetUserPostsForFeedCommand());
    }

    [HttpGet("test-token")]
    [Authorize]
    public IActionResult TestToken()
    {
        return Ok(new
        {
            IsAuthenticated = User.Identity.IsAuthenticated,
            Claims = User.Claims.Select(c => new { c.Type, c.Value })
        });
    }

    [Authorize]
    [HttpGet("token/test")]
    public IActionResult TestToken2()
    {
        var userId = User.FindFirst("user_id")?.Value;
        var email = User.FindFirst("email")?.Value;

        return Ok(new { userId, email });
    }


}
