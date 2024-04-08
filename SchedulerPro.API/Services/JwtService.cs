using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SchedulerPro.API.Interfaces;
using SchedulerPro.API.Models;
using SchedulerPro.DAL.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SchedulerPro.API.Services
{
  public class JwtService : IJwtService
  {
    private readonly AppSettings _appSettings;

    public JwtService(IOptions<AppSettings> appSettings)
    {
      _appSettings = appSettings.Value;
    }

    public string GenerateToken(User user)
    {
      var tokenHandler = new JwtSecurityTokenHandler();
      var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

      var tokenDescriptor = new SecurityTokenDescriptor
      {
        Issuer = _appSettings.Issuer,
        Audience = _appSettings.Audience,
        Subject = new ClaimsIdentity(new[]
        {
          new Claim(ClaimTypes.Name, user.Id.ToString()),
          new Claim(ClaimTypes.NameIdentifier, user.Email)
        }),
        Expires = DateTime.UtcNow.AddDays(7), // Expires after a week
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
      };
      var token = tokenHandler.CreateToken(tokenDescriptor);
      return tokenHandler.WriteToken(token);
    }
  }
}
