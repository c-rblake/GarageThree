﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace GarageThree.Migrations
{
    public partial class NewOverView : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "Owners",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PhonNumber",
                table: "Owners",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Age",
                table: "Owners");

            migrationBuilder.DropColumn(
                name: "PhonNumber",
                table: "Owners");
        }
    }
}
