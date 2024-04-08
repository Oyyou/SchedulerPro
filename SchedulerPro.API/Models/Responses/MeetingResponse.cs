namespace SchedulerPro.API.Models.Responses
{
  public class MeetingResponse(DAL.Models.Meeting meeting)
  {
    public Guid Id { get; set; } = meeting.Id;
    public string Name { get; set; } = meeting.Name;
    public DateTime StartTime { get; set; } = meeting.Start;
    public DateTime EndTime { get; set; } = meeting.End;
    public IEnumerable<UserResponse> Attendees { get; set; } = meeting.Attendees.Select(user => new UserResponse(user));
  }
}
