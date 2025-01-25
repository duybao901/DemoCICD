using FluentValidation;

namespace DemoCICD.Contract.Services.Product.Validators;
public class DeleteProductValidator : AbstractValidator<Command.DeleteProductCommand>
{
    public DeleteProductValidator()
    {
        RuleFor(product => product.Id).NotEmpty().NotNull();
    }
}
