using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SharkKnives.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Facas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Modelo = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Material = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Camadas = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Pegada = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Cabo = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Dimensoes = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Referencia = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    FotoUrl1 = table.Column<string>(type: "text", nullable: true),
                    FotoUrl2 = table.Column<string>(type: "text", nullable: true),
                    FotoUrl3 = table.Column<string>(type: "text", nullable: true),
                    Ativo = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Facas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    SenhaHash = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Nome = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Facas_Ativo",
                table: "Facas",
                column: "Ativo");

            migrationBuilder.CreateIndex(
                name: "IX_Facas_CreatedAt",
                table: "Facas",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Email",
                table: "Usuarios",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Facas");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
