using SchedulerPro.API.Models.Requests;
using SchedulerPro.DAL.Models;

namespace SchedulerPro.API.Interfaces
{
  public interface IMeetingService
  {
    IEnumerable<Meeting> GetAllMeetings();
    Task<Meeting> CreateMeeting(CreateMeetingRequest request);
    Task DeleteMeeting(Guid id);
    Task RemoveUserFromMeeting(Guid meetingId, Guid userId);
  }
}
