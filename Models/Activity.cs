using System.ComponentModel.DataAnnotations;

namespace ActivityTracker.Models
{
  public class Activity
  {
    [Key]
    public long Id { get; set; }

    [Required]
    [MaxLength(250)]
    public string ActivityType { get; set; }

    [Required]
    public int Met { get; set; }

    [Required]
    public int TotalGoal { get; set; }

    [Required]
    public int DailyGoal { get; set; }
  }
}