using System.Security.Claims;

namespace SchedulerPro.API.Interfaces
{
  public interface ITokenValidationService
  {
    ClaimsPrincipal ValidateToken(string token);
  }
}
