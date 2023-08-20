namespace CarWorkshop.Application.Models;

public record CartServiceDTO
{
    public string UserId { get; set; } = default!;
    public int CarWorkshopServiceId { get; set; }

    public string Description { get; set; } = default!;
    public decimal Cost { get; set; }
    public int Quantity { get; set; }
    public decimal TotalCost { get; set; }
}