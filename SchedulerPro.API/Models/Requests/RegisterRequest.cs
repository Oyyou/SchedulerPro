namespace SchedulerPro.API.Models.Requests
{
  public class RegisterRequest
  {
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string TimeZoneId { get; set; }
  }
}
