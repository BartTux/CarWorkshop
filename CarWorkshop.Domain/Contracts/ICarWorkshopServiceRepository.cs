using CarWorkshop.Domain.Entities;

namespace CarWorkshop.Domain.Contracts;

public interface ICarWorkshopServiceRepository
{
    Task<CarWorkshopService> GetById(int id);
    Task<QueryResult<CarWorkshopService>> GetByEncodedName(string encodedName,
                                                           int pageNumber,
                                                           int pageSize);
    Task Create(CarWorkshopService carWorkshopService);
    Task Delete(int id);
    Task Commit();
}
