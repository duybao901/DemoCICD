using DemoCICD.Contract.Enumerations;
using MediatR;

namespace DemoCICD.Contract.Extentions;
public static class ProductExtention
{
    public static string GetSortProductProperty(string sortColumn)
    {
        return sortColumn.ToLower() switch
        {
            "name" => "Name",
            "description" => "Description",
            "price" => "Price",
            _ => "Id"
            // REAL: product => product.CreatedDate by default
        };
    }
}
