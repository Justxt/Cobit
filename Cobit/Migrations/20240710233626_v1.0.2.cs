using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cobit.Migrations
{
    /// <inheritdoc />
    public partial class v102 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AGModel",
                table: "AGModel");

            migrationBuilder.RenameTable(
                name: "AGModel",
                newName: "AGModels");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AGModels",
                table: "AGModels",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AGModels",
                table: "AGModels");

            migrationBuilder.RenameTable(
                name: "AGModels",
                newName: "AGModel");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AGModel",
                table: "AGModel",
                column: "Id");
        }
    }
}
