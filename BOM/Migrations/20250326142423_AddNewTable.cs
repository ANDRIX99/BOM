using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BOM.Migrations
{
    /// <inheritdoc />
    public partial class AddNewTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VersioneDistintaBase",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdProduct = table.Column<int>(type: "int", nullable: false),
                    Version = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VersioneDistintaBase", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VersioneDistintaBase_Item_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Item",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DistintaBase",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdVersion = table.Column<int>(type: "int", nullable: false),
                    IdSon = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<float>(type: "real", nullable: false),
                    VersioneDistintaBaseId = table.Column<int>(type: "int", nullable: false),
                    FiglioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DistintaBase", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DistintaBase_Item_FiglioId",
                        column: x => x.FiglioId,
                        principalTable: "Item",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DistintaBase_VersioneDistintaBase_VersioneDistintaBaseId",
                        column: x => x.VersioneDistintaBaseId,
                        principalTable: "VersioneDistintaBase",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DistintaBase_FiglioId",
                table: "DistintaBase",
                column: "FiglioId");

            migrationBuilder.CreateIndex(
                name: "IX_DistintaBase_VersioneDistintaBaseId",
                table: "DistintaBase",
                column: "VersioneDistintaBaseId");

            migrationBuilder.CreateIndex(
                name: "IX_VersioneDistintaBase_ProductId",
                table: "VersioneDistintaBase",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DistintaBase");

            migrationBuilder.DropTable(
                name: "VersioneDistintaBase");
        }
    }
}
