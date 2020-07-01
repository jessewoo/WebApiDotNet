using System.Collections.Generic;
using ActivityTracker.Models;

namespace ActivityTracker.Data
{
  public interface IActivityTrackerRepo
  {
    bool SaveChanges();

    IEnumerable<Activity> GetAllActivities();
    Activity GetActivityById(int id);
    void CreateActivity(Activity activity);
  }
}