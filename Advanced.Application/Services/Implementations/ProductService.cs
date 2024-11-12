using Advanced.Application.Dtos;
using Advanced.Application.Requests;
using Advanced.Application.Responses;
using Advanced.Application.Services.Interfaces;
using Advanced.Domain.Entities;
using Advanced.Domain.Repositories.Products;
using Advanced.Domain.Wrappers;
using Advanced.Persistence;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Advanced.Application.Services.Implementations;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly MongoDb<Product> _mongoDb;

    public ProductService(IProductRepository productRepository, MongoDb<Product> mongoDb)
    {
        _productRepository = productRepository;
        _mongoDb = mongoDb;
    }

    public async Task<IEnumerable<TDto>> GetAllAsync<TDto>()
        => await _productRepository.GetAllAsync<TDto>();

    public async Task<PaginatedResult<ProductResponse>> GetPagedProducts(PagedDto paged)
        => await _productRepository.GetPaginatedAsync<ProductResponse>(paged.PageNumber, paged.PageSize);

    public async Task<BaseResponse<string>> AddProduct(CreateProductRequest product)
    {
        try
        {
            await _productRepository.AddAsync(product);
        }
        catch (Exception e)
        {
            return new BaseResponse<string>()
            {
                Error = e.Message,
                HasData = false,
                Success = false
            };
        }
        return new BaseResponse<string>()
        {
            HasData = false,
            Success = true
        };
    }

    public async Task AddProducts<TDto>(List<TDto> products) 
        => await _productRepository.AddEntitiesAsync(products);
    
    public async Task<List<Product>> SearchAsync(string searchTerm, string category, decimal? minPrice, decimal? maxPrice, int page, int pageSize)
    {
        var filterBuilder = Builders<Product>.Filter;
        var filter = filterBuilder.Empty; // Start with an empty filter (no filters)

        if (!string.IsNullOrEmpty(searchTerm))
        {
            filter &= filterBuilder.Regex("Name", new BsonRegularExpression(searchTerm, "i")); // Case-insensitive search by name
        }

        if (!string.IsNullOrEmpty(category))
        {
            filter &= filterBuilder.Eq("Category", category); // Filter by category
        }

        if (minPrice.HasValue)
        {
            filter &= filterBuilder.Gte("Price", minPrice.Value); // Filter by minimum price
        }

        if (maxPrice.HasValue)
        {
            filter &= filterBuilder.Lte("Price", maxPrice.Value); // Filter by maximum price
        }
        
        var c = _mongoDb.GetCollection("Products");

        return await c.Find(filter)
            .Skip((page - 1) * pageSize)
            .Limit(pageSize)
            .ToListAsync();
    }
    
    public async Task<int> GetTotalCountAsync(string searchTerm, string category, decimal? minPrice, decimal? maxPrice)
    {
        var filterBuilder = Builders<Product>.Filter;
        var filter = filterBuilder.Empty;

        if (!string.IsNullOrEmpty(searchTerm))
        {
            filter &= filterBuilder.Regex("Name", new BsonRegularExpression(searchTerm, "i"));
        }

        if (!string.IsNullOrEmpty(category))
        {
            filter &= filterBuilder.Eq("Category", category);
        }

        if (minPrice.HasValue)
        {
            filter &= filterBuilder.Gte("Price", minPrice.Value);
        }

        if (maxPrice.HasValue)
        {
            filter &= filterBuilder.Lte("Price", maxPrice.Value);
        }

        var c = _mongoDb.GetCollection("Products");
        return (int)await c.CountDocumentsAsync(filter);
    }
    
    public async Task<(IEnumerable<Product> Products, int TotalCount)> GetFilteredProductsAsync(string searchTerm, string category, int pageNumber, int pageSize)
    {
        var filterBuilder = Builders<Product>.Filter;
        var filter = filterBuilder.Empty; // Start with no filter

        if (!string.IsNullOrEmpty(searchTerm))
        {
            filter &= filterBuilder.Text(searchTerm); // Text search for product name
        }

        if (!string.IsNullOrEmpty(category))
        {
            filter &= filterBuilder.Eq(p => p.Category, category); // Filter by category
        }

        var c = _mongoDb.GetCollection("Products");

        var totalCount = await c.CountDocumentsAsync(filter);

        var products = await c.Find(filter)
            .Skip((pageNumber - 1) * pageSize) // Skip items from previous pages
            .Limit(pageSize) // Limit the number of items per page
            .ToListAsync();

        return (products, (int)totalCount);
    }

}