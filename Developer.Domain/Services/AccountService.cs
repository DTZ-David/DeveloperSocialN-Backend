using Developer.Domain.Common.Enums;
using Developer.Domain.Common.Exceptions;
using Developer.Domain.Ports.Configuration.JsonWebToken;
using Developer.Domain.Ports.Services;
using Developer.Domain.Ports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Developer.Domain.Ports.Configuration.Localization;
using Developer.Domain.Entities;

namespace Developer.Domain.Services;


[ApplicationService]
public class AccountService : IAccountService
{
    private readonly IGenericRepository<User> _userRepository;
    private readonly IJwtService _jwtService;
    private readonly ILocalizationService _localization;

    public AccountService(IGenericRepository<User> userRepository,
                          IJwtService jwtService,
                          ILocalizationService localization)
    {
        _userRepository = userRepository;
        _jwtService = jwtService;
        _localization = localization;
    }


 

    public async Task<string> ValidateMobileApp(string email, string password)
    {
        List<string> claimsValue = [];
        var findUser = await ValidateCredentials(email, password);

        // Public Claims 
        claimsValue.Add(findUser.Id);
        claimsValue.Add(findUser.Email);
        claimsValue.Add("4076de2f-91cc-4e5c-9bb9-e252489ef313");

        var token = _jwtService.BuildToken(claimsValue);

        return token;
    }

    private async Task<User> ValidateCredentials(string email, string password)
    {
        var user = (await _userRepository.FindAsync(
                    u => u.Email == email && u.Password == password)).FirstOrDefault();

        if (user == null)
        {
            throw new BusinessException(_localization.GetLocalizedByKey(MessageCode.IncorrectCredentials), (int)MessageStatusCode.Unauthorized);
        }

        return user;
    }
}
