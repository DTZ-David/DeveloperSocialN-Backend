using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Developer.Application.UseCases.ActivityLog.Dtos;

public record InteractionLogDto(
    string UserId,
    string TargetPostId,
    string Type,
    string? Content
);

