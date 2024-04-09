using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Intex_Group3_6.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ItemRecs",
                columns: table => new
                {
                    item = table.Column<string>(type: "TEXT", nullable: false),
                    rec1 = table.Column<string>(type: "TEXT", nullable: true),
                    rec2 = table.Column<string>(type: "TEXT", nullable: true),
                    rec3 = table.Column<string>(type: "TEXT", nullable: true),
                    rec4 = table.Column<string>(type: "TEXT", nullable: true),
                    rec5 = table.Column<string>(type: "TEXT", nullable: true),
                    rec6 = table.Column<string>(type: "TEXT", nullable: true),
                    rec7 = table.Column<string>(type: "TEXT", nullable: true),
                    rec8 = table.Column<string>(type: "TEXT", nullable: true),
                    rec9 = table.Column<string>(type: "TEXT", nullable: true),
                    rec10 = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemRecs", x => x.item);
                });

            migrationBuilder.CreateTable(
                name: "LineItems",
                columns: table => new
                {
                    transactionId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    productId = table.Column<int>(type: "INTEGER", nullable: false),
                    quantity = table.Column<int>(type: "INTEGER", nullable: false),
                    rating = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LineItems", x => x.transactionId);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    transactionId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    userId = table.Column<int>(type: "INTEGER", nullable: false),
                    transactionDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    dayOfWeek = table.Column<string>(type: "TEXT", nullable: false),
                    time = table.Column<int>(type: "INTEGER", nullable: false),
                    entryMode = table.Column<string>(type: "TEXT", nullable: false),
                    transactionAmount = table.Column<float>(type: "REAL", nullable: false),
                    countryOfTrnsaction = table.Column<string>(type: "TEXT", nullable: false),
                    shippingAddress = table.Column<string>(type: "TEXT", nullable: false),
                    bank = table.Column<string>(type: "TEXT", nullable: false),
                    typeOfCard = table.Column<string>(type: "TEXT", nullable: false),
                    fraud = table.Column<bool>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.transactionId);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    productId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    productName = table.Column<string>(type: "TEXT", nullable: false),
                    year = table.Column<int>(type: "INTEGER", nullable: false),
                    numParts = table.Column<int>(type: "INTEGER", nullable: false),
                    price = table.Column<float>(type: "REAL", nullable: false),
                    imgLink = table.Column<string>(type: "TEXT", nullable: false),
                    primaryColor = table.Column<string>(type: "TEXT", nullable: false),
                    secondaryColor = table.Column<string>(type: "TEXT", nullable: false),
                    description = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.productId);
                });

            migrationBuilder.CreateTable(
                name: "UserRecs",
                columns: table => new
                {
                    userId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    rec1 = table.Column<string>(type: "TEXT", nullable: true),
                    rec2 = table.Column<string>(type: "TEXT", nullable: true),
                    rec3 = table.Column<string>(type: "TEXT", nullable: true),
                    rec4 = table.Column<string>(type: "TEXT", nullable: true),
                    rec5 = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRecs", x => x.userId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    userId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    firstName = table.Column<string>(type: "TEXT", nullable: false),
                    lastName = table.Column<string>(type: "TEXT", nullable: false),
                    birthDate = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    country = table.Column<string>(type: "TEXT", nullable: false),
                    gender = table.Column<string>(type: "TEXT", nullable: false),
                    age = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.userId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemRecs");

            migrationBuilder.DropTable(
                name: "LineItems");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "UserRecs");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
