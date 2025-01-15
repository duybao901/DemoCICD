﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace DemoCICD.Application.UserCases.V1.Queries.Product;
public class GetProductQuery : IRequest<GetProductResponse>
{
    public string Name { get; set; }
    public double Price { get; set; }
}
