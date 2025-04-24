using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NovaExpediente.Persistence.Migrations
{
    public partial class AgregarClasificador : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clasificadores",
                columns: table => new
                {
                    IdClasificador = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreadoPor = table.Column<int>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    ModificadoPor = table.Column<int>(nullable: true),
                    FechaUltimaModificacion = table.Column<DateTime>(nullable: true),
                    Nombre = table.Column<string>(maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(type: "ntext", nullable: true),
                    Orden = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clasificadores", x => x.IdClasificador);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clasificadores");
        }
    }
}
