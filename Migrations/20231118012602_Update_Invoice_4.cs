using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QLNS1.Migrations
{
    public partial class Update_Invoice_4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "email",
                table: "Invoice");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "email",
                table: "Invoice",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
