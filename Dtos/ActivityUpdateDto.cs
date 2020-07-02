using System.ComponentModel.DataAnnotations;

namespace ActivityTracker.Dtos
{
  public class ActivityUpdateDto
  {
    // Id is created by the database

    [Required]
    [MaxLength(250)]
    public string ActivityType { get; set; }

    [Required]
    public int Met { get; set; }

    [Required]
    public int DailyGoal { get; set; }

    [Required]
    public int TotalGoal { get; set; }
  }
}