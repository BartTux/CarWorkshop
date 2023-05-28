using CarWorkshop.Application.Models;

namespace CarWorkshop.Mvc.ViewModels;

public class CarWorkshopServicesViewModel
{
    public string EncodedName { get; set; } = default!;

    public QueryResponse<CarWorkshopServiceDTO> QueryResult { get; set; } = default!; 
}
