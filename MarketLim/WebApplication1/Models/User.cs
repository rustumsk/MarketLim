using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models;

public class User
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string FullName { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [MaxLength(150)]
    public string Email { get; set; } = string.Empty;

    [Required]
    [MaxLength(255)]
    public string PasswordHash { get; set; } = string.Empty;

    [MaxLength(500)]
    public string? ProfilePicturePath { get; set; }

    [MaxLength(100)]
    public string? CourseOrSection { get; set; }

    public UserRole Role { get; set; } = UserRole.Buyer;

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAtUtc { get; set; }

    public ICollection<Product> ProductsForSale { get; set; } = new List<Product>();

    public ICollection<Order> Purchases { get; set; } = new List<Order>();

    public ICollection<Order> Sales { get; set; } = new List<Order>();
}
