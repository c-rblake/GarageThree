using Microsoft.EntityFrameworkCore.Migrations;

namespace GarageThree.Migrations
{
    public partial class init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MembershipId",
                table: "Owners");

            migrationBuilder.CreateIndex(
                name: "IX_Memberships_OwnerId",
                table: "Memberships",
                column: "OwnerId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Memberships_Owners_OwnerId",
                table: "Memberships",
                column: "OwnerId",
                principalTable: "Owners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Memberships_Owners_OwnerId",
                table: "Memberships");

            migrationBuilder.DropIndex(
                name: "IX_Memberships_OwnerId",
                table: "Memberships");

            migrationBuilder.AddColumn<int>(
                name: "MembershipId",
                table: "Owners",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
