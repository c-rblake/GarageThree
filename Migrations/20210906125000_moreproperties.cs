using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GarageThree.Migrations
{
    public partial class moreproperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Price",
                table: "VehicleType",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ReqparkingSpots",
                table: "VehicleType",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Size",
                table: "VehicleType",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "TypeName",
                table: "VehicleType",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "Vehicle",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Passengers",
                table: "Vehicle",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "RegistrationNumber",
                table: "Vehicle",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Wheels",
                table: "Vehicle",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "ArrivalTime",
                table: "Parking",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CollectTime",
                table: "Parking",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Owner",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Owner",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "VehicleType");

            migrationBuilder.DropColumn(
                name: "ReqparkingSpots",
                table: "VehicleType");

            migrationBuilder.DropColumn(
                name: "Size",
                table: "VehicleType");

            migrationBuilder.DropColumn(
                name: "TypeName",
                table: "VehicleType");

            migrationBuilder.DropColumn(
                name: "Color",
                table: "Vehicle");

            migrationBuilder.DropColumn(
                name: "Passengers",
                table: "Vehicle");

            migrationBuilder.DropColumn(
                name: "RegistrationNumber",
                table: "Vehicle");

            migrationBuilder.DropColumn(
                name: "Wheels",
                table: "Vehicle");

            migrationBuilder.DropColumn(
                name: "ArrivalTime",
                table: "Parking");

            migrationBuilder.DropColumn(
                name: "CollectTime",
                table: "Parking");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Owner");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Owner");
        }
    }
}
