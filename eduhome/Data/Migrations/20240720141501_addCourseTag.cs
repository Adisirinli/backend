using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEndProjectEdu.Data.Migrations
{
    /// <inheritdoc />
    public partial class addCourseTag : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseSkills_Skills_SkillId",
                table: "CourseSkills");

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

            migrationBuilder.RenameColumn(
                name: "SkillId",
                table: "CourseSkills",
                newName: "TagId");

            migrationBuilder.RenameIndex(
                name: "IX_CourseSkills_SkillId",
                table: "CourseSkills",
                newName: "IX_CourseSkills_TagId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseSkills_Skills_TagId",
                table: "CourseSkills",
                column: "TagId",
                principalTable: "Skills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseSkills_Skills_TagId",
                table: "CourseSkills");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Skills",
                newName: "University");

            migrationBuilder.RenameColumn(
                name: "TagId",
                table: "CourseSkills",
                newName: "SkillId");

            migrationBuilder.RenameIndex(
                name: "IX_CourseSkills_TagId",
                table: "CourseSkills",
                newName: "IX_CourseSkills_SkillId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_CourseSkills_Skills_SkillId",
                table: "CourseSkills",
                column: "SkillId",
                principalTable: "Skills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
