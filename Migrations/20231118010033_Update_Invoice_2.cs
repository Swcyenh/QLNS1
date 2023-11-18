using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QLNS1.Migrations
{
    public partial class Update_Invoice_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "No",
                table: "Invoice",
                newName: "Debt");

            migrationBuilder.AddColumn<string>(
                name: "email",
                table: "Invoice",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "sdt",
                table: "Invoice",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "email",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "sdt",
                table: "Invoice");

            migrationBuilder.RenameColumn(
                name: "Debt",
                table: "Invoice",
                newName: "No");
        }
    }
}
