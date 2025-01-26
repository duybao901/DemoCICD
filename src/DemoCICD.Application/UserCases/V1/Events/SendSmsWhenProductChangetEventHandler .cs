using DemoCICD.Contract.Abstractions.Message;
using DemoCICD.Contract.Services.V1.Product;

namespace DemoCICD.Application.UserCases.V1.Events;
public class SendSmaWhenProductCreatedEventHandler :
    IDomainEventHandler<DomainEvent.ProductCreated>,
    IDomainEventHandler<DomainEvent.ProductDeleted>
{
    public async Task Handle(DomainEvent.ProductCreated notification, CancellationToken cancellationToken)
    {
        await Task.Delay(5000, cancellationToken);
        await SendSms();
    }

    public async Task Handle(DomainEvent.ProductDeleted notification, CancellationToken cancellationToken)
    {
        await Task.Delay(5000, cancellationToken);
        await SendSms();
    }

    private static async Task SendSms()
    {
        // Send email...
    }
}
