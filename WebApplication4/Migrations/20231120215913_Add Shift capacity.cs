﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication4.Migrations
{
    /// <inheritdoc />
    public partial class AddShiftcapacity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ShiftWithCapacity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Debut = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Fin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Capacite = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShiftWithCapacity", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShiftWithCapacity");
        }
    }
}