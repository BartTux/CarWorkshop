namespace CarWorkshop.Application.Models;

public record CartServiceDTO
{
    public int CartId { get; set; }
    public int CarWorkshopServiceId { get; set; }

    public string Description { get; set; } = default!;
    public decimal Cost { get; set; }
    public int Quantity { get; set; }
    public decimal TotalCost { get; set; }
}