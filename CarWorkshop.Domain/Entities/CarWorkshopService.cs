namespace CarWorkshop.Domain.Entities;

public class CarWorkshopService
{
    public int Id { get; set; }
    public string Description { get; set; } = default!;
    public string? DetailedDescription { get; set; }
    public string Category { get; set; } = default!;
    public decimal Cost { get; set; }

    public int CarWorkshopId { get; set; }
    public CarWorkshop CarWorkshop { get; set; } = default!;
}
