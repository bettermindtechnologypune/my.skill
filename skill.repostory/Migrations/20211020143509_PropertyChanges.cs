using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace skill.repository.Migrations
{
    public partial class PropertyChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdminId",
                table: "business_unit");

            migrationBuilder.RenameColumn(
                name: "CreateDate",
                table: "Organization",
                newName: "CreatedDate");

            migrationBuilder.RenameColumn(
                name: "CreateDate",
                table: "Employee",
                newName: "CreatedDate");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Global_Config",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Global_Config");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "Organization",
                newName: "CreateDate");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "Employee",
                newName: "CreateDate");

            migrationBuilder.AddColumn<Guid>(
                name: "AdminId",
                table: "business_unit",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");
        }
    }
}
