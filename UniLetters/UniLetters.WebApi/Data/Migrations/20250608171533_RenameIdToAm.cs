using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniLetters.WebApi.Data.Migrations
{
    /// <inheritdoc />
    public partial class RenameIdToAm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Grade_Students_StudentId",
                table: "Grade");

            migrationBuilder.RenameColumn(
                name: "StudentId",
                table: "Grade",
                newName: "Am");

            migrationBuilder.AddColumn<int>(
                name: "Semester",
                table: "Courses",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Grade_Students_Am",
                table: "Grade",
                column: "Am",
                principalTable: "Students",
                principalColumn: "Am",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Grade_Students_Am",
                table: "Grade");

            migrationBuilder.DropColumn(
                name: "Semester",
                table: "Courses");

            migrationBuilder.RenameColumn(
                name: "Am",
                table: "Grade",
                newName: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Grade_Students_StudentId",
                table: "Grade",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Am",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
