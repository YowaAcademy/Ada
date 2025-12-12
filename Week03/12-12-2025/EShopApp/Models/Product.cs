using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EShopApp.Models;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string Category { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public DateTime CreatedAt { get; set; }

    // Concurency token
    [Timestamp]
    public byte[]? RowVersion { get; set; } = Array.Empty<byte>();

    // Soft Delete Properties
    public bool IsDeleted { get; set; } = false;
    public DateTime? DeletedAt { get; set; }
}
