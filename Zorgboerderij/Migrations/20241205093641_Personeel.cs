   using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zorgboerderij.Migrations
{
    /// <inheritdoc />
    public partial class Personeel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "personeel",
                columns: table => new
                {
                    persid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Voornaam = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Achternaam = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FotoFile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Maandag = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Dinsdag = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Woensdag = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Donderdag = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Vrijdag = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Zaterdag = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Groepskleur = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_personeel", x => x.persid);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "personeel");
        }
    }
}
