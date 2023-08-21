using CarWorkshop.Application.Models;
using CarWorkshop.Application.Services.Contracts;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace CarWorkshop.Application.Services;

public class UserContextService : IUserContextService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly HttpContext _httpContext;

    public ClaimsPrincipal User 
        => _httpContext.User.Identity is not null && _httpContext.User.Identity.IsAuthenticated
            ? _httpContext.User
            : throw new InvalidOperationException("User is not authenticated");

    public string UserId => FindClaim(ClaimTypes.NameIdentifier)
        ?? throw new InvalidOperationException("User id not found in user context");

    public string UserEmail => FindClaim(ClaimTypes.Email)
        ?? throw new InvalidOperationException("User email not found in user context");

    public IEnumerable<string> UserRoles => User.Claims
        .Where(claim => claim.Type is ClaimTypes.Role)
        .Select(claim => claim.Value);

    public UserContextService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;

        _httpContext = _httpContextAccessor.HttpContext
            ?? throw new InvalidOperationException("Http context is not represented");
    }

    public CurrentUser? GetCurrentUser() => new(UserId, UserEmail, UserRoles);

    private string? FindClaim(string claimType) 
        => User.FindFirst(claim => claim.Type == claimType)?.Value;
}
