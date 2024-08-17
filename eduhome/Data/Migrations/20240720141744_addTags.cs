using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEndProjectEdu.Data.Migrations
{
    /// <inheritdoc />
    public partial class addTags : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseSkills_Courses_CourseId",
                table: "CourseSkills");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseSkills_Skills_TagId",
                table: "CourseSkills");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Skills",
                table: "Skills");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseSkills",
                table: "CourseSkills");

            migrationBuilder.RenameTable(
                name: "Skills",
                newName: "Tags");

            migrationBuilder.RenameTable(
                name: "CourseSkills",
                newName: "CourseTags");

            migrationBuilder.RenameIndex(
                name: "IX_CourseSkills_TagId",
                table: "CourseTags",
                newName: "IX_CourseTags_TagId");

            migrationBuilder.RenameIndex(
                name: "IX_CourseSkills_CourseId",
                table: "CourseTags",
                newName: "IX_CourseTags_CourseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tags",
                table: "Tags",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseTags",
                table: "CourseTags",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseTags_Courses_CourseId",
                table: "CourseTags",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseTags_Tags_TagId",
                table: "CourseTags",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseTags_Courses_CourseId",
                table: "CourseTags");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseTags_Tags_TagId",
                table: "CourseTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tags",
                table: "Tags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseTags",
                table: "CourseTags");

            migrationBuilder.RenameTable(
                name: "Tags",
                newName: "Skills");

            migrationBuilder.RenameTable(
                name: "CourseTags",
                newName: "CourseSkills");

            migrationBuilder.RenameIndex(
                name: "IX_CourseTags_TagId",
                table: "CourseSkills",
                newName: "IX_CourseSkills_TagId");

            migrationBuilder.RenameIndex(
                name: "IX_CourseTags_CourseId",
                table: "CourseSkills",
                newName: "IX_CourseSkills_CourseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Skills",
                table: "Skills",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseSkills",
                table: "CourseSkills",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseSkills_Courses_CourseId",
                table: "CourseSkills",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseSkills_Skills_TagId",
                table: "CourseSkills",
                column: "TagId",
                principalTable: "Skills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
