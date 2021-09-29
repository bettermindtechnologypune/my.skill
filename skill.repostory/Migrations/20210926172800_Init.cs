using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace skill.repository.Migrations
{
   public partial class Init : Migration
   {
      protected override void Up(MigrationBuilder migrationBuilder)
      {
         migrationBuilder.AddColumn<string>(
          name: "BookLanguage",
         table: "organization",
         type: "VARCHAR(50)",
          nullable: true);
      }

      protected override void Down(MigrationBuilder migrationBuilder)
      {
        
      }
   }
}
