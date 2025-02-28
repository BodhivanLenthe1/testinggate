using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zorgboerderij.Migrations
{
    /// <inheritdoc />
    public partial class Afmeldingen : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "afmeldingen",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Client = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tandarts = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Vakantie = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Verjaardag = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ziek = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Vrij = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Dokter = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OpKarwei = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_afmeldingen", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "afmeldingen");
        }
    }
}
