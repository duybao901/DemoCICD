namespace DemoCICD.Contract.Services.V2.Product;
public static class Response
{
    public record ProductResponse(Guid Id, string Name, decimal Price, string Description);
}
