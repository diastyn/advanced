namespace Advanced.Middlewares;

public class AuthMiddleware
{
    private readonly RequestDelegate _next;

    public AuthMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (string.IsNullOrEmpty(context.Request.Headers.Authorization))
        {
            if (context.Request.Cookies.TryGetValue("AuthToken", out var token))
            {
                context.Request.Headers.Authorization = "Bearer " + token;
            }
        }
        
        Console.WriteLine("Authorization Header: " + context.Request.Headers.Authorization);
        await _next(context);
    }
}