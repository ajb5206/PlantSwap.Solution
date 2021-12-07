using Microsoft.EntityFrameworkCore.Migrations;

namespace PlantSwap.Migrations
{
    public partial class UpdateRequestClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImperfectMatch",
                table: "Requests");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ImperfectMatch",
                table: "Requests",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }
    }
}
