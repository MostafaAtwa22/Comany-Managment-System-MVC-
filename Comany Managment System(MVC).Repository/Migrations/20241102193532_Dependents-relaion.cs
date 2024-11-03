using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Comany_Managment_System_MVC_.Repository.Migrations
{
    /// <inheritdoc />
    public partial class Dependentsrelaion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Relation",
                table: "Dependents",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Relation",
                table: "Dependents");
        }
    }
}
