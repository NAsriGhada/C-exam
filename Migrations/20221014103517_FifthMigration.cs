using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace csharpexam.Migrations
{
    public partial class FifthMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ActUnit",
                table: "MeetUps",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActUnit",
                table: "MeetUps");
        }
    }
}
