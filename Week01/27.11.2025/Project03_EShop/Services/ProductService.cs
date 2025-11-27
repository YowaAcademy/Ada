using System;
using Project03_EShop.Models;

namespace Project03_EShop.Services;

public class ProductService : IProductService
{
    private List<Product> _products = [
        new Product { Id = 1, Name = "Laptop", Description = "A high-performance laptop", Price = 999.99m, Stock = 15 },
        new Product { Id = 2, Name = "Smartphone", Description = "A latest model smartphone", Price = 699.99m, Stock = 8 },
        new Product { Id = 3, Name = "Headphones", Description = "Noise-cancelling headphones", Price = 199.99m, Stock = 5 },
        new Product { Id = 4, Name = "Monitor", Description = "4K UHD Monitor", Price = 399.99m, Stock = 12 },
        new Product { Id = 5, Name = "Keyboard", Description = "Mechanical keyboard", Price = 89.99m, Stock = 2 }
    ];

    public Product Add(Product product)
    {
        _products.Add(product);
        return product;
    }

    public void Delete(int id)
    {
        var product = GetById(id);
        if(product is not null)
            _products.Remove(product);
    }

    public List<Product> GetAll()
    {
        return _products;
    }

    public Product? GetById(int id)
    {
        // return _products.Where(x=>x.Id==id).FirstOrDefault();
        return _products.FirstOrDefault(p => p.Id == id);
    }

    public List<Product> GetLowStockProducts(int threshold = 10)
    {
        return _products.Where(p => p.Stock < threshold).ToList();
    }

    public void Update(Product product)
    {
        var existsProduct= _products.FirstOrDefault(x=>x.Id==product.Id);
        if(existsProduct is not null)
        {
            existsProduct.Name=product.Name;
            existsProduct.Description=product.Description;
            existsProduct.Price=product.Price;
            existsProduct.Stock=product.Stock;
        }
    }
}
