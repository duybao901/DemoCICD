using FluentValidation;

namespace DemoCICD.Contract.Services.Product.Validators;
public class CreateProductValidator : AbstractValidator<Command.CreateProduct>
{
    public CreateProductValidator()
    {
        RuleFor(product => product.Name).NotEmpty();
        RuleFor(product => product.Price).GreaterThan(0);
        RuleFor(product => product.Description).NotEmpty();
    }
}
