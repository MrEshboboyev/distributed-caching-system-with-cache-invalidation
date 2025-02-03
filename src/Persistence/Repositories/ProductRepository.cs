using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

internal sealed class ProductRepository(
    ApplicationDbContext dbContext
) : IProductRepository
{
    public async Task<Product?> GetByIdAsync(
        Guid id, 
        CancellationToken cancellationToken)
    {
        return await dbContext.Set<Product>()
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task AddAsync(
        Product product, 
        CancellationToken cancellationToken)
    {
        await dbContext.Set<Product>().AddAsync(product, cancellationToken);
    }
}