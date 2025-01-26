using AutoMapper;
using DemoCICD.Contract.Abstractions.Shared;
using DemoCICD.Contract.Services.V1.Product;
using DemoCICD.Domain.Entities;

namespace DemoCICD.Application.Mapper;
public class ServiceProfile : Profile
{
    public ServiceProfile()
    {
        CreateMap<Product, Response.ProductResponse>().ReverseMap();
        CreateMap<PageResult<Product>, PageResult<Response.ProductResponse>>().ReverseMap();
    }
}
