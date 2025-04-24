using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NovaExpediente.Persistence.Migrations
{
    public partial class CreacionInicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_GradoAcademico",
                table: "GradoAcademico");

            migrationBuilder.RenameTable(
                name: "GradoAcademico",
                newName: "GradosAcademicos");

            migrationBuilder.AddColumn<int>(
                name: "CreadoPor",
                table: "GradosAcademicos",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                table: "GradosAcademicos",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaUltimaModificacion",
                table: "GradosAcademicos",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ModificadoPor",
                table: "GradosAcademicos",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_GradosAcademicos",
                table: "GradosAcademicos",
                column: "IdGradoAcademico");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_GradosAcademicos",
                table: "GradosAcademicos");

            migrationBuilder.DropColumn(
                name: "CreadoPor",
                table: "GradosAcademicos");

            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                table: "GradosAcademicos");

            migrationBuilder.DropColumn(
                name: "FechaUltimaModificacion",
                table: "GradosAcademicos");

            migrationBuilder.DropColumn(
                name: "ModificadoPor",
                table: "GradosAcademicos");

            migrationBuilder.RenameTable(
                name: "GradosAcademicos",
                newName: "GradoAcademico");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GradoAcademico",
                table: "GradoAcademico",
                column: "IdGradoAcademico");
        }
    }
}
