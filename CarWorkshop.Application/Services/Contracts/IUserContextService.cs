using CarWorkshop.Application.Models;
using System.Security.Claims;

namespace CarWorkshop.Application.Services.Contracts;

public interface IUserContextService
{
    public ClaimsPrincipal User { get; }
    public int UserId { get; }

    CurrentUser? GetCurrentUser();
}