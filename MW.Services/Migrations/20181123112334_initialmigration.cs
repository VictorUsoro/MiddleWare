using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MW.Services.Migrations
{
    public partial class initialmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Agents",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    RecordStatus = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    OtherNames = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    Gender = table.Column<int>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    ProviderAPI = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    RecordStatus = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    OtherNames = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    Gender = table.Column<int>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    PhoneNumber2 = table.Column<string>(nullable: true),
                    ShopLine = table.Column<string>(nullable: true),
                    ShopNumber = table.Column<string>(nullable: true),
                    ShopBlock = table.Column<string>(nullable: true),
                    MeterId = table.Column<string>(nullable: true),
                    PictureURL = table.Column<string>(nullable: true),
                    Tier = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Agents");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
