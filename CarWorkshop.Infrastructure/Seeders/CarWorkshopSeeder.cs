using CarWorkshop.Infrastructure.Persistence;

namespace CarWorkshop.Infrastructure.Seeders;

public class CarWorkshopSeeder
{
    private readonly CarWorkshopDbContext _dbContext;

    public CarWorkshopSeeder(CarWorkshopDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Seed()
    {
        if (!await _dbContext.Database.CanConnectAsync()) return;
        if (_dbContext.CarWorkshops.Any()) return;

        var mazdaAso = new Domain.Entities.CarWorkshop
        {
            Name = "Mazda ASO",
            Description = "Autoryzowany serwis Mazda Aso",
            About = "O serwisie autoryzowanym",
            ContactDetails = new()
            {
                PhoneNumber = "123657895",
                City = "Kraków",
                Street = "Gnojowa 54/2",
                PostalCode = "30-001"
            },
        };

        mazdaAso.EncodeName();

        await _dbContext.CarWorkshops.AddAsync(mazdaAso);
        await _dbContext.SaveChangesAsync();
    }
}
