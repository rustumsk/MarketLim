using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models;

public class MarketplaceTransaction
{
    public int Id { get; set; }

    public int OrderId { get; set; }

    public Order Order { get; set; } = null!;

    public int BuyerId { get; set; }

    public User Buyer { get; set; } = null!;

    public int SellerId { get; set; }

    public User Seller { get; set; } = null!;

    [Range(0.01, 999999.99)]
    public decimal Amount { get; set; }

    public TransactionType Type { get; set; } = TransactionType.Purchase;

    public TransactionStatus Status { get; set; } = TransactionStatus.Pending;

    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;

    [MaxLength(500)]
    public string? Notes { get; set; }
}
