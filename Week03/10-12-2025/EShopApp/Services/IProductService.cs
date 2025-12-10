using System;
using EShopApp.Models;

namespace EShopApp.Services;

public interface IProductService
{
    Task<List<Product>> GetAllAsync();
    Task<List<Product>> GetLowStockProductsAsync(int threshold);
    Task<List<Product>> GetProductsByCategoryAsync(string category);
    Task<(List<Product> Products, int TotalCount)> GetProductsPagedAsync(int pageNumber, int pageSize);
    Task<List<Product>> FilterAsync(ProductQuery query);
    Task<Product?> GetByIdAsync(int id);
    Task<Product> AddAsync(Product product);
    Task<Product?> UpdateAsync(int id, Product product);
    Task<bool> DeleteAsync(int id);
    Task<Product?> UpdateStockAsync(int id, int quantityChange);
    Task<bool> CheckStockAvailabilityAsync(int id, int requestedQuantity);
    
}
