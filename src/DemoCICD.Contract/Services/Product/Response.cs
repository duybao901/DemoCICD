namespace DemoCICD.Contract.Services.Product;
public static class Response
{
    public record ProductResponse(Guid Id, string Name, decimal Price, string Description);
}
