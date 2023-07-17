using CarWorkshop.Domain.Contracts;
using CarWorkshop.Domain.Entities;
using CarWorkshop.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Text;

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
            .Include(s => s.CarWorkshop)
            .FirstOrDefaultAsync(x => x.Id == id)
            ?? throw new Exception($"Cannot find service by id: { id }");
            

    public async Task<QueryResult<CarWorkshopService>> GetByEncodedName(string encodedName,
                                                                        string? searchPhrase,
                                                                        int pageNumber,
                                                                        int pageSize)
    {
        var baseQuery = _dbContext.Services
            .Where(x => x.CarWorkshop.EncodedName == encodedName);
            
        if (!string.IsNullOrEmpty(searchPhrase))
            baseQuery = baseQuery.Where(x => x.Description.Contains(searchPhrase));

        await baseQuery.ToListAsync();

        var totalCount = baseQuery.Count();
        pageNumber 
            = Math.Ceiling(totalCount / (double)pageSize) >= pageNumber ? pageNumber : 1;

        var results = await baseQuery
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new() { TotalCount = totalCount, PageNumber = pageNumber, Data = results };
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
