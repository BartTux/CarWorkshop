using CarWorkshop.Application.Models;
using System.Security.Claims;

namespace CarWorkshop.Application.Services.Contracts;

public interface IUserContextService
{
    public ClaimsPrincipal User { get; }
    public string UserId { get; }
    public string UserEmail { get; }
    public IEnumerable<string> UserRoles { get; }

    CurrentUser? GetCurrentUser();
}