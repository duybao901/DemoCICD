using DemoCICD.Contract.Abstractions.Message;
using DemoCICD.Contract.Services.Product;

namespace DemoCICD.Application.UserCases.V1.Events;
public class SendEmailWhenProductChangeEventHandler :
    IDomainEventHandler<DomainEvent.ProductCreated>,
    IDomainEventHandler<DomainEvent.ProductDeleted>
{
    public async Task Handle(DomainEvent.ProductCreated notification, CancellationToken cancellationToken)
    {
        await Task.Delay(5000, cancellationToken);
        await SendEmail();
    }

    public async Task Handle(DomainEvent.ProductDeleted notification, CancellationToken cancellationToken)
    {
        await Task.Delay(5000, cancellationToken);
        await SendEmail();
    }

    private static async Task SendEmail()
    {
        // Send email...
    }
}
