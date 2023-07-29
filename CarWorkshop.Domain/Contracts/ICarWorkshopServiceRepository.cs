using CarWorkshop.Domain.Entities;

namespace CarWorkshop.Domain.Contracts;

public interface ICarWorkshopServiceRepository
{
    Task<CarWorkshopService> GetById(int id);
    Task<CarWorkshopService?> GetByDescription(string description);
    Task<QueryResult<CarWorkshopService>> GetByEncodedName(string encodedName,
                                                           string? searchPhrase,
                                                           int pageNumber,
                                                           int pageSize);
    Task Create(CarWorkshopService carWorkshopService);
    Task Delete(int id);
    Task Commit();
}
