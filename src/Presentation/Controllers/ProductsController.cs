using Application.Products.Commands.CreateProduct;
using Application.Products.Queries.GetProduct;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("api/products")]
public sealed class ProductsController(ISender sender) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateProduct(
        CreateProductCommand request,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(request, cancellationToken);
        return result.IsFailure ? BadRequest(result.Error) : Ok(result.Value);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetProduct(
        Guid id,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetProductQuery(id), cancellationToken);
        return result.IsFailure ? NotFound(result.Error) : Ok(result.Value);
    }
}