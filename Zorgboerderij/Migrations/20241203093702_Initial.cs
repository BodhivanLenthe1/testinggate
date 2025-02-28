using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zorgboerderij.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "userAccounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrgId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Voornaam = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Achternaam = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Gebruikersnaam = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Wachtwoord = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userAccounts", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_userAccounts_Email",
                table: "userAccounts",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_userAccounts_Gebruikersnaam",
                table: "userAccounts",
                column: "Gebruikersnaam",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "userAccounts");
        }
    }
}
