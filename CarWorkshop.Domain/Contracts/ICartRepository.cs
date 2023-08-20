using CarWorkshop.Domain.Entities;

namespace CarWorkshop.Domain.Contracts;

public interface ICartRepository
{
    Task<List<CartService>> GetAllServices(string userId);
    Task<CartService?> GetServiceById(string userId, int serviceId);
    Task AddServiceToCart(CartService cartService);
    Task UpdateServiceQuantity(CartService service);
    Task DeleteServiceFromCart(CartService serviceCart);
    Task<bool> IsServiceInCart(string userId, int serviceId);
}
