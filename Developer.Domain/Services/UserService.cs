using Developer.Domain.Entities;
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
class UserService : IUserService
{
    private readonly IGenericRepository<User> _clientRepository;
    private readonly ILocalizationService _localizationService;

    public UserService(IGenericRepository<User> clientRepository, ILocalizationService localizationService)
    {
        _clientRepository = clientRepository;
        _localizationService = localizationService;
    }

    public async Task<User> CreateUserAsync(User usuario)
    {
        await _clientRepository.Add(usuario);
        return usuario;
    }
}
