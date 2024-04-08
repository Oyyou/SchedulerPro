namespace SchedulerPro.API.Models.Responses
{
  public class GetAllUsersResponse
  {
    public IEnumerable<UserResponse> Users { get; set; }

    public GetAllUsersResponse(IEnumerable<DAL.Models.User> users)
    {
      Users = users.Select(u => new UserResponse(u));
    }
  }
}
