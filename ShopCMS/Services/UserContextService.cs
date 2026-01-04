using Microsoft.AspNetCore.Http;
using ShopCMS.Application.Interfaces.UserContext_Service;
using System.Security.Claims;

namespace ShopCMS.Services;

public sealed class UserContextService : IUserContextService
{
    private readonly IHttpContextAccessor _http;

    public UserContextService(IHttpContextAccessor http)
    {
        _http = http;
    }

    private ClaimsPrincipal? User => _http.HttpContext?.User;

    public bool IsAuthenticated => User?.Identity?.IsAuthenticated == true;

    public int? UserId
    {
        get
        {
            // JWT: sub
            var sub = User?.FindFirstValue("sub")
                   ?? User?.FindFirstValue(ClaimTypes.NameIdentifier);

            return int.TryParse(sub, out var id) ? id : null;
        }
    }

    public string? Username
        => User?.FindFirstValue("unique_name")
        ?? User?.FindFirstValue(ClaimTypes.Name);

    public string? Role
        => User?.FindFirstValue(ClaimTypes.Role);
}
