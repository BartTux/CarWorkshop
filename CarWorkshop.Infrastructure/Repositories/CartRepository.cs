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

    public async Task<List<CartService>> GetAllServices(string userId)
        => await _dbContext.CartServices
            .Include(cs => cs.CarWorkshopService)
            .Where(cs => cs.AddedById == userId)
            .ToListAsync();

    public async Task<CartService?> GetServiceById(string userId, int serviceId)
        => await _dbContext.CartServices
            .Include(cs => cs.CarWorkshopService)
            .FirstOrDefaultAsync(cs => cs.AddedById == userId && cs.CarWorkshopServiceId == serviceId);

    public async Task AddServiceToCart(CartService cartService)
    {
        await _dbContext.CartServices.AddAsync(cartService);
        await SaveChangesAsync();
    }

    public async Task UpdateServiceQuantity(CartService service)
    {
        _dbContext.CartServices.Update(service);
        await SaveChangesAsync();
    }

    public async Task DeleteServiceFromCart(CartService serviceCart)
    {
        _dbContext.CartServices.Remove(serviceCart);
        await SaveChangesAsync();
    }

    public async Task SaveChangesAsync() => await _dbContext.SaveChangesAsync();

    public async Task<bool> IsServiceInCart(string userId, int serviceId)
        => await _dbContext.CartServices
            .AnyAsync(cs => cs.AddedById == userId && cs.CarWorkshopServiceId == serviceId);
}
