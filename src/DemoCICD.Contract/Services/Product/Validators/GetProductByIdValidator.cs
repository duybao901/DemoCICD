using FluentValidation;

namespace DemoCICD.Contract.Services.Product.Validators;
public class GetProductByIdValidator : AbstractValidator<Query.GetProductByIdQuery>
{
    public GetProductByIdValidator()
    {
        RuleFor(product => product.Id).NotEmpty();
    }
}
