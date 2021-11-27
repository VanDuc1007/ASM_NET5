using Microsoft.EntityFrameworkCore.Migrations;

namespace ASM.Migrations
{
    public partial class asm4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Trangthai",
                table: "DonHangs",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "Trangthai",
                table: "DonHangs",
                type: "bit",
                nullable: false,
                oldClrType: typeof(int));
        }
    }
}
