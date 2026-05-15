using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataFut.Migrations
{
    /// <inheritdoc />
    public partial class AddColunasClubeAtualEPosicao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClubeJogador");

            migrationBuilder.DropTable(
                name: "JogadorPosicao");

            migrationBuilder.RenameColumn(
                name: "ClubeId",
                table: "Jogadores",
                newName: "PosicaoId");

            migrationBuilder.AddColumn<int>(
                name: "ClubeAtualId",
                table: "Jogadores",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_Jogadores_ClubeAtualId",
                table: "Jogadores",
                column: "ClubeAtualId");

            migrationBuilder.CreateIndex(
                name: "IX_Jogadores_PosicaoId",
                table: "Jogadores",
                column: "PosicaoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Jogadores_Clubes_ClubeAtualId",
                table: "Jogadores",
                column: "ClubeAtualId",
                principalTable: "Clubes",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Jogadores_Posicoes_PosicaoId",
                table: "Jogadores",
                column: "PosicaoId",
                principalTable: "Posicoes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jogadores_Clubes_ClubeAtualId",
                table: "Jogadores");

            migrationBuilder.DropForeignKey(
                name: "FK_Jogadores_Posicoes_PosicaoId",
                table: "Jogadores");

            migrationBuilder.DropIndex(
                name: "IX_Jogadores_ClubeAtualId",
                table: "Jogadores");

            migrationBuilder.DropIndex(
                name: "IX_Jogadores_PosicaoId",
                table: "Jogadores");

            migrationBuilder.DropColumn(
                name: "ClubeAtualId",
                table: "Jogadores");

            migrationBuilder.RenameColumn(
                name: "PosicaoId",
                table: "Jogadores",
                newName: "ClubeId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.CreateTable(
                name: "ClubeJogador",
                columns: table => new
                {
                    ClubesId = table.Column<int>(type: "int", nullable: false),
                    JogadoresId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClubeJogador", x => new { x.ClubesId, x.JogadoresId });
                    table.ForeignKey(
                        name: "FK_ClubeJogador_Clubes_ClubesId",
                        column: x => x.ClubesId,
                        principalTable: "Clubes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClubeJogador_Jogadores_JogadoresId",
                        column: x => x.JogadoresId,
                        principalTable: "Jogadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JogadorPosicao",
                columns: table => new
                {
                    JogadoresId = table.Column<int>(type: "int", nullable: false),
                    PosicoesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JogadorPosicao", x => new { x.JogadoresId, x.PosicoesId });
                    table.ForeignKey(
                        name: "FK_JogadorPosicao_Jogadores_JogadoresId",
                        column: x => x.JogadoresId,
                        principalTable: "Jogadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JogadorPosicao_Posicoes_PosicoesId",
                        column: x => x.PosicoesId,
                        principalTable: "Posicoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClubeJogador_JogadoresId",
                table: "ClubeJogador",
                column: "JogadoresId");

            migrationBuilder.CreateIndex(
                name: "IX_JogadorPosicao_PosicoesId",
                table: "JogadorPosicao",
                column: "PosicoesId");
        }
    }
}
