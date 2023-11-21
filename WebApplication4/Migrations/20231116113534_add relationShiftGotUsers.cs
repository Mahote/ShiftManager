using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication4.Migrations
{
    /// <inheritdoc />
    public partial class addrelationShiftGotUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shifts_Users_UserId",
                table: "Shifts");

            migrationBuilder.DropIndex(
                name: "IX_Shifts_UserId",
                table: "Shifts");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Shifts");

            migrationBuilder.AddColumn<int>(
                name: "UsersId",
                table: "Shifts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Shifts_UsersId",
                table: "Shifts",
                column: "UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Shifts_Users_UsersId",
                table: "Shifts",
                column: "UsersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shifts_Users_UsersId",
                table: "Shifts");

            migrationBuilder.DropIndex(
                name: "IX_Shifts_UsersId",
                table: "Shifts");

            migrationBuilder.DropColumn(
                name: "UsersId",
                table: "Shifts");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Shifts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Shifts_UserId",
                table: "Shifts",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Shifts_Users_UserId",
                table: "Shifts",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
