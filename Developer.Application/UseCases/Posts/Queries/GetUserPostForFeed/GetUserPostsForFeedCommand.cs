using Developer.Application.UseCases.Posts.Dtos;
using Developer.Domain.Common.Wrappers.CustomResponse;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Developer.Application.UseCases.Posts.Queries.GetUserPostForFeed;

public class GetUserPostsForFeedCommand : IRequest<ActionResult<Response<IEnumerable<UserPostsDto>>>>;

