using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vin_de_la_france.Migrations
{
    /// <inheritdoc />
    public partial class freshMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "Vins");

            //migrationBuilder.CreateTable(
            //    name: "ArticlesClass",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        UnitPrice = table.Column<int>(type: "int", nullable: false),
            //        QuantityStock = table.Column<int>(type: "int", nullable: false),
            //        MinimumThreshold = table.Column<int>(type: "int", nullable: false),
            //        Reference = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //        FamillesClassId = table.Column<int>(type: "int", nullable: false),
            //        FournisseursClassId = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_ArticlesClass", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_ArticlesClass_Familles_FamillesClassId",
            //            column: x => x.FamillesClassId,
            //            principalTable: "Familles",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_ArticlesClass_Fournisseurs_FournisseursClassId",
            //            column: x => x.FournisseursClassId,
            //            principalTable: "Fournisseurs",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "ClientsClass",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_ClientsClass", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "LigneCommandeFournisseursClass",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Quantite = table.Column<int>(type: "int", nullable: false),
            //        PrixUnitaire = table.Column<int>(type: "int", nullable: false),
            //        CommandeFournisseursClassId = table.Column<int>(type: "int", nullable: false),
            //        ArticlesClassId = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_LigneCommandeFournisseursClass", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_LigneCommandeFournisseursClass_ArticlesClass_ArticlesClassId",
            //            column: x => x.ArticlesClassId,
            //            principalTable: "ArticlesClass",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "CommandeClientsClass",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Date = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
            //        Statut = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        ClientsClassId = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_CommandeClientsClass", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_CommandeClientsClass_ClientsClass_ClientsClassId",
            //            column: x => x.ClientsClassId,
            //            principalTable: "ClientsClass",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "LigneCommandeClientsClass",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Quantite = table.Column<int>(type: "int", nullable: false),
            //        PrixUnitaire = table.Column<int>(type: "int", nullable: false),
            //        CommandeClientsClassId = table.Column<int>(type: "int", nullable: false),
            //        ArticlesClassId = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_LigneCommandeClientsClass", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_LigneCommandeClientsClass_ArticlesClass_ArticlesClassId",
            //            column: x => x.ArticlesClassId,
            //            principalTable: "ArticlesClass",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_LigneCommandeClientsClass_CommandeClientsClass_CommandeClientsClassId",
            //            column: x => x.CommandeClientsClassId,
            //            principalTable: "CommandeClientsClass",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateIndex(
            //    name: "IX_ArticlesClass_FamillesClassId",
            //    table: "ArticlesClass",
            //    column: "FamillesClassId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_ArticlesClass_FournisseursClassId",
            //    table: "ArticlesClass",
            //    column: "FournisseursClassId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_CommandeClientsClass_ClientsClassId",
            //    table: "CommandeClientsClass",
            //    column: "ClientsClassId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_LigneCommandeClientsClass_ArticlesClassId",
            //    table: "LigneCommandeClientsClass",
            //    column: "ArticlesClassId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_LigneCommandeClientsClass_CommandeClientsClassId",
            //    table: "LigneCommandeClientsClass",
            //    column: "CommandeClientsClassId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_LigneCommandeFournisseursClass_ArticlesClassId",
            //    table: "LigneCommandeFournisseursClass",
            //    column: "ArticlesClassId");
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
                name: "ClientsClass");

            migrationBuilder.CreateTable(
                name: "Vins",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FamillesClassId = table.Column<int>(type: "int", nullable: false),
                    FournisseursClassId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuantityStock = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vins", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vins_Familles_FamillesClassId",
                        column: x => x.FamillesClassId,
                        principalTable: "Familles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Vins_Fournisseurs_FournisseursClassId",
                        column: x => x.FournisseursClassId,
                        principalTable: "Fournisseurs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vins_FamillesClassId",
                table: "Vins",
                column: "FamillesClassId");

            migrationBuilder.CreateIndex(
                name: "IX_Vins_FournisseursClassId",
                table: "Vins",
                column: "FournisseursClassId");
        }
    }
}
