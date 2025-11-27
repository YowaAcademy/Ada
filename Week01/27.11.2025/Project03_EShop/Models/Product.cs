using System;

namespace Project03_EShop.Models;

/// <summary>
/// E-Shop ürün modeli - Stok yönetimi için
/// </summary>
public class Product
{
    /// <summary>
    /// Ürün Id'si
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Ürün adı
    /// </summary>
    public string Name { get; set; }=string.Empty;


    /// <summary>
    /// Ürün açıklaması
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Ürün fiyatı (TL)
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Ürün stok miktarı (KRİTİK!)
    /// </summary>
    public int Stock { get; set; }
}
