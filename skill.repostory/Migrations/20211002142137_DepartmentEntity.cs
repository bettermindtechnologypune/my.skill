using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace skill.repository.Migrations
{
   public partial class DepartmentEntity : Migration
   {
      protected override void Up(MigrationBuilder migrationBuilder)
      {
         migrationBuilder.CreateTable(
             name: "Department",
             columns: table => new
             {
                Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                BusinessUnitId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                Name = table.Column<string>(type: "longtext", nullable: false)
                     .Annotation("MySql:CharSet", "utf8mb4"),
                ManagerId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                CreatedBy = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                     .Annotation("MySql:CharSet", "utf8mb4"),
                ModifiedBy = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                     .Annotation("MySql:CharSet", "utf8mb4"),
                CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                ModifiedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
             },
             constraints: table =>
             {
                table.PrimaryKey("PK_Department", x => x.Id);
             })
             .Annotation("MySql:CharSet", "utf8mb4");

         migrationBuilder.AddColumn<Guid>(
             name: "AdminId",
             table: "business_unit",
             type: "char(36)",
             nullable: false,
             defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
             collation: "ascii_general_ci");
      }

      protected override void Down(MigrationBuilder migrationBuilder)
      {
         migrationBuilder.DropTable(
             name: "Department");

         migrationBuilder.DropColumn(
               name: "AdminId",
               table: "business_unit");
      }
   }
}
