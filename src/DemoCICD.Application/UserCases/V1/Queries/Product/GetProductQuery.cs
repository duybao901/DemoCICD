using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemoCICD.Application.Abstractions;
using MediatR;

namespace DemoCICD.Application.UserCases.V1.Queries.Product;
public class GetProductQuery : IQuery<GetProductResponse>
{
    public string Name { get; set; }
    public double Price { get; set; }
}
