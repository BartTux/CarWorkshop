using CarWorkshop.Domain.Contracts;
using CarWorkshop.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CarWorkshop.Infrastructure.Repositories;

public class CarWorkshopRepository : ICarWorkshopRepository
{
    private readonly CarWorkshopDbContext _dbContext;

    public CarWorkshopRepository(CarWorkshopDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Domain.Entities.CarWorkshop>> GetAll()
        => await _dbContext.CarWorkshops.ToListAsync();

    public async Task<Domain.Entities.CarWorkshop> GetById(int id)
        => await _dbContext.CarWorkshops
            .FirstOrDefaultAsync(x => x.Id == id)
            ?? throw new Exception($"Cannot find car workshop by id: { id }");

    public async Task<Domain.Entities.CarWorkshop?> GetByName(string name)
        => await _dbContext.CarWorkshops
            .FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower());

    public async Task<Domain.Entities.CarWorkshop> GetByEncodedName(string encodedName)
        => await _dbContext.CarWorkshops
            .FirstOrDefaultAsync(x => x.EncodedName == encodedName)
            ?? throw new Exception();

    public async Task Create(Domain.Entities.CarWorkshop carWorkshop)
    {
        if (carWorkshop is null) throw new Exception();

        await _dbContext.AddAsync(carWorkshop);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Commit() => await _dbContext.SaveChangesAsync();
}