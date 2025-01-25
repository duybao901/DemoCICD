using MediatR;

namespace DemoCICD.Contract.Abstractions.Message;
public interface IDomainEvent : INotification
{
    public Guid Id { get; init; }
}
