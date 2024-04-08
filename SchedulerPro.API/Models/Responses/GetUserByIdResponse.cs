namespace SchedulerPro.API.Models.Responses
{
  public class GetUserByIdResponse
  {
    public UserResponse User { get; set; }

    public GetUserByIdResponse(DAL.Models.User user)
    {
      User = new UserResponse(user);
    }
  }
}
