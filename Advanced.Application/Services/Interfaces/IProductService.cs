using Advanced.Application.Dtos;
using Advanced.Application.Requests;
using Advanced.Application.Responses;
using Advanced.Domain.Entities;
using Advanced.Domain.Wrappers;

namespace Advanced.Application.Services.Interfaces;

public interface IProductService
{
    Task<IEnumerable<TDto>> GetAllAsync<TDto>();
    Task<PaginatedResult<ProductResponse>> GetPagedProducts(PagedDto paged);

    Task<BaseResponse<string>> AddProduct(CreateProductRequest product);
    Task AddProducts<TDto>(List<TDto> products);

    Task<List<Product>> SearchAsync(string searchTerm, string category, decimal? minPrice, decimal? maxPrice, int page,
        int pageSize);

    Task<int> GetTotalCountAsync(string searchTerm, string category, decimal? minPrice, decimal? maxPrice);

    Task<(IEnumerable<Product> Products, int TotalCount)> GetFilteredProductsAsync(string searchTerm, string category,
        int pageNumber, int pageSize);
}