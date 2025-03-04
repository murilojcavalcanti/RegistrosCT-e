using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RegistrosCTe.Infra.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class migration1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Carga",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantidade = table.Column<int>(type: "int", nullable: false),
                    Peso = table.Column<decimal>(type: "decimal(10,3)", nullable: false),
                    Volume = table.Column<decimal>(type: "decimal(10,3)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carga", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Viagems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Origem = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Destino = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Distancia = table.Column<decimal>(type: "decimal(10,3)", nullable: false),
                    DataInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ValorFrete = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    CargaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Viagems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Viagems_Carga_CargaId",
                        column: x => x.CargaId,
                        principalTable: "Carga",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CTe",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ValorCTe = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    ValorICMS = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Aliquota = table.Column<decimal>(type: "decimal(10,3)", nullable: false),
                    DataEmissao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ViagemId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CTe", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CTe_Viagems_ViagemId",
                        column: x => x.ViagemId,
                        principalTable: "Viagems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DespesasAdicionais",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    ViagemId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DespesasAdicionais", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DespesasAdicionais_Viagems_ViagemId",
                        column: x => x.ViagemId,
                        principalTable: "Viagems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CTe_ViagemId",
                table: "CTe",
                column: "ViagemId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DespesasAdicionais_ViagemId",
                table: "DespesasAdicionais",
                column: "ViagemId");

            migrationBuilder.CreateIndex(
                name: "IX_Viagems_CargaId",
                table: "Viagems",
                column: "CargaId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CTe");

            migrationBuilder.DropTable(
                name: "DespesasAdicionais");

            migrationBuilder.DropTable(
                name: "Viagems");

            migrationBuilder.DropTable(
                name: "Carga");
        }
    }
}
