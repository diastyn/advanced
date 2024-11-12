namespace Advanced.Domain.Wrappers;

public class BaseResponse<T>
{
    public bool HasData { get; set; }
    public string? Error { get; set; }
    public bool Success { get; set; }
    public T? Data { get; set; }
}