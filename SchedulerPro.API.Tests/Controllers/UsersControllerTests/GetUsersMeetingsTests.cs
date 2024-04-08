namespace SchedulerPro.API.Tests.Controllers.UsersControllerTests
{
  public class GetUsersMeetingsTests : UsersControllerBase
  {
    [Fact]
    public void GetUsersMeetings_ShouldReturnMeetings_WhenServiceWorks()
    {
      var userId = Guid.NewGuid();
      var userWithMeetings = new User { Id = userId, Meetings = [new(), new()] };
      _repositoryMock.Setup(r => r.GetUserByIdWithMeetings(userId)).Returns(userWithMeetings);

      var result = _controller.GetUsersMeetings(userId) as ObjectResult;
      Assert.NotNull(result);
      Assert.Equal(200, result.StatusCode);

      var response = result.Value as GetAllMeetingsResponse;
      Assert.NotNull(response);
      Assert.Equal(2, response.Meetings.Count());
    }

    [Fact]
    public void GetUsersMeetings_ShouldReturnBadRequest_ServiceFails()
    {
      var userId = Guid.NewGuid();

      _repositoryMock.Setup(r => r.GetUserByIdWithMeetings(userId)).Throws(new Exception("Something went wrong"));
      var result = _controller.GetUsersMeetings(userId) as ObjectResult;
      Assert.NotNull(result);
      Assert.Equal(400, result.StatusCode);
    }
  }
}