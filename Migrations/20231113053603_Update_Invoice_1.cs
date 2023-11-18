using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QLNS1.Migrations
{
    public partial class Update_Invoice_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "No",
                table: "Invoice",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "No",
                table: "Invoice");
        }
    }
}
