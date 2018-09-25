using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Booking.API.Migrations
{
    public partial class CreateBookingMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "booking_detail_hilo",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "booking_hilo",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "Passenger_hilo",
                incrementBy: 10);

            migrationBuilder.CreateTable(
                name: "Booking",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    FlightId = table.Column<int>(nullable: false),
                    TotalPassenger = table.Column<int>(nullable: false),
                    TripDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Booking", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Passenger",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 100, nullable: false),
                    IndentityNo = table.Column<string>(maxLength: 20, nullable: false),
                    LastName = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Passenger", x => x.Id);
                    table.UniqueConstraint("AlternateKey_IndentityNo", x => x.IndentityNo);
                });

            migrationBuilder.CreateTable(
                name: "BookingDetail",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    BookingId = table.Column<int>(nullable: false),
                    Note = table.Column<string>(maxLength: 250, nullable: true),
                    PassengerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookingDetail_Booking_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Booking",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookingDetail_Passenger_PassengerId",
                        column: x => x.PassengerId,
                        principalTable: "Passenger",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookingDetail_BookingId",
                table: "BookingDetail",
                column: "BookingId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingDetail_PassengerId",
                table: "BookingDetail",
                column: "PassengerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookingDetail");

            migrationBuilder.DropTable(
                name: "Booking");

            migrationBuilder.DropTable(
                name: "Passenger");

            migrationBuilder.DropSequence(
                name: "booking_detail_hilo");

            migrationBuilder.DropSequence(
                name: "booking_hilo");

            migrationBuilder.DropSequence(
                name: "Passenger_hilo");
        }
    }
}
