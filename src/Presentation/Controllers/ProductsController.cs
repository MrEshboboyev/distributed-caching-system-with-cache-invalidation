using Application.Products.Commands.CreateProduct;
using Application.Products.Queries.GetProduct;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.Abstractions;

namespace Presentation.Controllers;

[ApiController]
[Route("api/products")]
public sealed class ProductsController(ISender sender) : ApiController(sender)
{
    [HttpPost]
    public async Task<IActionResult> CreateProduct(
        CreateProductCommand request,
        CancellationToken cancellationToken)
    {
        var result = await Sender.Send(request, cancellationToken);
        return result.IsSuccess ? Ok(result) : BadRequest(request);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetProduct(
        Guid id,
        CancellationToken cancellationToken)
    {
        var result = await Sender.Send(new GetProductQuery(id), cancellationToken);        
        return result.IsSuccess ? Ok(result) : NotFound();
    }
}