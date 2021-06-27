using Microsoft.EntityFrameworkCore.Migrations;

namespace Employee_Stack.Migrations
{
    public partial class FixedManyToMany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "EmployeeTechStack",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    TeckStackId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeTechStack", x => new { x.EmployeeId, x.TeckStackId });
                    table.ForeignKey(
                        name: "FK_EmployeeTechStack_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeTechStack_TeckStack_TeckStackId",
                        column: x => x.TeckStackId,
                        principalTable: "TeckStack",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeTechStack_TeckStackId",
                table: "EmployeeTechStack",
                column: "TeckStackId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeTechStack");

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
    }
}
