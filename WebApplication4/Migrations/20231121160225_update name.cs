using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication4.Migrations
{
    /// <inheritdoc />
    public partial class updatename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Fin",
                table: "ShiftWithCapacity",
                newName: "Start");

            migrationBuilder.RenameColumn(
                name: "Debut",
                table: "ShiftWithCapacity",
                newName: "End");

            migrationBuilder.RenameColumn(
                name: "Capacite",
                table: "ShiftWithCapacity",
                newName: "Capacity");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Start",
                table: "ShiftWithCapacity",
                newName: "Fin");

            migrationBuilder.RenameColumn(
                name: "End",
                table: "ShiftWithCapacity",
                newName: "Debut");

            migrationBuilder.RenameColumn(
                name: "Capacity",
                table: "ShiftWithCapacity",
                newName: "Capacite");
        }
    }
}
