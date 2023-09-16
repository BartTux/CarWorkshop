using CarWorkshop.Application.Models;

namespace CarWorkshop.Mvc.ViewModels;

public class CarWorkshopViewModel
{
    public required List<CarWorkshopDTO> CarWorkshops { get; set; } = new();

    public List<string?> Cities { get => RemoveDuplicates(); }

    private List<string?> RemoveDuplicates() => CarWorkshops
        .Select(cw => cw.City)
        .Distinct()
        .Order()
        .ToList();
}
