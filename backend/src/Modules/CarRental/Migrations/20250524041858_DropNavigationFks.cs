using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRental.Migrations
{
    /// <inheritdoc />
    public partial class DropNavigationFks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Rentals_RentalId",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Rentals_Cars_CarModelId",
                table: "Rentals");

            migrationBuilder.DropIndex(
                name: "IX_Rentals_CarModelId",
                table: "Rentals");

            migrationBuilder.DropIndex(
                name: "IX_Payments_RentalId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "CarModelId",
                table: "Rentals");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CarModelId",
                table: "Rentals",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_CarModelId",
                table: "Rentals",
                column: "CarModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_RentalId",
                table: "Payments",
                column: "RentalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Rentals_RentalId",
                table: "Payments",
                column: "RentalId",
                principalTable: "Rentals",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rentals_Cars_CarModelId",
                table: "Rentals",
                column: "CarModelId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
