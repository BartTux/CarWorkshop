namespace CarWorkshop.Application.Models;

public record CartDTO
{
    public int Id { get; set; }
    public List<CartServiceDTO> Services { get; set; } = new();

    public decimal TotalSumCost { get; set; }
}
