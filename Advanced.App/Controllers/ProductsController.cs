using Advanced.Application.Dtos;
using Advanced.Application.Requests;
using Advanced.Application.Responses;
using Advanced.Application.Services.Interfaces;
using Advanced.Models;
using Microsoft.AspNetCore.Mvc;

namespace Advanced.Controllers;

public class ProductsController : Controller
{
    private readonly IProductService _productService;
    private const int PageSize = 10;
    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }
    
    [HttpPost]
    [Route("add-product")]
    public async Task<IActionResult> AddProduct([FromBody]CreateProductRequest product) 
        => Ok(await _productService.AddProduct(product));
    
    [HttpGet]
    [Route("all-products")]
    public async Task<IActionResult> GetPaginatedProducts([FromQuery] PagedDto pagedDto)
        => Ok(await _productService.GetPagedProducts(pagedDto));

    [HttpGet("products")]
    public async Task<IActionResult> Product()
    {
        var products = await _productService.GetAllAsync<ProductResponse>();
        return View(products);
    }

    [HttpPost("add-products")]
    public async Task<IActionResult> AddProducts([FromBody] List<CreateProductRequest> products)
    {
        await _productService.AddProducts(products);
        return Created();
    }

    [HttpGet("paginated")]
    public async Task<IActionResult> Index([FromQuery] PagedDto pagedDto)
        => View(await _productService.GetPagedProducts(pagedDto));

    [HttpGet("index")]
    public async Task<IActionResult> Home() 
        => View(await _productService.GetAllAsync<ProductResponse>());
    
    [HttpGet]
    public async Task<IActionResult> Details() 
        => View(await _productService.GetAllAsync<ProductResponse>());

    [HttpGet("search")]
    public async Task<IActionResult> Search(string searchTerm, string category, int pageNumber = 1)
    {
        var (products, totalCount) = await _productService.GetFilteredProductsAsync(searchTerm, category, pageNumber, PageSize);

        var model = new ProductSearchViewModel
        {
            SearchTerm = searchTerm,
            Category = category,
            Products = products,
            TotalCount = totalCount,
            PageNumber = pageNumber,
            PageSize = PageSize
        };
        return View(model);
    }
}
