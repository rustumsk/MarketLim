using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<MarketplaceTransaction> Transactions => Set<MarketplaceTransaction>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("Users");
            entity.HasIndex(user => user.Email).IsUnique();
            entity.Property(user => user.Role).HasConversion<string>().HasMaxLength(50);
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable("Categories");
            entity.HasIndex(category => category.Name).IsUnique();
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("Products");
            entity.Property(product => product.Price).HasPrecision(18, 2);
            entity.Property(product => product.Status).HasConversion<string>().HasMaxLength(50);

            entity.HasOne(product => product.Seller)
                .WithMany(user => user.ProductsForSale)
                .HasForeignKey(product => product.SellerId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(product => product.Category)
                .WithMany(category => category.Products)
                .HasForeignKey(product => product.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.ToTable("Orders");
            entity.Property(order => order.UnitPrice).HasPrecision(18, 2);
            entity.Property(order => order.TotalAmount).HasPrecision(18, 2);
            entity.Property(order => order.Status).HasConversion<string>().HasMaxLength(50);

            entity.HasOne(order => order.Product)
                .WithMany(product => product.Orders)
                .HasForeignKey(order => order.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(order => order.Buyer)
                .WithMany(user => user.Purchases)
                .HasForeignKey(order => order.BuyerId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(order => order.Seller)
                .WithMany(user => user.Sales)
                .HasForeignKey(order => order.SellerId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<MarketplaceTransaction>(entity =>
        {
            entity.ToTable("Transactions");
            entity.Property(transaction => transaction.Amount).HasPrecision(18, 2);
            entity.Property(transaction => transaction.Type).HasConversion<string>().HasMaxLength(50);
            entity.Property(transaction => transaction.Status).HasConversion<string>().HasMaxLength(50);

            entity.HasOne(transaction => transaction.Order)
                .WithMany(order => order.Transactions)
                .HasForeignKey(transaction => transaction.OrderId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(transaction => transaction.Buyer)
                .WithMany()
                .HasForeignKey(transaction => transaction.BuyerId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(transaction => transaction.Seller)
                .WithMany()
                .HasForeignKey(transaction => transaction.SellerId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}
