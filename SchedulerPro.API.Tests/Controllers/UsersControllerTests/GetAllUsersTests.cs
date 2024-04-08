namespace SchedulerPro.API.Tests.Controllers.UsersControllerTests
{
  public class GetAllUsersTests : UsersControllerBase
  {
    [Fact]
    public void GetAllUsers_ShouldReturnAllUsers_WhenServiceWorks()
    {
      _repositoryMock.Setup(r => r.GetAllUsers())
        .Returns(new List<User>()
        {
          new()
          {
            Id = Guid.NewGuid(),
            FirstName = "fn1",
            LastName = "ln1",
            Email = "em1@gmail.com",
            TimeZoneId = "Here/Now",
          },
          new()
          {
            Id = Guid.NewGuid(),
            FirstName = "fn2",
            LastName = "ln2",
            Email = "em2@gmail.com",
            TimeZoneId = "Here/Now",
          }
        });

      var result = _controller.GetAllUsers() as ObjectResult;
      Assert.NotNull(result);
      Assert.Equal(200, result.StatusCode);

      var response = result.Value as GetAllUsersResponse;
      Assert.Equal(2, response!.Users.Count());
    }

    [Fact]
    public void GetAllUsers_ShouldReturnBadRequest_ServiceFails()
    {
      _repositoryMock.Setup(r => r.GetAllUsers()).Throws(new Exception("Something wicked came this way"));
      var result = _controller.GetAllUsers() as ObjectResult;
      Assert.NotNull(result);
      Assert.Equal(400, result.StatusCode);
    }
  }
}
