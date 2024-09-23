using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace VinWpf.Migrations
{
    /// <inheritdoc />
    public partial class db : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClientsClass",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: false),
                    Phone = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientsClass", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FamilleClass",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FamilleClass", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FournisseursClass",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: false),
                    Phone = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FournisseursClass", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ArticleClass",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    UnitPrice = table.Column<int>(type: "integer", nullable: false),
                    QuantityStock = table.Column<int>(type: "integer", nullable: false),
                    MinimumThreshold = table.Column<int>(type: "integer", nullable: false),
                    Reference = table.Column<Guid>(type: "uuid", nullable: false),
                    FamilleClassId = table.Column<int>(type: "integer", nullable: false),
                    FournisseursClassId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleClass", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArticleClass_FamilleClass_FamilleClassId",
                        column: x => x.FamilleClassId,
                        principalTable: "FamilleClass",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArticleClass_FournisseursClass_FournisseursClassId",
                        column: x => x.FournisseursClassId,
                        principalTable: "FournisseursClass",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CommandeClientsClass",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Quantite = table.Column<int>(type: "integer", nullable: false),
                    PrixCommande = table.Column<int>(type: "integer", nullable: false),
                    ArticleClassId = table.Column<int>(type: "integer", nullable: false),
                    ClientsClassId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommandeClientsClass", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommandeClientsClass_ArticleClass_ArticleClassId",
                        column: x => x.ArticleClassId,
                        principalTable: "ArticleClass",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CommandeClientsClass_ClientsClass_ClientsClassId",
                        column: x => x.ClientsClassId,
                        principalTable: "ClientsClass",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CommandeFournisseurClass",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Quantite = table.Column<int>(type: "integer", nullable: false),
                    ArticleClassId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommandeFournisseurClass", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommandeFournisseurClass_ArticleClass_ArticleClassId",
                        column: x => x.ArticleClassId,
                        principalTable: "ArticleClass",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArticleClass_FamilleClassId",
                table: "ArticleClass",
                column: "FamilleClassId");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleClass_FournisseursClassId",
                table: "ArticleClass",
                column: "FournisseursClassId");

            migrationBuilder.CreateIndex(
                name: "IX_CommandeClientsClass_ArticleClassId",
                table: "CommandeClientsClass",
                column: "ArticleClassId");

            migrationBuilder.CreateIndex(
                name: "IX_CommandeClientsClass_ClientsClassId",
                table: "CommandeClientsClass",
                column: "ClientsClassId");

            migrationBuilder.CreateIndex(
                name: "IX_CommandeFournisseurClass_ArticleClassId",
                table: "CommandeFournisseurClass",
                column: "ArticleClassId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommandeClientsClass");

            migrationBuilder.DropTable(
                name: "CommandeFournisseurClass");

            migrationBuilder.DropTable(
                name: "ClientsClass");

            migrationBuilder.DropTable(
                name: "ArticleClass");

            migrationBuilder.DropTable(
                name: "FamilleClass");

            migrationBuilder.DropTable(
                name: "FournisseursClass");
        }
    }
}
