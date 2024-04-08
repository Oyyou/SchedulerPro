namespace SchedulerPro.API.Models.Responses
{
  public class UserResponse(DAL.Models.User user)
  {
    public Guid Id { get; set; } = user.Id;
    public string FirstName { get; set; } = user.FirstName;
    public string LastName { get; set; } = user.LastName;
    public string Email { get; set; } = user.Email;
    public string TimeZoneId { get; set; } = user.TimeZoneId;
  }
}
