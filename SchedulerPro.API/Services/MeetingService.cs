using Microsoft.EntityFrameworkCore;
using SchedulerPro.API.Interfaces;
using SchedulerPro.API.Models.Requests;
using SchedulerPro.DAL;
using SchedulerPro.DAL.Models;

namespace SchedulerPro.API.Services
{
  public class MeetingService : IMeetingService
  {
    private readonly SchedulerProContext _context;

    public MeetingService(
      SchedulerProContext context)
    {
      _context = context;
    }

    public async Task<Meeting> CreateMeeting(CreateMeetingRequest request)
    {
      var startTime = DateTime.Parse(request.StartTime);
      var timeZone = TimeZoneInfo.FindSystemTimeZoneById(request.TimeZoneId);
      var startTimeUtc = TimeZoneInfo.ConvertTimeToUtc(startTime, timeZone);

      var endTimeUtc = startTimeUtc.AddMinutes(request.Duration);

      var meeting = new Meeting()
      {
        Name = request.Name,
        Start = startTimeUtc,
        End = endTimeUtc,
        Attendees = request.AttendeeIds.Select(id => _context.Users.FirstOrDefault(u => u.Id == id)).Where(u => u != null).ToList(),
      };

      var newMeeting = await _context.Meetings.AddAsync(meeting);
      await _context.SaveChangesAsync();
      return newMeeting.Entity;
    }

    public async Task DeleteMeeting(Guid id)
    {
      var meeting = await _context.Meetings.FirstOrDefaultAsync(m => m.Id == id);
      if (meeting == null)
      {
        return;
      }

      _context.Remove(meeting);
      await _context.SaveChangesAsync();
    }

    public async Task RemoveUserFromMeeting(Guid meetingId, Guid userId)
    {
      var meeting = await _context.Meetings
        .Include(m => m.Attendees)
        .FirstOrDefaultAsync(m => m.Id == meetingId);
      if (meeting == null)
      {
        return;
      }

      var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
      if (user == null)
      {
        return;
      }

      meeting.Attendees.Remove(user);

      if (meeting.Attendees.Count == 0)
      {
        _context.Remove(meeting);
      }

      await _context.SaveChangesAsync();
    }

    public IEnumerable<Meeting> GetAllMeetings() => _context.Meetings.Include(m => m.Attendees);
  }
}
