using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Flight.API.Migrations
{
    public partial class CreateFlightMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "flight_hilo",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "flight_model_hilo",
                incrementBy: 10);

            migrationBuilder.CreateTable(
                name: "FlightModel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Capacity = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Type = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlightModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Flight",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    ArrivalCity = table.Column<string>(maxLength: 200, nullable: false),
                    ArrivalTime = table.Column<TimeSpan>(nullable: false),
                    DepartureCity = table.Column<string>(maxLength: 200, nullable: false),
                    DepartureTime = table.Column<TimeSpan>(nullable: false),
                    FlightModelId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flight", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Flight_FlightModel_FlightModelId",
                        column: x => x.FlightModelId,
                        principalTable: "FlightModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Flight_FlightModelId",
                table: "Flight",
                column: "FlightModelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Flight");

            migrationBuilder.DropTable(
                name: "FlightModel");

            migrationBuilder.DropSequence(
                name: "flight_hilo");

            migrationBuilder.DropSequence(
                name: "flight_model_hilo");
        }
    }
}
