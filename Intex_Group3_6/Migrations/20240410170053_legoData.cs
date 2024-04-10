using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Intex_Group3_6.Migrations
{
    /// <inheritdoc />
    public partial class legoData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ItemRecs",
                columns: table => new
                {
                    item = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    rec1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    rec2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    rec3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    rec4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    rec5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    rec6 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    rec7 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    rec8 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    rec9 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    rec10 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemRecs", x => x.item);
                });

            migrationBuilder.CreateTable(
                name: "LineItems",
                columns: table => new
                {
                    TransactionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    rating = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LineItems", x => x.TransactionId);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    transactionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<int>(type: "int", nullable: false),
                    transactionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dayOfWeek = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    time = table.Column<int>(type: "int", nullable: false),
                    entryMode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    transactionAmount = table.Column<float>(type: "real", nullable: false),
                    typeOfTransaction = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    countryOfTransaction = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    shippingAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    bank = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    typeOfCard = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    fraud = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.transactionId);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    productId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    productName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    year = table.Column<int>(type: "int", nullable: false),
                    numParts = table.Column<int>(type: "int", nullable: false),
                    price = table.Column<float>(type: "real", nullable: false),
                    imgLink = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    primaryColor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    secondaryColor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    category1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    category2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    category3 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.productId);
                });

            migrationBuilder.CreateTable(
                name: "UserRecs",
                columns: table => new
                {
                    userId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    rec1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    rec2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    rec3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    rec4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    rec5 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRecs", x => x.userId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    userId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    firstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    lastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    birthDate = table.Column<DateOnly>(type: "date", nullable: false),
                    country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    age = table.Column<int>(type: "int", nullable: false)
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
