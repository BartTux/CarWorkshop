using CarWorkshop.Application.Models;

namespace CarWorkshop.Application.Services;

public interface IUserContextService
{
    CurrentUser? GetCurrentUser();
}