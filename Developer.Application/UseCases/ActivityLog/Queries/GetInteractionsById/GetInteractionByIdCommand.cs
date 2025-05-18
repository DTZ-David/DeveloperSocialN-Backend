using Developer.Domain.Common.Wrappers.CustomResponse;
using Developer.Domain.Entities.Posts;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Developer.Application.UseCases.ActivityLog.Queries.GetInteractionsById
{
    class GetInteractionByIdCommand : IRequest<ActionResult<Response<Comments>>>;
}
