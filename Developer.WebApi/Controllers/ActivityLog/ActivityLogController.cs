using Developer.Application.UseCases.ActivityLog.Command;
using Developer.Application.UseCases.ActivityLog.Dtos;
using Developer.Application.UseCases.Posts.Commands;
using Developer.Application.UseCases.Posts.Dtos;
using Developer.Domain.Common.Wrappers.CustomResponse;
using Developer.WebApi.Common.Constants;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Developer.WebApi.Controllers.ActivityLog;

/// <summary>
/// Controller for managing areas related operations.
/// </summary>
[ApiController]
[Route(BaseRoute.BaseRouteUrl)]
public class ActivityLogController : BaseController
{
    /// <summary>
    /// Registra una interacción (like, comentario, etc.).
    /// </summary>
    /// <param name="command">Comando con los datos de la interacción</param>
    /// <returns>Interacción registrada</returns>
    [HttpPost]
    public async Task<ActionResult<Response<InteractionLogDto>>> CreateUser(string language, [FromBody] RegisterInteractionCommand command)
    {
        return await Mediator.Send(command);
    }


}
