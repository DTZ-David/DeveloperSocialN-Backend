using AutoMapper;
using Developer.Application.UseCases.Posts.Dtos;
using Developer.Domain.Common.Enums;
using Developer.Domain.Common.Wrappers.CustomResponse;
using Developer.Domain.Ports;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Developer.Application.UseCases.Posts.Queries.GetUserPostForFeed;

class GetUserPostsForFeedCommandHandler : IRequestHandler<GetUserPostsForFeedCommand, ActionResult<Response<IEnumerable<UserPostsDto>>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetUserPostsForFeedCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ActionResult<Response<IEnumerable<UserPostsDto>>>> Handle(GetUserPostsForFeedCommand request, CancellationToken cancellationToken)
    {
        var claims = await _unitOfWork.ClaimsService.GetUserClaim();
        var userPosts = await _unitOfWork.PostService.GetUserPostForFeed();
        //var userPosts = categoriesSearch.Select(x => _mapper.Map<TransportationCategoryDto>(x));
        var userPostsDto = _mapper.Map<IEnumerable<UserPostsDto>>(userPosts);


        return new OkObjectResult(new Response<IEnumerable<UserPostsDto>>((int)MessageStatusCode.Succes, userPostsDto));
    }
}
