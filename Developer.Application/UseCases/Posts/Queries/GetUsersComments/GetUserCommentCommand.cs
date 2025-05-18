using Developer.Application.UseCases.Posts.Dtos;
using Developer.Domain.Common.Wrappers.CustomResponse;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Developer.Application.UseCases.Posts.Queries.GetUsersComments;

public record GetUserCommentsCommand : IRequest<ActionResult<Response<IEnumerable<CommentDto>>>>;
