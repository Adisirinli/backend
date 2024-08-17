using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEndProjectEdu.Data.Migrations
{
    /// <inheritdoc />
    public partial class addDateTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Courses",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Blogs",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Blogs");
        }
    }
}
