using AutoMapper;
using Developer.Application.UseCases.ActivityLog.Dtos;
using Developer.Domain.Common.Enums;
using Developer.Domain.Common.Wrappers.CustomResponse;
using Developer.Domain.Entities.User;
using Developer.Domain.Ports.Configuration.Localization;
using Developer.Domain.Ports;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Developer.Domain.Common.Exceptions;

namespace Developer.Application.UseCases.ActivityLog.Command;

public class RegisterInteractionCommandHandler : IRequestHandler<RegisterInteractionCommand, Response<InteractionLogDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILocalizationService _localizationService;

    public RegisterInteractionCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, ILocalizationService localizationService)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _localizationService = localizationService;
    }

    public async Task<Response<InteractionLogDto>> Handle(RegisterInteractionCommand request, CancellationToken cancellationToken)
    {
        var claims = await _unitOfWork.ClaimsService.GetUserClaim();
        if (claims is null)
        {
            throw new BusinessException("Error de autenticidad", (int)MessageStatusCode.BadRequest);
        }

        var interaction = new InteractionLog(
            claims.UserId,
            request.TargetPostId,
            request.Type,
            request.Content
        );

        var result = await _unitOfWork.InteractionUserLogService.CreateUserLog(interaction);
        var dto = _mapper.Map<InteractionLogDto>(result);

        return new Response<InteractionLogDto>((int)MessageStatusCode.Create, dto);
    }
}
