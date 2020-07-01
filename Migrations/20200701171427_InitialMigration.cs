using Microsoft.EntityFrameworkCore.Migrations;

namespace ActivityTracker.Migrations
{
  public partial class InitialMigration : Migration
  {
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.CreateTable(
          name: "Activities",
          columns: table => new
          {
            Id = table.Column<long>(nullable: false)
                  .Annotation("SqlServer:Identity", "1, 1"),
            ActivityType = table.Column<string>(maxLength: 250, nullable: false),
            Met = table.Column<int>(nullable: false),
            TotalGoal = table.Column<int>(nullable: false),
            DailyGoal = table.Column<int>(nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_Activities", x => x.Id);
          });
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropTable(
          name: "Activities");
    }
  }
}
