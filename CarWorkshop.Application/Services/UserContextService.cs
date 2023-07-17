using CarWorkshop.Application.Models;
using CarWorkshop.Application.Services.Contracts;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace CarWorkshop.Application.Services;

public class UserContextService : IUserContextService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly HttpContext _httpContext;

    public ClaimsPrincipal User => _httpContext.User
        ?? throw new InvalidOperationException("User context is not represented");

    public int UserId 
        => int.Parse(User.FindFirst(claim => claim.Type == ClaimTypes.NameIdentifier)!.Value);

    public UserContextService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;

        _httpContext = _httpContextAccessor.HttpContext
            ?? throw new InvalidOperationException("Http context is not represented");
    }

    public CurrentUser? GetCurrentUser()
    {
        var user = _httpContextAccessor?.HttpContext?.User
            ?? throw new InvalidOperationException("User context is not represented");

        if (user.Identity is null || !user.Identity.IsAuthenticated) return null;

        var id = user.FindFirst(claim => claim.Type is ClaimTypes.NameIdentifier)!.Value;
        var email = user.FindFirst(claim => claim.Type is ClaimTypes.Email)!.Value;

        var roles = user.Claims
            .Where(claim => claim.Type is ClaimTypes.Role)
            .Select(claim => claim.Value);

        return new(id, email, roles);
    }
}
