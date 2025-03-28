using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BOM.Migrations
{
    /// <inheritdoc />
    public partial class EditChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdSon",
                table: "DistintaBase");

            migrationBuilder.DropColumn(
                name: "IdVersion",
                table: "DistintaBase");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdSon",
                table: "DistintaBase",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdVersion",
                table: "DistintaBase",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
