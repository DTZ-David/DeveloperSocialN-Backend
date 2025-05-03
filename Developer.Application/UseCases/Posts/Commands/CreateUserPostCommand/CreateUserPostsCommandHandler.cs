using AutoMapper;
using Developer.Domain.Common.Wrappers.CustomResponse;
using Developer.Domain.Ports;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Developer.Domain.Common.Enums;
using Developer.Application.UseCases.Posts.Dtos;
using Developer.Domain.Entities.Posts;
using Developer.Domain.Common.Exceptions;
using Developer.Domain.Ports.Configuration.Localization;



namespace Developer.Application.UseCases.Posts.Commands.CreateUserPostCommand
{
    public record CreatePostCommandHandler : IRequestHandler<CreateUserPostsCommand, ActionResult<Response<UserPostsDto>>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILocalizationService _localizationService;

     

        public CreatePostCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, ILocalizationService localizationService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _localizationService = localizationService;
        }

        public async Task<ActionResult<Response<UserPostsDto>>> Handle(CreateUserPostsCommand request, CancellationToken cancellationToken)
        {

            var userClaim = await _unitOfWork.ClaimsService.GetUserClaim();


            if (userClaim is null)
            {
             throw new BusinessException(_localizationService.GetLocalizedByKey(MessageCode.UnauthorizedToken),
             (int)MessageStatusCode.Conflict);
            }
           
            
                var commentsPostUser = new List<Comments>();

                var userPosts = new UserPosts(
                    authorId: userClaim.UserId,
                    codeSnippet: request.CodeSnippet,
                    comments: commentsPostUser,
                    description: request.Description,
                    likes: 0,
                    tags: request.Tags!,
                    reactions: new List<Reaction>()
                );

                await _unitOfWork.PostService.CreatePostAsync(userPosts);

                var UserPostsDto = _mapper.Map<UserPostsDto>(userPosts);

                
                var response = new Response<UserPostsDto>((int)MessageStatusCode.Create, UserPostsDto);

                
                return new CreatedResult(string.Empty, response);
            
           
        }
    }
}