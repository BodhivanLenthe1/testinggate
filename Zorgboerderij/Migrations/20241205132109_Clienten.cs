using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zorgboerderij.Migrations
{
    /// <inheritdoc />
    public partial class Clienten : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "clienten",
                columns: table => new
                {
                    persid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Voornaam = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Achternaam = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FotoFile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Maandag = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    Dinsdag = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    Woensdag = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    Donderdag = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    Vrijdag = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    Zaterdag = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    Groepskleur = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_clienten", x => x.persid);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "clienten");
        }
    }
}
