namespace CarWorkshop.Application.Models;

public record QueryRequest
{
    public string? SearchPhrase { get; set; }
    public int PageSize { get; set; } = 5;
    public int PageNumber { get; set; } = 1;
}
