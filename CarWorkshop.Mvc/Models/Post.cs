namespace CarWorkshop.Mvc.Models;

public class Post
{
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string[] Tags { get; set; } = default!;
}
