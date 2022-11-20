using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace csharpexam.Migrations
{
    public partial class DhouhaMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ActivityDate",
                table: "MeetUps",
                newName: "Date");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Date",
                table: "MeetUps",
                newName: "ActivityDate");
        }
    }
}
