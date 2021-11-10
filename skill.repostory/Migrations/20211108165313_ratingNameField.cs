using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace skill.repository.Migrations
{
    public partial class ratingNameField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BUID",
                table: "LevelTwo");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Rating",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Rating");

            migrationBuilder.AddColumn<Guid>(
                name: "BUID",
                table: "LevelTwo",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");
        }
    }
}
