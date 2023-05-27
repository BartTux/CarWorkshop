namespace CarWorkshop.Application.Models;

public record CarWorkshopServiceDTO
{
    public int Id { get; set; }
    public string Description { get; set; } = default!;
    public decimal Cost { get; set; }
}
