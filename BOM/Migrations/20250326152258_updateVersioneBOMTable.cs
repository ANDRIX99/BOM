using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BOM.Migrations
{
    /// <inheritdoc />
    public partial class updateVersioneBOMTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdProduct",
                table: "VersioneDistintaBase");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdProduct",
                table: "VersioneDistintaBase",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
