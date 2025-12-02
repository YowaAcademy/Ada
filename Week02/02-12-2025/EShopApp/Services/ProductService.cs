using System;
using EShopApp.Models;

namespace EShopApp.Services;

public class ProductService : IProductService
{
    private List<Product> _products = [
        new Product { Id = 1, Name = "Laptop", Description = "A high-performance laptop", Price = 999.99m, Stock = 15, Category="Bilgisayar" },
        new Product { Id = 2, Name = "Smartphone", Description = "A latest model smartphone", Price = 699.99m, Stock = 8, Category="Telefon" },
        new Product { Id = 3, Name = "Headphones", Description = "Noise-cancelling headphones", Price = 199.99m, Stock = 5, Category="Telefon" },
        new Product { Id = 4, Name = "Monitor", Description = "4K UHD Monitor", Price = 399.99m, Stock = 12, Category="Bilgisayar Ürünleri" },
        new Product { Id = 5, Name = "Keyboard", Description = "Mechanical keyboard", Price = 89.99m, Stock = 2, Category="Bilgisayar Ürünleri" }
];
    public Product Add(Product product)
    {
        var maxId = _products.Any() ? _products.Max(p => p.Id) : 0;
        product.Id = maxId + 1;
        _products.Add(product);
        return product;
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
}
