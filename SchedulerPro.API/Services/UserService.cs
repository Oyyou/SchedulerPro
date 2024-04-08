using Microsoft.EntityFrameworkCore;
using SchedulerPro.API.Interfaces;
using SchedulerPro.API.Models.Requests;
using SchedulerPro.DAL;
using SchedulerPro.DAL.Models;

namespace SchedulerPro.API.Services
{
  public class UserService : IUserService
  {
    private readonly SchedulerProContext _context;

    public UserService(
      SchedulerProContext context)
    {
      _context = context;
    }

    public User Authenticate(LoginRequest request)
    {
      var user = _context.Users.SingleOrDefault(u => u.Email == request.Email);

      if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
      {
        return null;
      }

      return user;
    }

    public User Register(RegisterRequest request)
    {
      var newUser = new User()
      {
        FirstName = request.FirstName,
        LastName = request.LastName,
        Email = request.Email,
        PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
        TimeZoneId = request.TimeZoneId,
      };

      _context.Users.Add(newUser);
      _context.SaveChanges();

      return newUser;
    }

    public User GetUserById(Guid id) => _context.Users
      .SingleOrDefault(u => u.Id == id);

    public User GetUserByIdWithMeetings(Guid id) => _context.Users
      .Include(u => u.Meetings)
      .ThenInclude(m => m.Attendees)
      .SingleOrDefault(u => u.Id == id);

    public async Task DeleteUser(Guid id)
    {
      var user = GetUserById(id);
      _context.Remove(user);
      await _context.SaveChangesAsync();
    }

    public IEnumerable<User> GetAllUsers() => _context.Users;
  }
}
