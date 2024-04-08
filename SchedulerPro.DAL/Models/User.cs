using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerPro.DAL.Models
{
  public class User
  {
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string TimeZoneId { get; set; }

    public List<Meeting> Meetings { get; set; } = new();
  }
}
