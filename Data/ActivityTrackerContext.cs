using ActivityTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace ActivityTracker.Data
{
  public class ActivityTrackerContext : DbContext
  {
    public ActivityTrackerContext(DbContextOptions<ActivityTrackerContext> opt) : base(opt)
    {

    }

    public DbSet<Activity> Activities { get; set; }

  }
}