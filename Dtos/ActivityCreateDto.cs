namespace ActivityTracker.Dtos
{
  public class ActivityCreateDto
  {
    // Id is created by the database
    public string ActivityType { get; set; }
    public int Met { get; set; }
    public int DailyGoal { get; set; }
    public int TotalGoal { get; set; }
  }
}