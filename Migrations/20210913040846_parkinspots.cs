using Microsoft.EntityFrameworkCore.Migrations;

namespace GarageThree.Migrations
{
    public partial class parkinspots : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parkings_ParkingSpots_ParkingSpotId",
                table: "Parkings");

            migrationBuilder.DropForeignKey(
                name: "FK_Parkings_Vehicles_VehicleId",
                table: "Parkings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Parkings",
                table: "Parkings");

            migrationBuilder.RenameTable(
                name: "Parkings",
                newName: "VehicleParkingSpot");

            migrationBuilder.RenameIndex(
                name: "IX_Parkings_VehicleId",
                table: "VehicleParkingSpot",
                newName: "IX_VehicleParkingSpot_VehicleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VehicleParkingSpot",
                table: "VehicleParkingSpot",
                columns: new[] { "ParkingSpotId", "VehicleId" });

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleParkingSpot_ParkingSpots_ParkingSpotId",
                table: "VehicleParkingSpot",
                column: "ParkingSpotId",
                principalTable: "ParkingSpots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleParkingSpot_Vehicles_VehicleId",
                table: "VehicleParkingSpot",
                column: "VehicleId",
                principalTable: "Vehicles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VehicleParkingSpot_ParkingSpots_ParkingSpotId",
                table: "VehicleParkingSpot");

            migrationBuilder.DropForeignKey(
                name: "FK_VehicleParkingSpot_Vehicles_VehicleId",
                table: "VehicleParkingSpot");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VehicleParkingSpot",
                table: "VehicleParkingSpot");

            migrationBuilder.RenameTable(
                name: "VehicleParkingSpot",
                newName: "Parkings");

            migrationBuilder.RenameIndex(
                name: "IX_VehicleParkingSpot_VehicleId",
                table: "Parkings",
                newName: "IX_Parkings_VehicleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Parkings",
                table: "Parkings",
                columns: new[] { "ParkingSpotId", "VehicleId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Parkings_ParkingSpots_ParkingSpotId",
                table: "Parkings",
                column: "ParkingSpotId",
                principalTable: "ParkingSpots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Parkings_Vehicles_VehicleId",
                table: "Parkings",
                column: "VehicleId",
                principalTable: "Vehicles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
