using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ForEvidence.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Types",
                columns: table => new
                {
                    TypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TName = table.Column<string>(type: "varchar(55)", maxLength: 55, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Types", x => x.TypeID);
                });

            migrationBuilder.CreateTable(
                name: "StoreMasters",
                columns: table => new
                {
                    StoreID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StoreName = table.Column<string>(type: "varchar(55)", maxLength: 55, nullable: false),
                    SellDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StoreType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalDue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TypeID = table.Column<int>(type: "int", nullable: false),
                    ImageName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BTypeTypeID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreMasters", x => x.StoreID);
                    table.ForeignKey(
                        name: "FK_StoreMasters_Types_BTypeTypeID",
                        column: x => x.BTypeTypeID,
                        principalTable: "Types",
                        principalColumn: "TypeID");
                });

            migrationBuilder.CreateTable(
                name: "BookDetails",
                columns: table => new
                {
                    BookID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StoreID = table.Column<int>(type: "int", nullable: true),
                    TypeID = table.Column<int>(type: "int", nullable: false),
                    StoreMasterStoreID = table.Column<int>(type: "int", nullable: true),
                    BTypeTypeID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookDetails", x => x.BookID);
                    table.ForeignKey(
                        name: "FK_BookDetails_StoreMasters_StoreMasterStoreID",
                        column: x => x.StoreMasterStoreID,
                        principalTable: "StoreMasters",
                        principalColumn: "StoreID");
                    table.ForeignKey(
                        name: "FK_BookDetails_Types_BTypeTypeID",
                        column: x => x.BTypeTypeID,
                        principalTable: "Types",
                        principalColumn: "TypeID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookDetails_BTypeTypeID",
                table: "BookDetails",
                column: "BTypeTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_BookDetails_StoreMasterStoreID",
                table: "BookDetails",
                column: "StoreMasterStoreID");

            migrationBuilder.CreateIndex(
                name: "IX_StoreMasters_BTypeTypeID",
                table: "StoreMasters",
                column: "BTypeTypeID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookDetails");

            migrationBuilder.DropTable(
                name: "StoreMasters");

            migrationBuilder.DropTable(
                name: "Types");
        }
    }
}
