using Microsoft.EntityFrameworkCore.Migrations;

namespace Employee_Stack.Migrations
{
    public partial class CreatedRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "TeckStack",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TeckStack_EmployeeId",
                table: "TeckStack",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_TeckStack_Employees_EmployeeId",
                table: "TeckStack",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeckStack_Employees_EmployeeId",
                table: "TeckStack");

            migrationBuilder.DropIndex(
                name: "IX_TeckStack_EmployeeId",
                table: "TeckStack");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "TeckStack");
        }
    }
}
