using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MedicinApi.Migrations
{
    public partial class AdicionandoEspecialidadeMedico : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Especialidades_Medicos_MedicoId",
                table: "Especialidades");

            migrationBuilder.DropIndex(
                name: "IX_Especialidades_MedicoId",
                table: "Especialidades");

            migrationBuilder.DropColumn(
                name: "MedicoId",
                table: "Especialidades");

            migrationBuilder.CreateTable(
                name: "EspecialidadeMedicos",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    MedicoId = table.Column<Guid>(nullable: false),
                    EspecialidadeId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EspecialidadeMedicos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EspecialidadeMedicos_Medicos_MedicoId",
                        column: x => x.MedicoId,
                        principalTable: "Medicos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EspecialidadeMedicos_MedicoId",
                table: "EspecialidadeMedicos",
                column: "MedicoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EspecialidadeMedicos");

            migrationBuilder.AddColumn<Guid>(
                name: "MedicoId",
                table: "Especialidades",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Especialidades_MedicoId",
                table: "Especialidades",
                column: "MedicoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Especialidades_Medicos_MedicoId",
                table: "Especialidades",
                column: "MedicoId",
                principalTable: "Medicos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
