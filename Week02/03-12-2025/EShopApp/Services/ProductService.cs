using System;
using EShopApp.Models;

namespace EShopApp.Services;

public class ProductService : IProductService
{

    private List<Product> _products = [
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

    public Product Add(Product product)
    {
        var maxId = _products.Any() ? _products.Max(p => p.Id) : 0;
        product.Id = maxId + 1;
        _products.Add(product);
        return product;
    }

    public bool CheckStockAvailability(int id, int requestedQuantity)
    {
        var product = _products.FirstOrDefault(p => p.Id == id);
        if (product is null)
        {
            return false;
        }
        return product.Stock>=requestedQuantity;
    }

    public bool Delete(int id)
    {
        var product = _products.FirstOrDefault(p => p.Id == id);
        if (product is null)
        {
            return false;
        }
        _products.Remove(product);
        return true;
    }

    public List<Product> GetAll()
    {
        return _products;
    }

    public Product? GetById(int id)
    {
        return _products.FirstOrDefault(p => p.Id == id);
    }

    public List<Product> GetLowStockProducts(int threshold)
    {
        return _products.Where(p => p.Stock <= threshold).ToList();
    }

    public List<Product> GetProductsByCategory(string category)
    {
        return _products.Where(p=>p.Category==category).ToList();
    }

    public Product? Update(int id, Product product)
    {
        var existingProduct = _products.FirstOrDefault(p => p.Id == id);
        if (existingProduct is null)
        {
            return null;
        }
        existingProduct.Name = product.Name;
        existingProduct.Price = product.Price;
        existingProduct.Stock = product.Stock;
        existingProduct.Description = product.Description;
        existingProduct.Category = product.Category;

        return existingProduct;
    }

    public Product? UpdateStock(int id, int quantityChange)
    {
        var product = _products.FirstOrDefault(x=>x.Id==id);
        if(product is null)
        {
            return null;
        }
        var newStock = product.Stock + quantityChange;
        if(newStock<0)
        {
            throw new InvalidOperationException($"Stok yetersiz! Mevcut Stok: {product.Stock}, İstenen: {quantityChange}");
        }
        product.Stock=newStock;
        return product;
    }
}
