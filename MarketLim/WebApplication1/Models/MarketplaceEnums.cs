namespace WebApplication1.Models;

public enum UserRole
{
    Buyer = 0,
    Seller = 1,
    Admin = 2
}

public enum ProductStatus
{
    Available = 0,
    Reserved = 1,
    Sold = 2,
    Removed = 3
}

public enum OrderStatus
{
    Pending = 0,
    Completed = 1,
    Cancelled = 2
}

public enum TransactionStatus
{
    Pending = 0,
    Completed = 1,
    Cancelled = 2,
    Failed = 3
}

public enum TransactionType
{
    Purchase = 0,
    Cancellation = 1,
    Refund = 2
}
