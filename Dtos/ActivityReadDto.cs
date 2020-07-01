namespace ActivityTracker.Dtos
{
  public class ActivityReadDto
  {
    public long Id { get; set; }
    public string ActivityType { get; set; }
    public int Met { get; set; }
    public int DailyGoal { get; set; }
  }
}