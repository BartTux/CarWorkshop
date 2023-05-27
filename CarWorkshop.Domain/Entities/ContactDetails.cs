namespace CarWorkshop.Domain.Entities;

public class ContactDetails
{
    public string PhoneNumber { get; set; } = default!;
    public string? City { get; set; }
    public string? Street { get; set; }
    public string? PostalCode { get; set; }
}
