using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Companies.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Exchange = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ticker = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Isin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Website = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: ["Id", "Name", "Exchange", "Ticker", "Isin", "Website"],
                values: new object[,]
                {
                    { Guid.NewGuid(), "Apple Inc.", "NASDAQ", "AAPL", "US0378331005", "https://www.apple.com" },
                    { Guid.NewGuid(), "British Airways Plc", "Pink Sheets", "BAIRY", "US1104193065", null },
                    { Guid.NewGuid(), "Heineken NV", "Euronext Amsterdam", "HEIA", "NL0000009165", null },
                    { Guid.NewGuid(), "Panasonic Corp", "Tokyo Stock Exchange", "6752", "JP3866800000", "http://www.panasonic.co.jp" },
                    { Guid.NewGuid(), "Porsche Automobil", "Deutsche Börse", "PAH3", "DE000PAH0038", "https://www.porsche.com/" },
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Companies");
        }
    }
}
