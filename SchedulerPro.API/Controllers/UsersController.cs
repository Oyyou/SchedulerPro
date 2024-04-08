using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchedulerPro.API.Interfaces;
using SchedulerPro.API.Models.Responses;

namespace SchedulerPro.API.Controllers
{
  [ApiController]
  [Route("api/users")]
  public class UsersController : ControllerBase
  {
    private readonly IUserService _userService;

    public UsersController(
      IUserService userService)
    {
      _userService = userService;
    }

    [HttpGet]
    [Authorize]
    public IActionResult GetAllUsers()
    {
      try
      {
        var users = _userService.GetAllUsers();

        return Ok(new GetAllUsersResponse(users));
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
        return BadRequest(new { error = "Failed to get all users" });
      }
    }

    [HttpGet("{id}")]
    [Authorize]
    public IActionResult GetUserById(Guid id)
    {
      try
      {
        var user = _userService.GetUserById(id);

        return Ok(new GetUserByIdResponse(user));
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
        return BadRequest(new { error = $"Failed to get user with id '{id}'" });
      }
    }

    [HttpGet("{id}/meetings")]
    [Authorize]
    public IActionResult GetUsersMeetings(Guid id)
    {
      try
      {
        var user = _userService.GetUserByIdWithMeetings(id);

        return Ok(new GetAllMeetingsResponse(user.Meetings));
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
        return BadRequest(new { error = $"Failed to get user with id '{id}'" });
      }
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
      try
      {
        await _userService.DeleteUser(id);

        return Ok(new { message = "User successfully deleted" });
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
        return BadRequest(new { error = $"Failed to delete user with id '{id}'" });
      }
    }
  }
}
