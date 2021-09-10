using Microsoft.EntityFrameworkCore.Migrations;

namespace GarageThree.Migrations
{
    public partial class postmerge : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhonNumber",
                table: "Owners");

            migrationBuilder.AddColumn<int>(
                name: "MembersOverviewId",
                table: "Vehicles",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Owners",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MembersOverview",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OwnerId = table.Column<int>(type: "int", nullable: false),
                    VehicleCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MembersOverview", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_MembersOverviewId",
                table: "Vehicles",
                column: "MembersOverviewId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_MembersOverview_MembersOverviewId",
                table: "Vehicles",
                column: "MembersOverviewId",
                principalTable: "MembersOverview",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_MembersOverview_MembersOverviewId",
                table: "Vehicles");

            migrationBuilder.DropTable(
                name: "MembersOverview");

            migrationBuilder.DropIndex(
                name: "IX_Vehicles_MembersOverviewId",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "MembersOverviewId",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Owners");

            migrationBuilder.AddColumn<int>(
                name: "PhonNumber",
                table: "Owners",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
