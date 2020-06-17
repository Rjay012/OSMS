using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OSMS.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Standards",
                columns: table => new
                {
                    StandardID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StandardName = table.Column<string>(type: "varchar(100)", nullable: false),
                    Description = table.Column<string>(type: "varchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Standards", x => x.StandardID);
                });

            migrationBuilder.CreateTable(
                name: "Instructors",
                columns: table => new
                {
                    InstructorID = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    InstructorName = table.Column<string>(type: "varchar(100)", nullable: false),
                    StandardID = table.Column<int>(nullable: false),
                    RowVersion = table.Column<byte[]>(type: "timestamp", nullable: true),
                    Password = table.Column<string>(type: "varchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instructors", x => x.InstructorID);
                    table.ForeignKey(
                        name: "FK_Instructors_Standards_StandardID",
                        column: x => x.StandardID,
                        principalTable: "Standards",
                        principalColumn: "StandardID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    StudentID = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    StudentName = table.Column<string>(type: "varchar(100)", nullable: false),
                    StandardID = table.Column<int>(nullable: false),
                    RowVersion = table.Column<byte[]>(type: "timestamp", nullable: true),
                    Password = table.Column<string>(type: "varchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.StudentID);
                    table.ForeignKey(
                        name: "FK_Students_Standards_StandardID",
                        column: x => x.StandardID,
                        principalTable: "Standards",
                        principalColumn: "StandardID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Instructors_StandardID",
                table: "Instructors",
                column: "StandardID");

            migrationBuilder.CreateIndex(
                name: "IX_Students_StandardID",
                table: "Students",
                column: "StandardID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Instructors");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Standards");
        }
    }
}
