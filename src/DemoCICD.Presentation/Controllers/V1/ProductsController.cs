using DemoCICD.Contract.Abstractions.Shared;
using DemoCICD.Contract.Services.Product;
using DemoCICD.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;

namespace DemoCICD.Presentation.Controllers.V1;
[ApiVersion(1)]
public class ProductsController : ApiController
{
    public ProductsController(ISender sender)
        : base(sender)
    {
    }

    [HttpGet(Name = "GetProducts")]
    [ProducesResponseType(typeof(Result<IEnumerable<Response.ProductResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Products()
    {
        var result = await Sender.Send(new Query.GetProductQuery());
        return Ok(result);
    }

    [HttpPost(Name = "CreateProducts")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Products([FromBody] Command.CreateProduct CreateProduct)
    {
        var result = await Sender.Send(CreateProduct);
        return Ok(result);
    }

    [HttpGet("{productId}")]
    [ProducesResponseType(typeof(Result<Response.ProductResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Products(Guid productId)
    {
        Result<Response.ProductResponse> result = await Sender.Send(new Query.GetProductById(productId));
        return Ok(result);
    }

    [HttpPut("{productId}")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Products(Guid productId, [FromBody] Command.UpdateProduct updateProduct)
    {
        var updateProductCommand = new Command.UpdateProduct(
            productId,
            updateProduct.Name,
            updateProduct.Price,
            updateProduct.Description);

        var result = await Sender.Send(updateProductCommand);

        return Ok(result);
    }

    [HttpDelete("{productId}")]
    public async Task<IActionResult> Prodducts(Guid productId, [FromBody] Command.DeleteProduct deleteProduct)
    {
        var deleteProductCommand = new Command.DeleteProduct(productId);

        var result = await Sender.Send(deleteProductCommand);

        return Ok(result);
    }
}
