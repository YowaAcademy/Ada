using System;
using Project03_EShop.Models;

namespace Project03_EShop.Services;

public interface IProductService
{
    List<Product> GetAll();
    Product? GetById(int id);
    List<Product> GetLowStockProducts(int threshold=10);
    Product Add(Product product);
    void Update(Product product);
    void Delete(int id);
}
