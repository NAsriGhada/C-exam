using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace csharpexam.Migrations
{
    public partial class OyviMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Date",
                table: "MeetUps",
                newName: "ActivityDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ActivityDate",
                table: "MeetUps",
                newName: "Date");
        }
    }
}
