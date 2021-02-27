using Microsoft.EntityFrameworkCore.Migrations;

namespace MedicinApi.Migrations
{
    public partial class AddRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_EspecialidadeMedicos_EspecialidadeId",
                table: "EspecialidadeMedicos",
                column: "EspecialidadeId");

            migrationBuilder.AddForeignKey(
                name: "FK_EspecialidadeMedicos_Especialidades_EspecialidadeId",
                table: "EspecialidadeMedicos",
                column: "EspecialidadeId",
                principalTable: "Especialidades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EspecialidadeMedicos_Especialidades_EspecialidadeId",
                table: "EspecialidadeMedicos");

            migrationBuilder.DropIndex(
                name: "IX_EspecialidadeMedicos_EspecialidadeId",
                table: "EspecialidadeMedicos");
        }
    }
}
