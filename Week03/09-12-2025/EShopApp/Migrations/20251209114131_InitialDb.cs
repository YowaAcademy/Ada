using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EShopApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    Category = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Price = table.Column<decimal>(type: "TEXT", nullable: false),
                    Stock = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Category", "Description", "Name", "Price", "Stock" },
                values: new object[,]
                {
                    { 1, "Bilgisayar", "Yüksek performanslı laptop", "Laptop Pro", 1299.99m, 10 },
                    { 2, "Bilgisayar", "Oyuncular için gelişmiş laptop", "Gaming Laptop", 1599.99m, 7 },
                    { 3, "Bilgisayar", "Taşınabilir ve hafif ultrabook", "Ultrabook", 999.99m, 12 },
                    { 4, "Telefon", "Amiral gemisi akıllı telefon", "Smartphone X", 899.99m, 5 },
                    { 5, "Telefon", "Uygun fiyatlı akıllı telefon", "Smartphone Lite", 499.99m, 9 },
                    { 6, "Bilgisayar Ürünleri", "RGB mekanik klavye", "Mechanical Keyboard", 129.99m, 20 },
                    { 7, "Bilgisayar Ürünleri", "Yüksek hassasiyetli oyuncu faresi", "Gaming Mouse", 59.99m, 30 },
                    { 8, "Bilgisayar Ürünleri", "27 inç 4K monitör", "4K Monitor", 349.99m, 8 },
                    { 9, "Bilgisayar Ürünleri", "1080p çözünürlüklü webcam", "Webcam HD", 49.99m, 15 },
                    { 10, "Bilgisayar Ürünleri", "Çok amaçlı bağlantı istasyonu", "USB-C Dock", 79.99m, 14 },
                    { 11, "Bilgisayar Ürünleri", "1TB hızlı taşınabilir SSD", "External SSD", 149.99m, 10 },
                    { 12, "Bilgisayar Ürünleri", "DDR4 3200MHz RAM", "RAM 16GB", 69.99m, 18 },
                    { 13, "Bilgisayar Ürünleri", "Sessiz işlemci soğutucusu", "CPU Cooler", 39.99m, 13 },
                    { 14, "Bilgisayar Ürünleri", "650W 80+ Bronze güç kaynağı", "Power Supply 650W", 89.99m, 9 },
                    { 15, "Bilgisayar Ürünleri", "ATX oyuncu kasası", "PC Case", 99.99m, 6 },
                    { 16, "Aksesuar", "Taşınabilir bluetooth hoparlör", "Bluetooth Speaker", 39.99m, 25 },
                    { 17, "Aksesuar", "Yüksek kapasiteli powerbank", "Powerbank 20.000mAh", 29.99m, 30 },
                    { 18, "Aksesuar", "Hızlı şarj kablosu", "Charging Cable", 9.99m, 50 },
                    { 19, "Aksesuar", "Kablosuz hızlı şarj cihazı", "Wireless Charger", 24.99m, 22 },
                    { 20, "Aksesuar", "Ayarlanabilir telefon standı", "Phone Stand", 14.99m, 40 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_Name",
                table: "Products",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
