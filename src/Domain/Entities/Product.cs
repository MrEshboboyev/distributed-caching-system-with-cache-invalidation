using Domain.Errors;
using Domain.Primitives;
using Domain.Shared;

namespace Domain.Entities;

public sealed class Product : Entity, IAuditableEntity
{
    private Product(
        Guid id,
        string name,
        string description,
        decimal price,
        int stockQuantity)
        : base(id)
    {
        Name = name;
        Description = description;
        Price = price;
        StockQuantity = stockQuantity;
    }

    public string Name { get; private set; }
    public string Description { get; private set; }
    public decimal Price { get; private set; }
    public int StockQuantity { get; private set; }
    
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }

    public static Result<Product> Create(
        string name,
        string description,
        decimal price,
        int stockQuantity)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result.Failure<Product>(
                DomainErrors.Product.InvalidName);

        if (price <= 0)
            return Result.Failure<Product>(DomainErrors.Product.InvalidPrice);

        if (stockQuantity < 0)
            return Result.Failure<Product>(DomainErrors.Product.InvalidStockQuantity);

        return new Product(
            Guid.NewGuid(),
            name,
            description,
            price,
            stockQuantity);
    }
}