using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoCICD.Application.UserCases.V1.Queries.Product;
public class GetProductResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
}
