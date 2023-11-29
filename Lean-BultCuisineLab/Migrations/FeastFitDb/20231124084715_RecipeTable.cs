using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lean_BultCuisineLab.Migrations.FeastFitDb
{
    public partial class RecipeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "recipes",
                columns: table => new
                {
                    RecipeId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IDOfRecipe = table.Column<long>(type: "bigint", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaxCalories = table.Column<int>(type: "int", nullable: false),
                    ProteinAmount = table.Column<float>(type: "real", nullable: false),
                    CarbAmount = table.Column<float>(type: "real", nullable: false),
                    RecipeImage = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_recipes", x => x.RecipeId);
                });

        }
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "recipes");

        }
    }
}
