using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TennisAssociation.Migrations
{
    public partial class ChangedPrimaryKeyOffan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Fans",
                table: "Fans");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Fans");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Fans",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Fans",
                table: "Fans",
                column: "Username");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Fans",
                table: "Fans");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Fans",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "Fans",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Fans",
                table: "Fans",
                column: "Id");
        }
    }
}
