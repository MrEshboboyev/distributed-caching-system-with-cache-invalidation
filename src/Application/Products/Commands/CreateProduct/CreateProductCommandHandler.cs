using Application.Abstractions.Messaging;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Shared;

namespace Application.Products.Commands.CreateProduct;

internal sealed class CreateProductCommandHandler(
    IProductRepository productRepository,
    IUnitOfWork unitOfWork
) : ICommandHandler<CreateProductCommand, Guid>
{
    public async Task<Result<Guid>> Handle(
        CreateProductCommand request,
        CancellationToken cancellationToken)
    {
        var productResult = Product.Create(
            request.Name,
            request.Description,
            request.Price,
            request.StockQuantity);

        if (productResult.IsFailure)
            return Result.Failure<Guid>(productResult.Error);

        await productRepository.AddAsync(productResult.Value, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(productResult.Value.Id);
    }
}