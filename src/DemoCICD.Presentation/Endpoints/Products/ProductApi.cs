using Asp.Versioning.Builder;
using DemoCICD.Contract.Abstractions.Shared;
using DemoCICD.Contract.Extentions;
using DemoCICD.Contract.Services.V1.Product;
using DemoCICD.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoCICD.Presentation.Endpoints.Products;
public class ProductApi : ApiEnpoint
{
    private const string BaseUrl = "/api/minimal/v{version:apiVersion}/products";

    public void MapProductApiV1(IVersionedEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup(BaseUrl).HasApiVersion(1);

        group.MapPost(string.Empty, CreateProducts);
        group.MapGet(string.Empty, GetProducts);
        group.MapGet("{productId}", GetProductsById);
        group.MapPut("{productId}", UpdateProducts);
        group.MapDelete("{productId}", DeleteProdducts);
    }

    public void MapProductApiV2(IVersionedEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup(BaseUrl).HasApiVersion(2);

        // !TODO move to V2: CreateProductsV2, GetProductsV2, GetProductsByIdV2, UpdateProductsV2, DeleteProdductsV2...
        group.MapPost(string.Empty, CreateProducts);
        group.MapGet(string.Empty, GetProducts);
    }

    public static async Task<IResult> GetProducts(ISender Sender,
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
        return Results.Ok(result);
    }

    public static async Task<IResult> CreateProducts(ISender Sender, [FromBody] Command.CreateProductCommand CreateProduct)
    {
        var result = await Sender.Send(CreateProduct);

        if (result.IsFailure)
        {
            return HandleFailureReturn(result);
        }

        return Results.Ok(result);
    }

    public static async Task<IResult> GetProductsById(ISender Sender, Guid productId)
    {
        Result<Response.ProductResponse> result = await Sender.Send(new Query.GetProductByIdQuery(productId));
        return Results.Ok(result);
    }

    public static async Task<IResult> UpdateProducts(ISender Sender, Guid productId, [FromBody] Command.UpdateProductCommand updateProduct)
    {
        var updateProductCommand = new Command.UpdateProductCommand(
            productId,
            updateProduct.Name,
            updateProduct.Price,
            updateProduct.Description);

        var result = await Sender.Send(updateProductCommand);

        return Results.Ok(result);
    }

    public static async Task<IResult> DeleteProdducts(ISender Sender, Guid productId)
    {
        var deleteProductCommand = new Command.DeleteProductCommand(productId);

        var result = await Sender.Send(deleteProductCommand);

        return Results.Ok(result);
    }
}
