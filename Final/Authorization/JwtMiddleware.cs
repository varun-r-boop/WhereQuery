namespace Final.Authorization;

using Final.Data;
using Final.Services;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;

    public JwtMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, IAuthService customerService, IJwtUtils jwtUtils, HomezillaContext _context)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        var userId = jwtUtils.ValidateToken(token);
        if (userId != null)
        {
            Console.WriteLine(userId);
            // attach user to context on successful jwt validation
            context.Items["User"] = await _context.customer.FindAsync(userId);  
        }

        await _next(context);
    }

}