using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models;

public class Order
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public Product Product { get; set; } = null!;

    public int BuyerId { get; set; }

    public User Buyer { get; set; } = null!;

    public int SellerId { get; set; }

    public User Seller { get; set; } = null!;

    [Range(1, int.MaxValue)]
    public int Quantity { get; set; } = 1;

    [Range(0.01, 999999.99)]
    public decimal UnitPrice { get; set; }

    [Range(0.01, 999999.99)]
    public decimal TotalAmount { get; set; }

    public OrderStatus Status { get; set; } = OrderStatus.Pending;

    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;

    public DateTime? CompletedAtUtc { get; set; }

    public DateTime? CancelledAtUtc { get; set; }

    [MaxLength(500)]
    public string? CancellationReason { get; set; }

    public ICollection<MarketplaceTransaction> Transactions { get; set; } = new List<MarketplaceTransaction>();
}
