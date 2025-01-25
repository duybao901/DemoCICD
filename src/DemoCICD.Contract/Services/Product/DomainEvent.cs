using DemoCICD.Contract.Abstractions.Message;

namespace DemoCICD.Contract.Services.Product;
public class DomainEvent
{
    public record ProductCreated(Guid Id)
        : IDomainEvent;

    public record ProductUpdated(Guid Id)
        : IDomainEvent;

    public record ProductDeleted(Guid Id)
        : IDomainEvent;
}
