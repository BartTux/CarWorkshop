namespace CarWorkshop.Domain.Entities;

public class QueryResult<T>
{
    public int TotalCount;
    public List<T> Data { get; set; } = new();
}
