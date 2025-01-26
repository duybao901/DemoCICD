using FluentValidation;

namespace DemoCICD.Contract.Services.V2.Product.Validators;
public class GetProductByIdValidator : AbstractValidator<Query.GetProductByIdQuery>
{
    public GetProductByIdValidator()
    {
        RuleFor(product => product.Id).NotEmpty();
    }
}
