using Asp.Versioning.Builder;
using Carter;
using DemoCICD.Contract.Abstractions.Shared;
using DemoCICD.Contract.Extentions;
using DemoCICD.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

using CommandV1 = DemoCICD.Contract.Services.V1.Product;
using CommandV2 = DemoCICD.Contract.Services.V2.Product;

namespace DemoCICD.Presentation.Endpoints.Products;
public class ProductCarterApi : ApiEnpoint, ICarterModule
{
    private const string BaseUrl = "/api/carter/v{version:apiVersion}/products";

    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.NewVersionedApi("products-carter-name-show-on-swagger")
            .MapGroup(BaseUrl).HasApiVersion(1);

        group.MapPost(string.Empty, CreateProductsV1);
        group.MapGet(string.Empty, GetProductsV1);
        group.MapGet("{productId}", GetProductsByIdV1);
        group.MapPut("{productId}", UpdateProductsV1);
        group.MapDelete("{productId}", DeleteProdductsV1);

        var group2 = app.NewVersionedApi("products-carter-name-show-on-swagger")
            .MapGroup(BaseUrl).HasApiVersion(2);

        group2.MapPost(string.Empty, CreateProductsV2);
        group2.MapGet(string.Empty, GetProductsV2);
        group2.MapGet("{productId}", GetProductsByIdV2);
        group2.MapPut("{productId}", UpdateProductsV2);
        group2.MapDelete("{productId}", DeleteProdductsV2);
    }

    #region ========== V1 ==========
    public static async Task<IResult> GetProductsV1(ISender Sender,
        string? searchTerm = null,
        string? sortColumn = null,
        string? sortOrder = null,
        string? sortOrderAndColumn = null,
        int pageIndex = 1,
        int pageSize = 5)
    {
        var result = await Sender.Send(new CommandV1.Query.GetProductQuery(
            searchTerm,
            sortColumn,
            SortOrderExtension.ConvertStringToSortOrder(sortOrder),
            SortOrderExtension.ConvertStringToSortOrderV2(sortOrderAndColumn),
            pageIndex,
            pageSize));
        return Results.Ok(result);
    }

    public static async Task<IResult> CreateProductsV1(ISender Sender, [FromBody] CommandV1.Command.CreateProductCommand CreateProduct)
    {
        var result = await Sender.Send(CreateProduct);

        //if (result.IsFailure)
        //{
        //    return HandleFailureReturn(result);
        //}

        return Results.Ok(result);
    }

    public static async Task<IResult> GetProductsByIdV1(ISender Sender, Guid productId)
    {
        Result<CommandV1.Response.ProductResponse> result = await Sender.Send(new CommandV1.Query.GetProductByIdQuery(productId));
        return Results.Ok(result);
    }

    public static async Task<IResult> UpdateProductsV1(ISender Sender, Guid productId, [FromBody] CommandV1.Command.UpdateProductCommand updateProduct)
    {
        var updateProductCommand = new CommandV1.Command.UpdateProductCommand(
            productId,
            updateProduct.Name,
            updateProduct.Price,
            updateProduct.Description);

        var result = await Sender.Send(updateProductCommand);

        return Results.Ok(result);
    }

    public static async Task<IResult> DeleteProdductsV1(ISender Sender, Guid productId)
    {
        var deleteProductCommand = new CommandV1.Command.DeleteProductCommand(productId);

        var result = await Sender.Send(deleteProductCommand);

        return Results.Ok(result);
    }
    #endregion ========== V1 ==========

    #region ========== V2 ==========
    public static async Task<IResult> GetProductsV2(ISender Sender,
        string? searchTerm = null,
        string? sortColumn = null,
        string? sortOrder = null,
        string? sortOrderAndColumn = null,
        int pageIndex = 1,
        int pageSize = 5)
    {
        var result = await Sender.Send(new CommandV2.Query.GetProductQuery());
        return Results.Ok(result);
    }

    public static async Task<IResult> CreateProductsV2(ISender Sender, [FromBody] CommandV2.Command.CreateProductCommand CreateProduct)
    {
        var result = await Sender.Send(CreateProduct);

        if (result.IsFailure)
        {
            return HandleFailureReturn(result);
        }

        return Results.Ok(result);
    }

    public static async Task<IResult> GetProductsByIdV2(ISender Sender, Guid productId)
    {
        Result<CommandV2.Response.ProductResponse> result = await Sender.Send(new CommandV2.Query.GetProductByIdQuery(productId));
        return Results.Ok(result);
    }

    public static async Task<IResult> UpdateProductsV2(ISender Sender, Guid productId, [FromBody] CommandV2.Command.UpdateProductCommand updateProduct)
    {
        var updateProductCommand = new CommandV2.Command.UpdateProductCommand(
            productId,
            updateProduct.Name,
            updateProduct.Price,
            updateProduct.Description);

        var result = await Sender.Send(updateProductCommand);

        return Results.Ok(result);
    }

    public static async Task<IResult> DeleteProdductsV2(ISender Sender, Guid productId)
    {
        var deleteProductCommand = new CommandV2.Command.DeleteProductCommand(productId);

        var result = await Sender.Send(deleteProductCommand);

        return Results.Ok(result);
    }
    #endregion ========== V2 ==========
}
