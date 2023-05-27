namespace CarWorkshop.Application.Models;

public record CarWorkshopDTO
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public string? About { get; set; }
    public string PhoneNumber { get; set; } = default!;
    public string? City { get; set; }
    public string? Street { get; set; }
    public string? PostalCode { get; set; }

    public string EncodedName { get; set; } = default!;
    public bool IsEditable { get; set; }
}
