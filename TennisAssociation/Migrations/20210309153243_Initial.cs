using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TennisAssociation.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "matches",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newsequentialid())"),
                    tournament = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    firstPlayer = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    secondPlayer = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    headToHeadFirst = table.Column<short>(type: "smallint", nullable: true),
                    headToHeadSecond = table.Column<short>(type: "smallint", nullable: true),
                    resultFirst = table.Column<short>(type: "smallint", nullable: true),
                    resultSecond = table.Column<short>(type: "smallint", nullable: true),
                    date = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_matches", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "players",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newsequentialid())"),
                    firstName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    lastName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    country = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    height = table.Column<short>(type: "smallint", nullable: true),
                    weight = table.Column<short>(type: "smallint", nullable: true),
                    birth = table.Column<DateTime>(type: "date", nullable: true),
                    currentRankingSingle = table.Column<short>(type: "smallint", nullable: true),
                    bestRankingSingle = table.Column<short>(type: "smallint", nullable: true),
                    currentRankingDouble = table.Column<short>(type: "smallint", nullable: true),
                    bestRankingDouble = table.Column<short>(type: "smallint", nullable: true),
                    sex = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    hand = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    img = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_players", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "matches");

            migrationBuilder.DropTable(
                name: "players");
        }
    }
}
