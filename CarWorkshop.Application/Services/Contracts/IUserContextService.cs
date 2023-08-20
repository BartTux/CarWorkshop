using CarWorkshop.Application.Models;
using System.Security.Claims;

namespace CarWorkshop.Application.Services.Contracts;

public interface IUserContextService
{
    ClaimsPrincipal User { get; }
    string UserId { get; }
    string UserEmail { get; }
    IEnumerable<string> UserRoles { get; }

    CurrentUser? GetCurrentUser();
}