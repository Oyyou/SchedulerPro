using SchedulerPro.DAL.Models;

namespace SchedulerPro.API.Interfaces
{
  public interface IJwtService
  {
    public string GenerateToken(User user);
  }
}
