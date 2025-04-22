using Developer.Domain.Entities.Posts;
using Developer.Domain.Entities.User;
using Developer.Domain.Ports.Configuration.Localization;
using Developer.Domain.Ports;
using Developer.Domain.Ports.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Developer.Domain.Services;

[ApplicationService]
class InterationUserLogService : IInteractionUserLogService
{
    private readonly IGenericRepository<InteractionLog> _logRepository;
    private readonly ILocalizationService _localizationService;

    public InterationUserLogService(IGenericRepository<InteractionLog> logRepository, ILocalizationService localizationService)
    {
        _logRepository = logRepository;
        _localizationService = localizationService;
    }

    public async Task<InteractionLog> CreateUserLog(InteractionLog userLog)
    {
        await _logRepository.Add(userLog);
        return userLog;
    }
}
