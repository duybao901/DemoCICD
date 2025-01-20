using FluentValidation;

namespace DemoCICD.Contract.Services.Product.Validators;
public class GetProductByIdValidator : AbstractValidator<Query.GetProductById>
{
    public GetProductByIdValidator()
    {
        RuleFor(product => product.Id).NotEmpty();
    }
}
