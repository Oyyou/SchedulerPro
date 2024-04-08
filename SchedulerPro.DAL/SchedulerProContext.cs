using Microsoft.EntityFrameworkCore;
using SchedulerPro.DAL.Models;

namespace SchedulerPro.DAL
{
  public class SchedulerProContext : DbContext
  {
    public DbSet<User> Users { get; set; }

    public DbSet<Meeting> Meetings { get; set; }

    public SchedulerProContext(DbContextOptions<SchedulerProContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      var users = new User[]
      {
        new() {
          Id = Guid.NewGuid(),
          FirstName = "Niall",
          LastName = "Lewin",
          Email = "niall.lewin@hotmail.com",
          PasswordHash = BCrypt.Net.BCrypt.HashPassword("Password123!"),
          TimeZoneId = "Europe/London",
        },
        new() {
          Id = Guid.NewGuid(),
          FirstName = "John",
          LastName = "Davis",
          Email = "john.davis@hotmail.com",
          PasswordHash = BCrypt.Net.BCrypt.HashPassword("Password123!"),
          TimeZoneId = "Europe/Berlin",
        },
        new() {
          Id = Guid.NewGuid(),
          FirstName = "Kat",
          LastName = "Jones",
          Email = "kat.jones@hotmail.com",
          PasswordHash = BCrypt.Net.BCrypt.HashPassword("Password123!"),
          TimeZoneId = "Asia/Tokyo",
        },
      };

      modelBuilder.Entity<User>().HasData(users);
    }
  }
}
