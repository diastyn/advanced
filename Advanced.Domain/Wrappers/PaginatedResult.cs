namespace Advanced.Domain.Wrappers;

public class PaginatedResult<T>
{
    public List<T>? Data { get; set; }
    public long TotalCount { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
}