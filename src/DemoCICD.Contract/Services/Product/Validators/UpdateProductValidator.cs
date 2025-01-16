using FluentValidation;

namespace DemoCICD.Contract.Services.Product.Validators;
public class UpdateProductValidator : AbstractValidator<Command.UpdateProduct>
{
    public UpdateProductValidator()
    {
        RuleFor(product => product.Id).NotEmpty().NotNull();
        RuleFor(product => product.Name).NotEmpty();
        RuleFor(product => product.Price).GreaterThan(0);
        RuleFor(product => product.Description).NotEmpty();
    }
}
