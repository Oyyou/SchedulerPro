namespace SchedulerPro.API.Tests.Controllers.UsersControllerTests
{
  public class UsersControllerBase
  {
    protected readonly Mock<IUserService> _repositoryMock;
    protected readonly UsersController _controller;

    public UsersControllerBase()
    {
      _repositoryMock = new Mock<IUserService>();

      _controller = new(_repositoryMock.Object)
      {
        ControllerContext = new ControllerContext()
        {
          HttpContext = new DefaultHttpContext()
        }
      };
    }
  }
}
