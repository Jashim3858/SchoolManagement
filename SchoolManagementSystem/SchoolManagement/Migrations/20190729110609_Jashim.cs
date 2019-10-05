using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SchoolManagement.Migrations
{
    public partial class Jashim : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClassInformation",
                columns: table => new
                {
                    ClassID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClassName = table.Column<string>(nullable: true),
                    NoOfStudent = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassInformation", x => x.ClassID);
                });

            migrationBuilder.CreateTable(
                name: "RegisterViewModel",
                columns: table => new
                {
                    RegisterViewModelID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(nullable: false),
                    Password = table.Column<string>(maxLength: 100, nullable: false),
                    ConfirmPassword = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegisterViewModel", x => x.RegisterViewModelID);
                });

            migrationBuilder.CreateTable(
                name: "StudentInformation",
                columns: table => new
                {
                    StudentID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StudentName = table.Column<string>(nullable: true),
                    FathersName = table.Column<string>(nullable: true),
                    MothersName = table.Column<string>(nullable: true),
                    DOB = table.Column<DateTime>(nullable: false),
                    Religion = table.Column<string>(nullable: true),
                    GurdiansCellPhone = table.Column<string>(nullable: true),
                    StudentAddress = table.Column<string>(nullable: true),
                    ClassID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentInformation", x => x.StudentID);
                    table.ForeignKey(
                        name: "FK_StudentInformation_ClassInformation_ClassID",
                        column: x => x.ClassID,
                        principalTable: "ClassInformation",
                        principalColumn: "ClassID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeacherInformation",
                columns: table => new
                {
                    TeacherID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TeacherName = table.Column<string>(nullable: true),
                    DOB = table.Column<DateTime>(nullable: false),
                    TeachersCellPhone = table.Column<string>(nullable: true),
                    Religion = table.Column<string>(nullable: true),
                    TeacherAddress = table.Column<string>(nullable: true),
                    ImagePath = table.Column<string>(nullable: true),
                    ClassID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherInformation", x => x.TeacherID);
                    table.ForeignKey(
                        name: "FK_TeacherInformation_ClassInformation_ClassID",
                        column: x => x.ClassID,
                        principalTable: "ClassInformation",
                        principalColumn: "ClassID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentInformation_ClassID",
                table: "StudentInformation",
                column: "ClassID");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherInformation_ClassID",
                table: "TeacherInformation",
                column: "ClassID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RegisterViewModel");

            migrationBuilder.DropTable(
                name: "StudentInformation");

            migrationBuilder.DropTable(
                name: "TeacherInformation");

            migrationBuilder.DropTable(
                name: "ClassInformation");
        }
    }
}
