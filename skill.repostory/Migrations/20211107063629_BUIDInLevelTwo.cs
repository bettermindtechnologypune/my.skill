using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace skill.repository.Migrations
{
    public partial class BUIDInLevelTwo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "BUID",
                table: "LevelTwo",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BUID",
                table: "LevelTwo");
        }
    }
}
