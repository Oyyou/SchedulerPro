namespace SchedulerPro.API.Tests.Controllers.UsersControllerTests
{
  public class DeleteUserTests : UsersControllerBase
  {
    [Fact]
    public async Task DeleteUser_ShouldReturnOk_WhenServiceWorks()
    {
      var userId = Guid.NewGuid();
      _repositoryMock.Setup(r => r.DeleteUser(userId)).Returns(Task.CompletedTask);

      var result = await _controller.DeleteUser(userId) as ObjectResult;
      Assert.NotNull(result);
      Assert.Equal(200, result.StatusCode);
    }

    [Fact]
    public async Task DeleteUser_ShouldReturnBadRequest_ServiceFails()
    {
      var userId = Guid.NewGuid();

      _repositoryMock.Setup(r => r.DeleteUser(userId)).Throws(new Exception("Something went wrong"));
      var result = await _controller.DeleteUser(userId) as ObjectResult;
      Assert.NotNull(result);
      Assert.Equal(400, result.StatusCode);
    }
  }
}
