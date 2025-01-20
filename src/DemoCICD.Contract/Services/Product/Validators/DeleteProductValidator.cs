using FluentValidation;

namespace DemoCICD.Contract.Services.Product.Validators;
public class DeleteProductValidator : AbstractValidator<Command.DeleteProduct>
{
    public DeleteProductValidator()
    {
        RuleFor(product => product.Id).NotEmpty().NotNull();
    }
}
