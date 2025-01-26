using FluentValidation;

namespace DemoCICD.Contract.Services.V2.Product.Validators;
public class DeleteProductValidator : AbstractValidator<Command.DeleteProductCommand>
{
    public DeleteProductValidator()
    {
        RuleFor(product => product.Id).NotEmpty().NotNull();
    }
}
