using Developer.Application.UseCases.Posts.Commands;
using Developer.Application.UseCases.Posts.Commands.CreateUserPostCommand;
using Developer.Application.UseCases.Posts.Dtos;
using Developer.Application.UseCases.Posts.Queries.GetUserPostById;
using Developer.Application.UseCases.Posts.Queries.GetUserPostForFeed;
using Developer.Application.UseCases.Posts.Queries.GetUserPostsByUserSearchId;
using Developer.Application.UseCases.Posts.Queries.GetUsersComments;
using Developer.Application.UseCases.Posts.Queries.GetUsersCommentsByPostId;
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
    [Authorize]
    [HttpPost]
    [Route("CreatePost")]
    public async Task<ActionResult<Response<UserPostsDto>>> CreateUser(string language, [FromBody] CreateUserPostsCommand command)
    {
        return await Mediator.Send(command);
    }

    [Authorize]
    [HttpPost]
    [Route("AddComment")]
    public async Task<ActionResult<Response<bool>>> AddComment(string language, [FromBody] AddCommentCommand command)
    {
        return await Mediator.Send(command);
    }

    [Authorize]
    [HttpGet]
    [Route("GetFeed")]
    public async Task<ActionResult<Response<IEnumerable<UserPostsDto>>>> GetUserPostsForFeed()
    {
        return await Mediator.Send(new GetUserPostsForFeedCommand());
    }

    [Authorize]
    [HttpGet]
    [Route("GetUserPostById")]
    public async Task<ActionResult<Response<IEnumerable<UserPostsDto>>>> GetUserPostById()
    {
        return await Mediator.Send(new GetUserPostsByIdCommand());
    }

    [Authorize]
    [HttpGet]
    [Route("GetUserInteraction")]
    public async Task<ActionResult<Response<IEnumerable<CommentDto>>>> GetUserComments()
    {
        return await Mediator.Send(new GetUserCommentsCommand());
    }
    [Authorize]
    [HttpGet]
    [Route("GetCommentByPostId")]
    public async Task<ActionResult<Response<IEnumerable<CommentDto>>>> GetUserCommentsByPostId([FromQuery] string postId)
    {
        return await Mediator.Send(new GetUsersCommentsByPostIdCommand(postId));
    }

    [Authorize]
    [HttpGet]
    [Route("GetUserPostByUserIdSearch")]
    public async Task<ActionResult<Response<IEnumerable<UserPostsDto>>>> GetUserPostByUserIdSearch([FromQuery] string email)
    {
        return await Mediator.Send(new GetUserPostsByUserSearchIdCommand(email));
    }


}
