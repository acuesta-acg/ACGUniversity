using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Acg.University.DAL.SqlServer.Migrations
{
    public partial class usuario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RolUsuario_UsuariosDeUni_UsuariosId",
                table: "RolUsuario");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsuariosDeUni",
                table: "UsuariosDeUni");

            migrationBuilder.RenameTable(
                name: "UsuariosDeUni",
                newName: "Usuarios");

            migrationBuilder.AlterColumn<string>(
                name: "Login",
                table: "Usuarios",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Usuarios",
                table: "Usuarios",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Login",
                table: "Usuarios",
                column: "Login",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RolUsuario_Usuarios_UsuariosId",
                table: "RolUsuario",
                column: "UsuariosId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RolUsuario_Usuarios_UsuariosId",
                table: "RolUsuario");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Usuarios",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Usuarios_Login",
                table: "Usuarios");

            migrationBuilder.RenameTable(
                name: "Usuarios",
                newName: "UsuariosDeUni");

            migrationBuilder.AlterColumn<string>(
                name: "Login",
                table: "UsuariosDeUni",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsuariosDeUni",
                table: "UsuariosDeUni",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RolUsuario_UsuariosDeUni_UsuariosId",
                table: "RolUsuario",
                column: "UsuariosId",
                principalTable: "UsuariosDeUni",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
