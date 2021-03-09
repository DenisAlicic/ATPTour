using Microsoft.EntityFrameworkCore.Migrations;

namespace TennisAssociation.Migrations
{
    public partial class RemoveCountryForFanDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Country",
                table: "Fans");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}
