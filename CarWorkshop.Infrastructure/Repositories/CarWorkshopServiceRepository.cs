using CarWorkshop.Domain.Contracts;
using CarWorkshop.Domain.Entities;
using CarWorkshop.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace CarWorkshop.Infrastructure.Repositories;

public class CarWorkshopServiceRepository : ICarWorkshopServiceRepository
{
    private readonly CarWorkshopDbContext _dbContext;

    public CarWorkshopServiceRepository(CarWorkshopDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<CarWorkshopService> GetById(int id)
        => await _dbContext.Services
            .FirstOrDefaultAsync(x => x.Id == id)
            ?? throw new Exception($"Cannot find service by id: { id }");
            

    public async Task<QueryResult<CarWorkshopService>> GetByEncodedName(string encodedName,
                                                                        int pageNumber,
                                                                        int pageSize)
    {
        var baseQuery = await _dbContext
            .Services
            .Where(x => x.CarWorkshop.EncodedName == encodedName)
            .ToListAsync();

        var totalCount = baseQuery.Count();

        var results = baseQuery
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        return new() { TotalCount = totalCount, Data = results };
    }

    public async Task Create(CarWorkshopService carWorkshopService)
    {
        await _dbContext.Services.AddAsync(carWorkshopService);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var service = await _dbContext.Services
            .FirstOrDefaultAsync(x => x.Id == id)
            ?? throw new Exception($"Cannot find service by id: { id }");

        _dbContext.Services.Attach(service);
        _dbContext.Services.Remove(service);

        await _dbContext.SaveChangesAsync();
    }

    public async Task Commit() => await _dbContext.SaveChangesAsync();
}
