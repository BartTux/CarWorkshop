using CarWorkshop.Application.Models;

namespace CarWorkshop.Application.Services.Contracts;

public interface IUserContextService
{
    CurrentUser? GetCurrentUser();
}