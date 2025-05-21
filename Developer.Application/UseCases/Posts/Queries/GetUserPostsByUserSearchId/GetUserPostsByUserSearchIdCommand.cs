using Developer.Domain.Common.Wrappers.CustomResponse;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Developer.Application.UseCases.Posts.Queries.GetUserPostsByUserSearchId;

public record GetUserPostsByUserSearchIdCommand(string email) : IRequest<ActionResult<Response<IEnumerable<UserPostsDto>>>>;
