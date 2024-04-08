namespace SchedulerPro.API.Models.Requests
{
  public class CreateMeetingRequest
  {
    public string Name { get; set; }
    public string StartTime { get; set; }
    public int Duration { get; set; }
    public Guid[] AttendeeIds { get; set; }
    public string TimeZoneId { get; set; }
  }
}
