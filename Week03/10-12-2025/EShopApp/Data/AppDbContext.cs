using System;
using EShopApp.Models;
using Microsoft.EntityFrameworkCore;

namespace EShopApp.Data;

public class AppDbContext : DbContext
{

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }
    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // modelBuilder.Entity<Product>().HasKey(p=>p.Id);
        // modelBuilder.Entity<Product>().Property(p=>p.Name).IsRequired().HasMaxLength(100);

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Name).IsRequired().HasMaxLength(100);
            entity.Property(p => p.Price).IsRequired();
            entity.Property(p => p.Stock).IsRequired();
            entity.Property(p => p.Category).HasMaxLength(50);
            entity.Property(p => p.Description).HasMaxLength(500);
            entity.HasIndex(p => p.Name).IsUnique();
            entity.Property(p => p.CreatedAt).ValueGeneratedOnAdd().HasDefaultValueSql("CURRENT_TIMESTAMP");
        });

        List<Product> products = [
            // Kategori 1: Bilgisayar (3 adet)
            new Product { Id = 1, Name = "Laptop Pro", Description = "Yüksek performanslı laptop", Price = 1299.99m, Stock = 10, Category = "Bilgisayar" },
            new Product { Id = 2, Name = "Gaming Laptop", Description = "Oyuncular için gelişmiş laptop", Price = 1599.99m, Stock = 7, Category = "Bilgisayar" },
            new Product { Id = 3, Name = "Ultrabook", Description = "Taşınabilir ve hafif ultrabook", Price = 999.99m, Stock = 12, Category = "Bilgisayar" },

            // Kategori 2: Telefon (2 adet)
            new Product { Id = 4, Name = "Smartphone X", Description = "Amiral gemisi akıllı telefon", Price = 899.99m, Stock = 5, Category = "Telefon" },
            new Product { Id = 5, Name = "Smartphone Lite", Description = "Uygun fiyatlı akıllı telefon", Price = 499.99m, Stock = 9, Category = "Telefon" },

            // Kategori 3: Bilgisayar Ürünleri (10 adet)
            new Product { Id = 6, Name = "Mechanical Keyboard", Description = "RGB mekanik klavye", Price = 129.99m, Stock = 20, Category = "Bilgisayar Ürünleri" },
            new Product { Id = 7, Name = "Gaming Mouse", Description = "Yüksek hassasiyetli oyuncu faresi", Price = 59.99m, Stock = 30, Category = "Bilgisayar Ürünleri" },
            new Product { Id = 8, Name = "4K Monitor", Description = "27 inç 4K monitör", Price = 349.99m, Stock = 8, Category = "Bilgisayar Ürünleri" },
            new Product { Id = 9, Name = "Webcam HD", Description = "1080p çözünürlüklü webcam", Price = 49.99m, Stock = 15, Category = "Bilgisayar Ürünleri" },
            new Product { Id = 10, Name = "USB-C Dock", Description = "Çok amaçlı bağlantı istasyonu", Price = 79.99m, Stock = 14, Category = "Bilgisayar Ürünleri" },
            new Product { Id = 11, Name = "External SSD", Description = "1TB hızlı taşınabilir SSD", Price = 149.99m, Stock = 10, Category = "Bilgisayar Ürünleri" },
            new Product { Id = 12, Name = "RAM 16GB", Description = "DDR4 3200MHz RAM", Price = 69.99m, Stock = 18, Category = "Bilgisayar Ürünleri" },
            new Product { Id = 13, Name = "CPU Cooler", Description = "Sessiz işlemci soğutucusu", Price = 39.99m, Stock = 13, Category = "Bilgisayar Ürünleri" },
            new Product { Id = 14, Name = "Power Supply 650W", Description = "650W 80+ Bronze güç kaynağı", Price = 89.99m, Stock = 9, Category = "Bilgisayar Ürünleri" },
            new Product { Id = 15, Name = "PC Case", Description = "ATX oyuncu kasası", Price = 99.99m, Stock = 6, Category = "Bilgisayar Ürünleri" },

            // Kategori 4: Aksesuar (5 adet)
            new Product { Id = 16, Name = "Bluetooth Speaker", Description = "Taşınabilir bluetooth hoparlör", Price = 39.99m, Stock = 25, Category = "Aksesuar" },
            new Product { Id = 17, Name = "Powerbank 20.000mAh", Description = "Yüksek kapasiteli powerbank", Price = 29.99m, Stock = 30, Category = "Aksesuar" },
            new Product { Id = 18, Name = "Charging Cable", Description = "Hızlı şarj kablosu", Price = 9.99m, Stock = 50, Category = "Aksesuar" },
            new Product { Id = 19, Name = "Wireless Charger", Description = "Kablosuz hızlı şarj cihazı", Price = 24.99m, Stock = 22, Category = "Aksesuar" },
            new Product { Id = 20, Name = "Phone Stand", Description = "Ayarlanabilir telefon standı", Price = 14.99m, Stock = 40, Category = "Aksesuar" }
        ];

        modelBuilder.Entity<Product>().HasData(products);

    }
}
