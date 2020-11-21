using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MedicinApi.Migrations
{
    public partial class Add_Authentication : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Especialidades_Medicos_MedicoId",
                table: "Especialidades");

            migrationBuilder.AlterColumn<Guid>(
                name: "MedicoId",
                table: "Especialidades",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Username = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    PasswordHash = table.Column<byte[]>(nullable: true),
                    PasswordSalt = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Especialidades_Medicos_MedicoId",
                table: "Especialidades",
                column: "MedicoId",
                principalTable: "Medicos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Especialidades_Medicos_MedicoId",
                table: "Especialidades");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.AlterColumn<Guid>(
                name: "MedicoId",
                table: "Especialidades",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddForeignKey(
                name: "FK_Especialidades_Medicos_MedicoId",
                table: "Especialidades",
                column: "MedicoId",
                principalTable: "Medicos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
