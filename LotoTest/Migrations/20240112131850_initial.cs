using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LotoTest.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LotteryCombinations",
                columns: table => new
                {
                    CombinationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DrawDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Numbers = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LotteryCombinations", x => x.CombinationId);
                });;
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LotteryCombinations");
        }
    }
}
