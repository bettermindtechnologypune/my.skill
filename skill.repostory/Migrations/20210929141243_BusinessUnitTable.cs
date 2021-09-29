using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace skill.repository.Migrations
{
    public partial class BusinessUnitTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
         migrationBuilder.CreateTable(
            name: "business_unit",
            columns: table => new
            {
               Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
               Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
               Email = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
               CompanyAddress = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
               City = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
               State = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
               PostalCode = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
               ContactNumber = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
               WebSite = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                    .Annotation("MySql:CharSet", "utf8mb4"),
               OrgId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
               CreatedBy = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
               ModifiedBy = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                    .Annotation("MySql:CharSet", "utf8mb4"),
               CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
               ModifiedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true)
            },
            constraints: table =>
            {
               table.PrimaryKey("PK_business_unit", x => x.Id);
            })
            .Annotation("MySql:CharSet", "utf8mb4");
      }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
