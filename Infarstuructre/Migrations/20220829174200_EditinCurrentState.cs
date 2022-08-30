using Microsoft.EntityFrameworkCore.Migrations;

namespace Infarstuructre.Migrations
{
    public partial class EditinCurrentState : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CurrentStaut",
                table: "SubCategories",
                newName: "CurrentState");

            migrationBuilder.RenameColumn(
                name: "CurrentStaut",
                table: "Categories",
                newName: "CurrentState");

            migrationBuilder.RenameColumn(
                name: "CurrentStaut",
                table: "Books",
                newName: "CurrentState");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CurrentState",
                table: "SubCategories",
                newName: "CurrentStaut");

            migrationBuilder.RenameColumn(
                name: "CurrentState",
                table: "Categories",
                newName: "CurrentStaut");

            migrationBuilder.RenameColumn(
                name: "CurrentState",
                table: "Books",
                newName: "CurrentStaut");
        }
    }
}
