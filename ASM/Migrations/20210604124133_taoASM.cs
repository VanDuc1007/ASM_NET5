using Microsoft.EntityFrameworkCore.Migrations;

namespace ASM.Migrations
{
    public partial class taoASM : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MonAns",
                columns: table => new
                {
                    MonAnID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 250, nullable: false),
                    Gia = table.Column<double>(nullable: false),
                    Phanloai = table.Column<int>(nullable: false),
                    Hinh = table.Column<string>(maxLength: 250, nullable: true),
                    Mota = table.Column<string>(maxLength: 250, nullable: true),
                    Trangthai = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonAns", x => x.MonAnID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MonAns");
        }
    }
}
