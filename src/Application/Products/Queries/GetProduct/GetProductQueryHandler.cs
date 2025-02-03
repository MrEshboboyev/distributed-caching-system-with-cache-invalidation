using Application.Abstractions.Messaging;
using Domain.Entities;
using Domain.Errors;
using Domain.Interfaces;
using Domain.Shared;
using MediatR;

namespace Application.Products.Queries.GetProduct;

internal sealed class GetProductQueryHandler(
    IProductRepository productRepository
) : IQueryHandler<GetProductQuery, Product>
{
    public async Task<Result<Product>> Handle(
        GetProductQuery request,
        CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByIdAsync(request.Id, cancellationToken);
        return product is null 
            ? Result.Failure<Product>(
                DomainErrors.Product.NotFound(request.Id)) 
            : Result.Success(product);
    }
}