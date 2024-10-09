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
                name: "FamillesClass",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FamillesClass", x => x.Id);
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
                name: "CommandeClientsClass",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Statut = table.Column<string>(type: "text", nullable: false),
                    ClientsClassId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommandeClientsClass", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommandeClientsClass_ClientsClass_ClientsClassId",
                        column: x => x.ClientsClassId,
                        principalTable: "ClientsClass",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ArticlesClass",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    UnitPrice = table.Column<int>(type: "integer", nullable: false),
                    QuantityStock = table.Column<int>(type: "integer", nullable: false),
                    MinimumThreshold = table.Column<int>(type: "integer", nullable: false),
                    Reference = table.Column<Guid>(type: "uuid", nullable: false),
                    FamillesClassId = table.Column<int>(type: "integer", nullable: false),
                    FournisseursClassId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticlesClass", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArticlesClass_FamillesClass_FamillesClassId",
                        column: x => x.FamillesClassId,
                        principalTable: "FamillesClass",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArticlesClass_FournisseursClass_FournisseursClassId",
                        column: x => x.FournisseursClassId,
                        principalTable: "FournisseursClass",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CommandeFournisseursClass",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Statut = table.Column<string>(type: "text", nullable: false),
                    FournisseursClassId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommandeFournisseursClass", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommandeFournisseursClass_FournisseursClass_FournisseursCla~",
                        column: x => x.FournisseursClassId,
                        principalTable: "FournisseursClass",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LigneCommandeClientsClass",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Quantite = table.Column<int>(type: "integer", nullable: false),
                    PrixUnitaire = table.Column<int>(type: "integer", nullable: false),
                    CommandeClientsClassId = table.Column<int>(type: "integer", nullable: false),
                    ArticlesClassId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LigneCommandeClientsClass", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LigneCommandeClientsClass_ArticlesClass_ArticlesClassId",
                        column: x => x.ArticlesClassId,
                        principalTable: "ArticlesClass",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LigneCommandeClientsClass_CommandeClientsClass_CommandeClie~",
                        column: x => x.CommandeClientsClassId,
                        principalTable: "CommandeClientsClass",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LigneCommandeFournisseursClass",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Quantite = table.Column<int>(type: "integer", nullable: false),
                    PrixUnitaire = table.Column<int>(type: "integer", nullable: false),
                    CommandeFournisseursClassId = table.Column<int>(type: "integer", nullable: false),
                    ArticlesClassId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LigneCommandeFournisseursClass", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LigneCommandeFournisseursClass_ArticlesClass_ArticlesClassId",
                        column: x => x.ArticlesClassId,
                        principalTable: "ArticlesClass",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LigneCommandeFournisseursClass_CommandeFournisseursClass_Co~",
                        column: x => x.CommandeFournisseursClassId,
                        principalTable: "CommandeFournisseursClass",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArticlesClass_FamillesClassId",
                table: "ArticlesClass",
                column: "FamillesClassId");

            migrationBuilder.CreateIndex(
                name: "IX_ArticlesClass_FournisseursClassId",
                table: "ArticlesClass",
                column: "FournisseursClassId");

            migrationBuilder.CreateIndex(
                name: "IX_CommandeClientsClass_ClientsClassId",
                table: "CommandeClientsClass",
                column: "ClientsClassId");

            migrationBuilder.CreateIndex(
                name: "IX_CommandeFournisseursClass_FournisseursClassId",
                table: "CommandeFournisseursClass",
                column: "FournisseursClassId");

            migrationBuilder.CreateIndex(
                name: "IX_LigneCommandeClientsClass_ArticlesClassId",
                table: "LigneCommandeClientsClass",
                column: "ArticlesClassId");

            migrationBuilder.CreateIndex(
                name: "IX_LigneCommandeClientsClass_CommandeClientsClassId",
                table: "LigneCommandeClientsClass",
                column: "CommandeClientsClassId");

            migrationBuilder.CreateIndex(
                name: "IX_LigneCommandeFournisseursClass_ArticlesClassId",
                table: "LigneCommandeFournisseursClass",
                column: "ArticlesClassId");

            migrationBuilder.CreateIndex(
                name: "IX_LigneCommandeFournisseursClass_CommandeFournisseursClassId",
                table: "LigneCommandeFournisseursClass",
                column: "CommandeFournisseursClassId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LigneCommandeClientsClass");

            migrationBuilder.DropTable(
                name: "LigneCommandeFournisseursClass");

            migrationBuilder.DropTable(
                name: "CommandeClientsClass");

            migrationBuilder.DropTable(
                name: "ArticlesClass");

            migrationBuilder.DropTable(
                name: "CommandeFournisseursClass");

            migrationBuilder.DropTable(
                name: "ClientsClass");

            migrationBuilder.DropTable(
                name: "FamillesClass");

            migrationBuilder.DropTable(
                name: "FournisseursClass");
        }
    }
}
