using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models;

public class Product
{
    public int Id { get; set; }

    [Required]
    [MaxLength(150)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MaxLength(1000)]
    public string Description { get; set; } = string.Empty;

    [Range(0.01, 999999.99)]
    public decimal Price { get; set; }

    [Range(0, int.MaxValue)]
    public int QuantityAvailable { get; set; } = 1;

    [MaxLength(500)]
    public string? ImagePath { get; set; }

    public ProductStatus Status { get; set; } = ProductStatus.Available;

    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAtUtc { get; set; }

    public int SellerId { get; set; }

    public User Seller { get; set; } = null!;

    public int CategoryId { get; set; }

    public Category Category { get; set; } = null!;

    public ICollection<Order> Orders { get; set; } = new List<Order>();
}
