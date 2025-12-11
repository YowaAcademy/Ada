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

    public async Task<Product> AddAsync(Product product)
    {
        if (product is null)
        {
            throw new ArgumentException("Ürün bilgisi boş olamaz!", nameof(product));
        }
        if (string.IsNullOrWhiteSpace(product.Name))
        {
            throw new ArgumentException("Ürün adı boş olamaz!", nameof(product));
        }
        if (product.Stock < 0)
        {
            throw new ArgumentException("Stok miktarı negatif olamaz!", nameof(product));
        }
        if (product.Price < 0)
        {
            throw new ArgumentException("Ürün fiyatı negatif olamaz!", nameof(product));
        }
        var exists = await _context
            .Products
            .AnyAsync(p => p.Name == product.Name);
        if (exists)
        {
            throw new InvalidOperationException($"'{product.Name}' adında bir ürün zaten mevcut!");
        }
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();

        return product;
    }

    public async Task<bool> CheckStockAvailabilityAsync(int id, int requestedQuantity)
    {
        var product = await _context.Products.FindAsync(id);
        if (product is null) return false;
        return product.Stock >= requestedQuantity;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var product = await _context.Products.IgnoreQueryFilters().Where(p => p.Id == id).FirstOrDefaultAsync();
        if (product is null)
        {
            return false;
        }
        product.IsDeleted = !product.IsDeleted;
        product.DeletedAt = DateTime.UtcNow;

        var result = await _context.SaveChangesAsync();
        return result > 0;
    }

    public async Task<List<Product>> FilterAsync(ProductQuery query)
    {
        IQueryable<Product> productsQuery = _context.Products;
        if (!string.IsNullOrEmpty(query.Category))
        {
            productsQuery = productsQuery.Where(p => p.Category.ToLower() == query.Category.ToLower());
        }
        if (query.MinPrice.HasValue)
        {
            productsQuery = productsQuery.Where(p => p.Price >= query.MinPrice.Value);
        }
        if (query.MaxPrice.HasValue)
        {
            productsQuery = productsQuery.Where(p => p.Price <= query.MaxPrice.Value);
        }
        if (query.MinStock.HasValue)
        {
            productsQuery = productsQuery.Where(p => p.Stock >= query.MinStock.Value);
        }

        var result = await productsQuery.OrderBy(p => p.Name).ToListAsync();
        return result;
    }

    public async Task<List<Product>> GetAllAsync()
    {
        var products = await _context
            .Products
            .OrderBy(p => p.Name)
            .ToListAsync();
        return products;
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        // var product = await _context.Products.Where(p=>p.Id==id).FirstOrDefaultAsync();
        var product = await _context.Products.FindAsync(id);
        return product;
    }

    public Task<List<Product>> GetDeletedProductsAsync()
    {
        var deletedProducts = _context
            .Products
            .IgnoreQueryFilters()
            .Where(p => p.IsDeleted)
            .ToListAsync();
        return deletedProducts;
    }

    public async Task<List<Product>> GetLowStockProductsAsync(int threshold)
    {
        var products = await _context
            .Products
            .Where(p => p.Stock < threshold)
            .OrderBy(p => p.Stock)
            .ToListAsync();
        return products;
    }

    public async Task<List<Product>> GetProductsByCategoryAsync(string category)
    {
        var products = await _context
            .Products
            .Where(p => p.Category == category)
            .OrderBy(p => p.Name)
            .ToListAsync();
        return products;
    }

    public async Task<(List<Product> Products, int TotalCount)> GetProductsPagedAsync(int pageNumber = 2, int pageSize = 10)
    {
        var totalCount = await _context.Products.CountAsync();
        var products = await _context
            .Products
            .OrderBy(p => p.Name)
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync();
        return (products, totalCount);
    }

    public async Task<Product?> UpdateAsync(int id, Product product)
    {
        if (product is null)
        {
            throw new ArgumentException("Ürün bilgisi boş olamaz!", nameof(product));
        }
        if (string.IsNullOrWhiteSpace(product.Name))
        {
            throw new ArgumentException("Ürün adı boş olamaz!", nameof(product));
        }
        if (id != product.Id)
        {
            throw new ArgumentException("Id değerleri eşleşmiyor", nameof(product));
        }
        if (product.Stock < 0)
        {
            throw new ArgumentException("Stok miktarı negatif olamaz!", nameof(product));
        }
        if (product.Price < 0)
        {
            throw new ArgumentException("Ürün fiyatı negatif olamaz!", nameof(product));
        }


        var existingProduct = await _context.Products.FindAsync(id);
        if (existingProduct is null)
        {
            return null;
        }


        // Rowversion kontrolü(concurency token)
        if (product.RowVersion is not null && !existingProduct.RowVersion.SequenceEqual(product.RowVersion))
        {
            throw new DbUpdateConcurrencyException("Ürün başka bir kullanıcı tarafından güncellenemiş. Lütfen yeniden deneyiniz!");
        }

        existingProduct.Name = product.Name;
        existingProduct.Price = product.Price;
        existingProduct.Stock = product.Stock;
        existingProduct.Category = product.Category;
        existingProduct.Description = product.Description;
        existingProduct.RowVersion = product.RowVersion!;

        await _context.SaveChangesAsync();

        return existingProduct;
    }

    public async Task<Product?> UpdateStockAsync(int id, int quantityChange)
    {
        var product = await _context.Products.FindAsync(id);
        if (product is null)
        {
            throw new ArgumentNullException(nameof(product), "Ürün bulunamadığı için stok güncellenemedi!");
        }
        var newStock = product.Stock + quantityChange;
        if (newStock < 0)
        {
            throw new InvalidOperationException($"Stok yetersiz! Mevcut stok: {product.Stock}, İstenen değişiklik: {quantityChange}");
        }
        product.Stock = newStock;
        await _context.SaveChangesAsync();
        return product;
    }
}


// public async Task<bool> DeleteAsync(int id)
// {
//     var product = await _context.Products.FindAsync(id);
//     if (product is null)
//     {
//         return false;
//     }
//     _context.Products.Remove(product);
//     var result = await _context.SaveChangesAsync();
//     return result > 0;
// }


// var entry = _context.Entry(existingProduct);

// entry.Property(p=>p.Name).IsModified=true;