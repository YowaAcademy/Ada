using System;
using EShopApp.Models;

namespace EShopApp.Services;

public interface IProductService
{
    Task<List<Product>> GetAllAsync();
    Task<Product?> GetByIdAsync(int id);
    Task<Product> AddAsync(Product product);
    Product? Update(int id, Product product);
    bool Delete(int id);
    Task<List<Product>> GetLowStockProductsAsync(int threshold);
    Task<List<Product>> GetProductsByCategoryAsync(string category);
    Task<Product?> UpdateStockAsync(int id, int quantityChange);
    Task<bool> CheckStockAvailabilityAsync(int id, int requestedQuantity);
    // Task Metot();
}
