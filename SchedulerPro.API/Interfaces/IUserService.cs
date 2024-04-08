using SchedulerPro.API.Models.Requests;
using SchedulerPro.DAL.Models;

namespace SchedulerPro.API.Interfaces
{
  public interface IUserService
  {
    User Authenticate(LoginRequest request);
    User Register(RegisterRequest request);
    User GetUserById(Guid id);
    User GetUserByIdWithMeetings(Guid id);
    Task DeleteUser(Guid id);
    IEnumerable<User> GetAllUsers();
  }
}
