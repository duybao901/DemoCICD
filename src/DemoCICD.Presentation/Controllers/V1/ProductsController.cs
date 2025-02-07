﻿using Asp.Versioning;
using DemoCICD.Contract.Abstractions.Shared;
using DemoCICD.Contract.Enumerations;
using DemoCICD.Contract.Extentions;
using DemoCICD.Contract.Services.V1.Product;
using DemoCICD.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<IActionResult> Products(
        string? searchTerm = null,
        string? sortColumn = null,
        string? sortOrder = null,
        string? sortOrderAndColumn = null,
        int pageIndex = 1,
        int pageSize = 5)
    {
        var result = await Sender.Send(new Query.GetProductQuery(
            searchTerm,
            sortColumn,
            SortOrderExtension.ConvertStringToSortOrder(sortOrder),
            SortOrderExtension.ConvertStringToSortOrderV2(sortOrderAndColumn),
            pageIndex,
            pageSize));
        return Ok(result);
    }

    [HttpPost(Name = "CreateProducts")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Products([FromBody] Command.CreateProductCommand CreateProduct)
    {
        var result = await Sender.Send(CreateProduct);

        if (result.IsFailure)
        {
            return HandleFailureReturn(result);
        }

        return Ok(result);
    }

    [HttpGet("{productId}")]
    [ProducesResponseType(typeof(Result<Response.ProductResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Products(Guid productId)
    {
        Result<Response.ProductResponse> result = await Sender.Send(new Query.GetProductByIdQuery(productId));
        return Ok(result);
    }

    [HttpPut("{productId}")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Products(Guid productId, [FromBody] Command.UpdateProductCommand updateProduct)
    {
        var updateProductCommand = new Command.UpdateProductCommand(
            productId,
            updateProduct.Name,
            updateProduct.Price,
            updateProduct.Description);

        var result = await Sender.Send(updateProductCommand);

        return Ok(result);
    }

    [HttpDelete("{productId}")]
    public async Task<IActionResult> Prodducts(Guid productId)
    {
        var deleteProductCommand = new Command.DeleteProductCommand(productId);

        var result = await Sender.Send(deleteProductCommand);

        return Ok(result);
    }
}
