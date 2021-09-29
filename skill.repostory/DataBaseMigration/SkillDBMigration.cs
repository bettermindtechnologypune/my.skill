using Microsoft.EntityFrameworkCore.Migrations;
using skill.repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skill.repository.DataBaseMigration
{
   public class SkillDBMigration : Migration
   {
      protected override void Up(MigrationBuilder migrationBuilder)
      {
         migrationBuilder.AddColumn<string>(
               name: "BookLanguage",
               table: "organization",               
               nullable: true);
      }
   }
}
