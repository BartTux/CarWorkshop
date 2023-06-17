namespace CarWorkshop.Application.Models;

public record QueryResponse<T>
{
    public List<T> Data { get; set; } = new();
    public int TotalCount { get; set; }

    public string? SearchPhrase { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }

    public int TotalPages => Convert.ToInt32(Math.Ceiling((double) TotalCount / PageSize));
    public bool HasNextPage => PageNumber < TotalPages;
    public bool HasPreviousPage => PageNumber > 1;
    public int? NextPage => HasNextPage ? PageNumber + 1 : null;
    public int? PreviousPage => HasPreviousPage ? PageNumber - 1 : null;
}
