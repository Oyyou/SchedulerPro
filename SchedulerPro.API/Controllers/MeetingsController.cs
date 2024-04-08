using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchedulerPro.API.Interfaces;
using SchedulerPro.API.Models.Requests;
using SchedulerPro.API.Models.Responses;
using SchedulerPro.API.Services;

namespace SchedulerPro.API.Controllers
{
  [ApiController]
  [Route("api/meetings")]
  public class MeetingsController : ControllerBase
  {
    private readonly IMeetingService _meetingService;

    public MeetingsController(
      IMeetingService userService)
    {
      _meetingService = userService;
    }

    [HttpGet]
    [Authorize]
    public IActionResult GetAllMeetings()
    {
      try
      {
        var meetings = _meetingService.GetAllMeetings();

        return Ok(new GetAllMeetingsResponse(meetings));
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
        return BadRequest(new { error = "Failed to get all meetings" });
      }
    }

    [HttpPost("create")]
    [Authorize]
    public async Task<IActionResult> CreateMeeting([FromBody] CreateMeetingRequest request)
    {
      try
      {
        var meeting = await _meetingService.CreateMeeting(request);

        return Ok(new { meeting = new MeetingResponse(meeting) });
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
        return BadRequest(new { error = "Failed to create meeting" });
      }
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteMeeting(Guid id)
    {
      try
      {
        await _meetingService.DeleteMeeting(id);
        return Ok();
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
        return BadRequest(new { error = $"Failed to get delete meeting with id '{id}'" });
      }
    }

    [HttpDelete("{meetingId}/removeuser/{userId}")]
    [Authorize]
    public async Task<IActionResult> RemoveUserFromMeeting(Guid meetingId, Guid userId)
    {
      try
      {
        await _meetingService.RemoveUserFromMeeting(meetingId, userId);
        return Ok();
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
        return BadRequest(new { error = $"Failed to remove user with id '{userId}' from meeting with id '{meetingId}'" });
      }
    }
  }
}
