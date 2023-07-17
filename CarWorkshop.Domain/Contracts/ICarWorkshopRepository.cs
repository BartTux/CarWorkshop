namespace CarWorkshop.Domain.Contracts;

public interface ICarWorkshopRepository
{
    Task<IEnumerable<Entities.CarWorkshop>> GetAll();
    Task<Entities.CarWorkshop> GetById(int id);
    Task<Entities.CarWorkshop?> GetByName(string name);
    Task<Entities.CarWorkshop> GetByEncodedName(string encodedName);
    Task Create(Entities.CarWorkshop carWorkshop);
    Task Commit();
}
