using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SchedulerPro.API.Interfaces;
using SchedulerPro.API.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SchedulerPro.API.Services
{
  public class JwtTokenValidationService : ITokenValidationService
  {
    private readonly AppSettings _appSettings;

    public JwtTokenValidationService(IOptions<AppSettings> appSettings)
    {
      _appSettings = appSettings.Value;
    }

    public ClaimsPrincipal ValidateToken(string token)
    {
      var tokenHandler = new JwtSecurityTokenHandler();
      var validationParameters = GetValidationParameters();

      return tokenHandler.ValidateToken(token, validationParameters, out SecurityToken _);
    }

    private TokenValidationParameters GetValidationParameters()
    {
      var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

      return new TokenValidationParameters
      {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
      };
    }
  }
}
