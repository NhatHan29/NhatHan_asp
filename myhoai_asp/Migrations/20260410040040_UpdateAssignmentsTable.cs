using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace myhoai_asp.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAssignmentsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HocKy",
                table: "Assignments",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "LopId",
                table: "Assignments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_LopId",
                table: "Assignments",
                column: "LopId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_Classes_LopId",
                table: "Assignments",
                column: "LopId",
                principalTable: "Classes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_Classes_LopId",
                table: "Assignments");

            migrationBuilder.DropIndex(
                name: "IX_Assignments_LopId",
                table: "Assignments");

            migrationBuilder.DropColumn(
                name: "HocKy",
                table: "Assignments");

            migrationBuilder.DropColumn(
                name: "LopId",
                table: "Assignments");
        }
    }
}
