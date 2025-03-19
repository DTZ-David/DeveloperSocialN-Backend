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

class RegisterUserServices : IUserRegisterServices
{
    private readonly IGenericRepository<RegisterUser> _clientRepository;
    private readonly ILocalizationService _localizationService;

    public RegisterUserServices(IGenericRepository<RegisterUser> clientRepository, ILocalizationService localizationService)
    {
        _clientRepository = clientRepository;
        _localizationService = localizationService;
    }

    public async Task<Entities.RegisterUser> CreateUserAsync(Entities.RegisterUser usuario)
    {
        await _clientRepository.Add(usuario);
        return usuario;
    }

    public Task<IEnumerable<Entities.RegisterUser>> GetAllUsersAsync()
    {
        throw new NotImplementedException();
    }
}