using System.Collections.Generic;
using ActivityTracker.Models;

namespace ActivityTracker.Data
{
  public class MockActivityTrackerRepo : IActivityTrackerRepo
  {
    public void CreateActivity(Activity activity)
    {
      throw new System.NotImplementedException();
    }

    public void DeleteActivity(Activity activity)
    {
      throw new System.NotImplementedException();
    }

    public Activity GetActivityById(int id)
    {
      return new Activity { Id = 1, ActivityType = "steps", Met = 1000, TotalGoal = 900, DailyGoal = 100 };
    }

    public IEnumerable<Activity> GetAllActivities()
    {
      var activities = new List<Activity>
      {
        new Activity { Id = 1, ActivityType = "steps", Met = 1000, TotalGoal = 900, DailyGoal = 100 },
        new Activity { Id = 2, ActivityType = "calories", Met = 2000, TotalGoal = 200, DailyGoal = 200 },
        new Activity { Id = 3, ActivityType = "miles", Met = 9, TotalGoal = 5, DailyGoal = 2 }
      };

      return activities;

    }

    public bool SaveChanges()
    {
      throw new System.NotImplementedException();
    }

    public void UpdateActivity(Activity activity)
    {
      throw new System.NotImplementedException();
    }
  }
}