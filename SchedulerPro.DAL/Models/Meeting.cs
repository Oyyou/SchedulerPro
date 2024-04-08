using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerPro.DAL.Models
{
  public class Meeting
  {
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public List<User> Attendees { get; set; } = [];
  }
}
