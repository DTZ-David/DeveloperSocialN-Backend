using AutoMapper;
using Developer.Application.UseCases.User.Dtos;
using Developer.Domain.Common.Wrappers.CustomResponse;
using Developer.Domain.Ports.Configuration.Localization;
using Developer.Domain.Ports;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Developer.Domain.Common.Enums;



namespace Developer.Application.UseCases.User.Commands
{
    public record CreatePostCommandHandler : IRequestHandler<CreateUserPostsCommand, ActionResult<Response<UserPostsDto>>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CreatePostCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ActionResult<Response<UserPostsDto>>> Handle(CreateUserPostsCommand request, CancellationToken cancellationToken)
        {
            // Crear la entidad userPosts con los datos del comando
            var userPosts = new Domain.Entities.userPosts(
                authorId: request.AuthorId,
                codeSnippet: request.CodeSnippet,
                description: request.Description,
                language: request.Language,
                tags: request.Tags
            );

            // Guardar el userPosts en la base de datos
            await _unitOfWork.PostService.CreatePostAsync(userPosts);

            // Mapear la entidad userPosts a UserPostsDto
            var UserPostsDto = _mapper.Map<UserPostsDto>(userPosts);

            // Crear la respuesta
            var response = new Response<UserPostsDto>((int)MessageStatusCode.Create, UserPostsDto);

            // Devolver el resultado creado
            return new CreatedResult(string.Empty, response);
        }
    }
}