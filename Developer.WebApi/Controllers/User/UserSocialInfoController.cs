using Developer.Application.UseCases.User.Dtos;
using Developer.Application.UseCases.Users.Commands.FollowersUser;
using Developer.Application.UseCases.Users.Dtos;
using Developer.Application.UseCases.Users.Queries.GetUserByUsername;
using Developer.Application.UseCases.Users.Queries.GetUserPostsByEmail;
using Developer.Domain.Common.Wrappers.CustomResponse;
using Developer.WebApi.Common.Constants;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Developer.WebApi.Controllers.User;

/// <summary>
/// Controller for managing areas related operations.
/// </summary>
[ApiController]
[Route(BaseRoute.BaseRouteUrl)]
public class UserSocialInfoController : BaseController
{
    private readonly IMediator _mediator;

    public UserSocialInfoController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Permite seguir o dejar de seguir a un usuario.
    /// </summary>
    /// <param name="command">Datos del seguimiento</param>
    /// <returns>Resultado con la información de seguidores</returns>
    [HttpPut]
    [Route("follow")]
    public async Task<ActionResult<Response<FollowersDto>>> FollowOrUnfollowUser([FromBody] UpdateFollowersUserCommand command)
    {
        return await Mediator.Send(command);
    }

    /// <summary>
    /// Permite seguir o dejar de seguir a un usuario.
    /// </summary>
    /// <param name="command">Datos del seguimiento</param>
    /// <returns>Resultado con la información de seguidores</returns>
    [HttpPost]
    [Route("GetUserByUsername")]
    public async Task<ActionResult<Response<UserDto>>> GetUserByUsername([FromBody] GetUserByUsernameCommand command)
    {
        return await Mediator.Send(command);
    }

    /// <summary>
    /// Permite seguir o dejar de seguir a un usuario.
    /// </summary>
    /// <param name="command">Datos del seguimiento</param>
    /// <returns>Resultado con la información de seguidores</returns>
    [HttpPost]
    [Route("GetUserPostByEmail")]
    public async Task<ActionResult<Response<UserPostsDto>>> GetUserPostByEmail([FromBody] GetUserPostsByEmailCommand command)
    {
        return await Mediator.Send(command);
    }
}
