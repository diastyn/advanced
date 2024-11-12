using Advanced.Application.Dtos;
using Advanced.Application.Requests;
using Advanced.Application.Responses;
using Advanced.Domain.Entities;
using AutoMapper;

namespace Advanced.Application.Mappings;

public sealed class ProductMapping : Profile
{
    public ProductMapping()
    {
        CreateProductMapping();
        ResponseProductMapping();
        ProductDtoMapping();
    }

    private void CreateProductMapping()
    {
        CreateMap<CreateProductRequest, Product>();
    }

    private void ResponseProductMapping()
    {
        CreateMap<Product, ProductResponse>();
    }
    
    private void ProductDtoMapping()
    {
        CreateMap<Product, ProductDto>();
    }
    
    private void ProductsMapping()
    {
        CreateMap<List<Product>, List<ProductDto>>();
    }
}