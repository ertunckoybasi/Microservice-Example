using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Customer.Persistence.Migrations
{
    public partial class firstdb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ErpCustomer",
                columns: table => new
                {
                    RecId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CurrentAccountCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentAccountName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentAccountEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentAccountPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentAccountAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InUse = table.Column<bool>(type: "bit", nullable: false),
                    InsertedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InsertedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ErpCustomer", x => x.RecId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ErpCustomer");
        }
    }
}
