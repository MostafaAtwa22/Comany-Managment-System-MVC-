using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Comany_Managment_System_MVC_.Repository.Migrations
{
    /// <inheritdoc />
    public partial class updatekeydependent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Dependents",
                table: "Dependents");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Dependents",
                table: "Dependents",
                columns: new[] { "Id", "EmployeeId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Dependents",
                table: "Dependents");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Dependents",
                table: "Dependents",
                columns: new[] { "Name", "EmployeeId" });
        }
    }
}
