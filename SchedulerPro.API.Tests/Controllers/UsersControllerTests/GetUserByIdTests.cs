namespace SchedulerPro.API.Tests.Controllers.UsersControllerTests
{
  public class GetUserByIdTests : UsersControllerBase
  {
    [Fact]
    public void GetUserById_ShouldReturnUser_WhenServiceWorks()
    {
      var userId = Guid.NewGuid();
      _repositoryMock.Setup(r => r.GetUserById(userId))
        .Returns(new User()
        {
          Id = Guid.NewGuid(),
          FirstName = "fn",
          LastName = "ln",
          Email = "em@gmail.com",
          TimeZoneId = "Here/Now",
        });

      var result = _controller.GetUserById(userId) as ObjectResult;
      Assert.NotNull(result);
      Assert.Equal(200, result.StatusCode);

      var response = result.Value as GetUserByIdResponse;
      Assert.Equal("fn", response!.User.FirstName);
      Assert.Equal("ln", response!.User.LastName);
    }

    [Fact]
    public void GetUserById_ShouldReturnBadRequest_ServiceFails()
    {
      var userId = Guid.NewGuid();

      _repositoryMock.Setup(r => r.GetUserById(userId)).Throws(new Exception("Something wicked came this way"));
      var result = _controller.GetUserById(userId) as ObjectResult;
      Assert.NotNull(result);
      Assert.Equal(400, result.StatusCode);
    }
  }
}
