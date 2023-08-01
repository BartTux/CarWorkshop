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
            ?? throw new Exception("No services detected");
}
