using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EShopApp.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Category = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    Stock = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    RowVersion = table.Column<byte[]>(type: "bytea", rowVersion: true, nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Category", "DeletedAt", "Description", "IsDeleted", "Name", "Price", "Stock" },
                values: new object[,]
                {
                    { 1, "Bilgisayar", null, "Yüksek performanslı laptop", false, "Laptop Pro", 1299.99m, 10 },
                    { 2, "Bilgisayar", null, "Oyuncular için gelişmiş laptop", false, "Gaming Laptop", 1599.99m, 7 },
                    { 3, "Bilgisayar", null, "Taşınabilir ve hafif ultrabook", false, "Ultrabook", 999.99m, 12 },
                    { 4, "Telefon", null, "Amiral gemisi akıllı telefon", false, "Smartphone X", 899.99m, 5 },
                    { 5, "Telefon", null, "Uygun fiyatlı akıllı telefon", false, "Smartphone Lite", 499.99m, 9 },
                    { 6, "Bilgisayar Ürünleri", null, "RGB mekanik klavye", false, "Mechanical Keyboard", 129.99m, 20 },
                    { 7, "Bilgisayar Ürünleri", null, "Yüksek hassasiyetli oyuncu faresi", false, "Gaming Mouse", 59.99m, 30 },
                    { 8, "Bilgisayar Ürünleri", null, "27 inç 4K monitör", false, "4K Monitor", 349.99m, 8 },
                    { 9, "Bilgisayar Ürünleri", null, "1080p çözünürlüklü webcam", false, "Webcam HD", 49.99m, 15 },
                    { 10, "Bilgisayar Ürünleri", null, "Çok amaçlı bağlantı istasyonu", false, "USB-C Dock", 79.99m, 14 },
                    { 11, "Bilgisayar Ürünleri", null, "1TB hızlı taşınabilir SSD", false, "External SSD", 149.99m, 10 },
                    { 12, "Bilgisayar Ürünleri", null, "DDR4 3200MHz RAM", false, "RAM 16GB", 69.99m, 18 },
                    { 13, "Bilgisayar Ürünleri", null, "Sessiz işlemci soğutucusu", false, "CPU Cooler", 39.99m, 13 },
                    { 14, "Bilgisayar Ürünleri", null, "650W 80+ Bronze güç kaynağı", false, "Power Supply 650W", 89.99m, 9 },
                    { 15, "Bilgisayar Ürünleri", null, "ATX oyuncu kasası", false, "PC Case", 99.99m, 6 },
                    { 16, "Aksesuar", null, "Taşınabilir bluetooth hoparlör", false, "Bluetooth Speaker", 39.99m, 25 },
                    { 17, "Aksesuar", null, "Yüksek kapasiteli powerbank", false, "Powerbank 20.000mAh", 29.99m, 30 },
                    { 18, "Aksesuar", null, "Hızlı şarj kablosu", false, "Charging Cable", 9.99m, 50 },
                    { 19, "Aksesuar", null, "Kablosuz hızlı şarj cihazı", false, "Wireless Charger", 24.99m, 22 },
                    { 20, "Aksesuar", null, "Ayarlanabilir telefon standı", false, "Phone Stand", 14.99m, 40 }
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
