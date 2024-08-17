using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEndProjectEdu.Data.Migrations
{
    /// <inheritdoc />
    public partial class addSkillProps : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Skills",
                newName: "University");

            migrationBuilder.AddColumn<string>(
                name: "Course",
                table: "Skills",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Education",
                table: "Skills",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Events",
                table: "Skills",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Learning",
                table: "Skills",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Teachers",
                table: "Skills",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Course",
                table: "Skills");

            migrationBuilder.DropColumn(
                name: "Education",
                table: "Skills");

            migrationBuilder.DropColumn(
                name: "Events",
                table: "Skills");

            migrationBuilder.DropColumn(
                name: "Learning",
                table: "Skills");

            migrationBuilder.DropColumn(
                name: "Teachers",
                table: "Skills");

            migrationBuilder.RenameColumn(
                name: "University",
                table: "Skills",
                newName: "Name");
        }
    }
}
