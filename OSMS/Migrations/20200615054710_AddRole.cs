using Microsoft.EntityFrameworkCore.Migrations;

namespace OSMS.Migrations
{
    public partial class AddRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RoleID",
                table: "Students",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RoleID",
                table: "Instructors",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    RoleID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.RoleID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Students_RoleID",
                table: "Students",
                column: "RoleID");

            migrationBuilder.CreateIndex(
                name: "IX_Instructors_RoleID",
                table: "Instructors",
                column: "RoleID");

            migrationBuilder.AddForeignKey(
                name: "FK_Instructors_Role_RoleID",
                table: "Instructors",
                column: "RoleID",
                principalTable: "Role",
                principalColumn: "RoleID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Role_RoleID",
                table: "Students",
                column: "RoleID",
                principalTable: "Role",
                principalColumn: "RoleID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Instructors_Role_RoleID",
                table: "Instructors");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Role_RoleID",
                table: "Students");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropIndex(
                name: "IX_Students_RoleID",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Instructors_RoleID",
                table: "Instructors");

            migrationBuilder.DropColumn(
                name: "RoleID",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "RoleID",
                table: "Instructors");
        }
    }
}
