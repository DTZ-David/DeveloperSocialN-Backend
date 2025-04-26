using Developer.Domain.Entities;
using Developer.Domain.Ports.Configuration.Localization;
using Developer.Domain.Ports;
using Developer.Domain.Ports.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Developer.Domain.Entities.User;
using Developer.Domain.Common.Enums;
using Developer.Domain.Common.Exceptions;

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

    public async Task<User> GetUserByEmail(string email)
    {
        var user = (await _clientRepository.FindAsync(
             u => u.Email == email)).FirstOrDefault();
        _ = user ?? throw new BusinessException(_localizationService.GetLocalizedByKey(MessageCode.NotFound),
            (int)MessageStatusCode.NotFound);
        return user!;
    }

    public async Task<User> GetUserById(string id)
    {
        var user = await _clientRepository.GetById(id);
        _ = user ?? throw new BusinessException(_localizationService.GetLocalizedByKey(MessageCode.NotFound),
            (int)MessageStatusCode.NotFound);
        return user!;
    }
    public async Task<User> UpdateUser(User user)
    {
        var existingUser = await _clientRepository.GetById(user.Id);
        if (existingUser == null)
        {
            throw new BusinessException(_localizationService.GetLocalizedByKey(MessageCode.NotFound),
                (int)MessageStatusCode.NotFound);
        }

        await _clientRepository.Update(user);
        return user;
    }


}
