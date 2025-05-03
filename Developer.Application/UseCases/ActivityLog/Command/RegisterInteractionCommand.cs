using Developer.Application.UseCases.ActivityLog.Dtos;
using Developer.Domain.Common.Wrappers.CustomResponse;
using Developer.Domain.Entities.User;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Developer.Application.UseCases.ActivityLog.Command;

public record RegisterInteractionCommand(
  string TargetPostId,
  InteractionType Type,
  string? Content
) : IRequest<Response<InteractionLogDto>>;
