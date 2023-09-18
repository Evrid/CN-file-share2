using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentFileShare6.Migrations
{
    /// <inheritdoc />
    public partial class modifieddatatype : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Report_Document_DocumentID1",
                table: "Report");

            migrationBuilder.DropIndex(
                name: "IX_Report_DocumentID1",
                table: "Report");

            migrationBuilder.DropColumn(
                name: "DocumentID1",
                table: "Report");

            migrationBuilder.AlterColumn<string>(
                name: "UserID",
                table: "Report",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "DocumentID",
                table: "Report",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Report_DocumentID",
                table: "Report",
                column: "DocumentID");

            migrationBuilder.AddForeignKey(
                name: "FK_Report_Document_DocumentID",
                table: "Report",
                column: "DocumentID",
                principalTable: "Document",
                principalColumn: "DocumentID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Report_Document_DocumentID",
                table: "Report");

            migrationBuilder.DropIndex(
                name: "IX_Report_DocumentID",
                table: "Report");

            migrationBuilder.AlterColumn<int>(
                name: "UserID",
                table: "Report",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "DocumentID",
                table: "Report",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "DocumentID1",
                table: "Report",
                type: "varchar(255)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Report_DocumentID1",
                table: "Report",
                column: "DocumentID1");

            migrationBuilder.AddForeignKey(
                name: "FK_Report_Document_DocumentID1",
                table: "Report",
                column: "DocumentID1",
                principalTable: "Document",
                principalColumn: "DocumentID");
        }
    }
}
