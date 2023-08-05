using CarWorkshop.Domain.Contracts;
using CarWorkshop.Domain.Entities;
using CarWorkshop.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CarWorkshop.Infrastructure.Repositories;

public class CartRepository : ICartRepository
{
    private readonly CarWorkshopDbContext _dbContext;

    public CartRepository(CarWorkshopDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Cart> GetCartWithServicesForUser(string userId)
        => await _dbContext.Cart
            .Include(c => c.ServiceCarts)
                .ThenInclude(sc => sc.CarWorkshopService)
            .FirstOrDefaultAsync(c => c.AddedById == userId)
            ?? throw new Exception("No cart detected");

    public async Task<CarWorkshopServiceCart> GetServiceForCart(int cartId, int serviceId)
        => await _dbContext.ServiceCarts
            .FirstOrDefaultAsync(sc => sc.CartId == cartId && sc.CarWorkshopServiceId == serviceId)
            ?? throw new Exception("No cart service detected");

    public async Task UpdateServiceQuantity(CarWorkshopServiceCart service)
    {
        _dbContext.ServiceCarts.Update(service);
        await _dbContext.SaveChangesAsync();
    }
}
