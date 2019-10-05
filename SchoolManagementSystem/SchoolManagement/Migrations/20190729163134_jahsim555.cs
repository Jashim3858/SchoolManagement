using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SchoolManagement.Migrations
{
    public partial class jahsim555 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeacherInformation_ClassInformation_ClassID",
                table: "TeacherInformation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TeacherInformation",
                table: "TeacherInformation");

            migrationBuilder.RenameTable(
                name: "TeacherInformation",
                newName: "TeacherInformation_1");

            migrationBuilder.RenameIndex(
                name: "IX_TeacherInformation_ClassID",
                table: "TeacherInformation_1",
                newName: "IX_TeacherInformation_1_ClassID");

            migrationBuilder.AlterColumn<string>(
                name: "DOB",
                table: "StudentInformation",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeacherInformation_1",
                table: "TeacherInformation_1",
                column: "TeacherID");

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherInformation_1_ClassInformation_ClassID",
                table: "TeacherInformation_1",
                column: "ClassID",
                principalTable: "ClassInformation",
                principalColumn: "ClassID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeacherInformation_1_ClassInformation_ClassID",
                table: "TeacherInformation_1");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TeacherInformation_1",
                table: "TeacherInformation_1");

            migrationBuilder.RenameTable(
                name: "TeacherInformation_1",
                newName: "TeacherInformation");

            migrationBuilder.RenameIndex(
                name: "IX_TeacherInformation_1_ClassID",
                table: "TeacherInformation",
                newName: "IX_TeacherInformation_ClassID");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DOB",
                table: "StudentInformation",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeacherInformation",
                table: "TeacherInformation",
                column: "TeacherID");

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherInformation_ClassInformation_ClassID",
                table: "TeacherInformation",
                column: "ClassID",
                principalTable: "ClassInformation",
                principalColumn: "ClassID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
