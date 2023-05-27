namespace CarWorkshop.Application.Models;

public record QueryRequest
{
    public int PageSize { get; set; } = 5;
    public int PageNumber { get; set; } = 1;
}
