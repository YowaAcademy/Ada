using System;
using EShopApp.Models;

namespace EShopApp.Services;

public interface IProductService
{
    List<Product> GetAll();
    Product? GetById(int id);
    Product Add(Product product);
    Product? Update(int id, Product product);
    bool Delete(int id);
    List<Product> GetLowStockProducts(int threshold);
}
