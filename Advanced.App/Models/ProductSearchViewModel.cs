using Advanced.Domain.Entities;

namespace Advanced.Models;

public class ProductSearchViewModel
{
    public string SearchTerm { get; set; }
    public string Category { get; set; }
    public IEnumerable<Product> Products { get; set; }
    public int TotalCount { get; set; } // Total number of products that match the search
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize); // Calculate total pages
}