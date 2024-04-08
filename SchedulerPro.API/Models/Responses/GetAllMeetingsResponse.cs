namespace SchedulerPro.API.Models.Responses
{
  public class GetAllMeetingsResponse
  {
    public IEnumerable<MeetingResponse> Meetings { get; set; }

    public GetAllMeetingsResponse(IEnumerable<DAL.Models.Meeting> meetings)
    {
      Meetings = meetings.Select(u => new MeetingResponse(u))
        .OrderBy(m => m.StartTime);
    }
  }
}
