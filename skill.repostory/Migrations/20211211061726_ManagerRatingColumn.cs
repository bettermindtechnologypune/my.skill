using Microsoft.EntityFrameworkCore.Migrations;

namespace skill.repository.Migrations
{
    public partial class ManagerRatingColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsManagerRating",
                table: "Rating");

            migrationBuilder.AddColumn<int>(
                name: "ManagerRating",
                table: "Rating",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ManagerRating",
                table: "Rating");

            migrationBuilder.AddColumn<bool>(
                name: "IsManagerRating",
                table: "Rating",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }
    }
}
