using System;
using System.Collections.Generic;
using System.Linq;
using ActivityTracker.Models;

namespace ActivityTracker.Data
{
  public class SqlActivityTrackerRepo : IActivityTrackerRepo
  {
    private readonly ActivityTrackerContext _context;

    // Constructor - want to make use of DBContext to return real data
    // Where we get DBContext from? Get it from Constructor dependency injection
    // We now have an instance of _context
    public SqlActivityTrackerRepo(ActivityTrackerContext context)
    {
      _context = context;
    }

    public void CreateActivity(Activity activity)
    {
      if (activity == null)
      {
        throw new ArgumentNullException(nameof(activity));
      }

      _context.Activities.Add(activity);
    }

    public void DeleteActivity(Activity activity)
    {
      if (activity == null)
      {
        throw new ArgumentNullException(nameof(activity));
      }

      _context.Activities.Remove(activity);
    }

    public Activity GetActivityById(int id)
    {
      return _context.Activities.FirstOrDefault(p => p.Id == id);
    }

    public IEnumerable<Activity> GetAllActivities()
    {
      return _context.Activities.ToList();
    }

    public bool SaveChanges()
    {
      return (_context.SaveChanges() >= 0);
    }

    public void UpdateActivity(Activity activity)
    {
      // Nothing
    }
  }
}