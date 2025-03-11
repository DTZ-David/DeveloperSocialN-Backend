using Developer.Domain.Common.Enums;
using Developer.Domain.Ports.Configuration.JsonWebToken;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Developer.Infraestructure.Adapters.Configuration.JsonWebToken;

public class JwtService : IJwtService
{
    #region Constructor
    public JwtService()
    {
    }
    #endregion

    #region Public Methods
    public string BuildToken(List<string> claimsValue)
    {
        var secretKey = Encoding.UTF8.GetBytes(
            Environment.GetEnvironmentVariable("JWT_SECRET_KEY")
            ?? throw new Exception("JWT_SECRET_KEY no está configurado")
        );

        var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature);

        var encryptionKey = Encoding.UTF8.GetBytes(
            Environment.GetEnvironmentVariable("JWT_ENCRYPT_KEY")
            ?? throw new Exception("JWT_ENCRYPT_KEY no está configurado")
        );

        var encryptingCredentials = new EncryptingCredentials(new SymmetricSecurityKey(encryptionKey), SecurityAlgorithms.Aes128KW, SecurityAlgorithms.Aes128CbcHmacSha256);

        var expirationMinutes = int.Parse(
            Environment.GetEnvironmentVariable("JWT_EXPIRATION_MINUTES")
            ?? throw new Exception("JWT_EXPIRATION_MINUTES no está configurado")
        );

        var responseClaims = BuildClaims(claimsValue);

        var tokenDescription = new SecurityTokenDescriptor
        {
            IssuedAt = DateTime.UtcNow,
            NotBefore = DateTime.UtcNow,
            Expires = DateTime.UtcNow.AddMinutes(expirationMinutes),
            SigningCredentials = signingCredentials,
            EncryptingCredentials = encryptingCredentials,
            Subject = new ClaimsIdentity(responseClaims)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var securityToken = tokenHandler.CreateToken(tokenDescription);
        return tokenHandler.WriteToken(securityToken);
    }



    #endregion

    #region Private Methods
    private List<Claim> BuildClaims(List<string> claimsValue)
    {
        List<string> customClaimTypes = new()
        {
            ClaimOption.PrivateClaim,
            ClaimOption.UserId,
            ClaimOption.Email,
            ClaimOption.UserId
        };

        var responseClaims = new List<Claim>();

        for (int i = 0; i < customClaimTypes.Count; i++)
        {
            if (customClaimTypes[i].Equals(ClaimOption.PrivateClaim))
            {
                if (i == 0)
                {
                    responseClaims.Add(new Claim(ClaimTypes.Role, claimsValue[i]));
                }
            }
            else
            {
                responseClaims.Add(new Claim(customClaimTypes[i], claimsValue[i]));
            }
        }

        return responseClaims;
    }
    #endregion
}