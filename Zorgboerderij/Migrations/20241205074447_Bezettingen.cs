using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zorgboerderij.Migrations
{
    /// <inheritdoc />
    public partial class Bezettingen : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bezettingen",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Client = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Maandag = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Dinsdag = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Woensdag = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Donderdag = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Vrijdag = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Zaterdag = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Totaal = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bezettingen", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bezettingen");
        }
    }
}
