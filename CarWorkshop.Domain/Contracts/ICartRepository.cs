using CarWorkshop.Domain.Entities;

namespace CarWorkshop.Domain.Contracts;

public interface ICartRepository
{
    Task<Cart> GetCartWithServicesForUser(string userId);
}
