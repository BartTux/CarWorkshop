namespace CarWorkshop.Domain.Entities;

public class QueryResult<T>
{
    public int TotalCount { get; set; }
    public int PageNumber { get; set; }
    public List<T> Data { get; set; } = new();
}
