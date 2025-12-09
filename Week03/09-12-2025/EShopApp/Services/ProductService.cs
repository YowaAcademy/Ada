using System;
using EShopApp.Data;
using EShopApp.Models;
using Microsoft.EntityFrameworkCore;

namespace EShopApp.Services;

public class ProductService : IProductService
{
    private readonly AppDbContext _context;

    public ProductService(AppDbContext context)
    {
        _context = context;
    }

    public Task<Product> AddAsync(Product product)
    {
        throw new NotImplementedException();
    }

    public Task<bool> CheckStockAvailabilityAsync(int id, int requestedQuantity)
    {
        throw new NotImplementedException();
    }

    public bool Delete(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Product>> GetAllAsync()
    {
        var products = await _context.Products.ToListAsync();
        return products;
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        // var product = await _context.Products.Where(p=>p.Id==id).FirstOrDefaultAsync();
        var product = await _context.Products.FindAsync(id);
        return product;
    }

    public async Task<List<Product>> GetLowStockProductsAsync(int threshold)
    {
        var products = await _context
            .Products
            .Where(p => p.Stock < threshold)
            .ToListAsync();
        return products;
    }

    public async Task<List<Product>> GetProductsByCategoryAsync(string category)
    {
        var products = await _context
            .Products
            .Where(p => p.Category == category)
            .ToListAsync();
        return products;
    }

    public Product? Update(int id, Product product)
    {
        throw new NotImplementedException();
    }

    public Task<Product?> UpdateStockAsync(int id, int quantityChange)
    {
        throw new NotImplementedException();
    }
}
