using Microsoft.EntityFrameworkCore.Migrations;

namespace PlantSwap.Migrations
{
    public partial class UpdateOfferRequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HaveToOfferPlantCommonName",
                table: "Requests",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WillAcceptPlantCommonName",
                table: "Offers",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HaveToOfferPlantCommonName",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "WillAcceptPlantCommonName",
                table: "Offers");
        }
    }
}
