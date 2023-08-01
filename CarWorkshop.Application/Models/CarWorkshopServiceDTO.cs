namespace CarWorkshop.Application.Models;

public record CarWorkshopServiceDTO
{
    public int Id { get; set; }
    public string Description { get; set; } = default!;
    public string? DetailedDescription { get; set; }
    public string Category { get; set; } = default!;
    public decimal Cost { get; set; }
}
