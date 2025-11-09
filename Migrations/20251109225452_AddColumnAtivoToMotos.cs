using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mottu.Uwb.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnAtivoToMotos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                schema: "public",
                table: "Motos",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ativo",
                schema: "public",
                table: "Motos");
        }
    }
}
