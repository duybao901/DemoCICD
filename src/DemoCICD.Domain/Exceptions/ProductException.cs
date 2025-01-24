namespace DemoCICD.Domain.Exceptions;
public class ProductException
{
    public class ProductNotFoundException : NotFoundException
    {
        public ProductNotFoundException(Guid id)
            : base($"The product with the id {id} was not found")
        {
        }
    }
}
