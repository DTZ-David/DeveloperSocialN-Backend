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
    public record CreatePostCommandHandler : IRequestHandler<CreatePostCommand, ActionResult<Response<PostDto>>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CreatePostCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ActionResult<Response<PostDto>>> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            // Crear la entidad Post con los datos del comando
            var post = new Domain.Entities.Post(
                authorId: request.AuthorId,
                codeSnippet: request.CodeSnippet,
                description: request.Description,
                language: request.Language,
                tags: request.Tags
            );

            // Guardar el post en la base de datos
            await _unitOfWork.PostService.CreatePostAsync(post);

            // Mapear la entidad Post a PostDto
            var postDto = _mapper.Map<PostDto>(post);

            // Crear la respuesta
            var response = new Response<PostDto>((int)MessageStatusCode.Create, postDto);

            // Devolver el resultado creado
            return new CreatedResult(string.Empty, response);
        }
    }
}