using Microsoft.AspNetCore.Identity;

namespace CarWorkshop.Domain.Entities;

public class CarWorkshop
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public string? About { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public string? CreatedById { get; set; } = default!;
    public IdentityUser? CreatedBy { get; set; }

    public ContactDetails ContactDetails { get; set; } = default!;

    public string EncodedName { get; private set; } = default!;

    public List<CarWorkshopService> Services { get; set; } = new();

    public void EncodeName()
        => EncodedName = Name.ToLower().Replace(" ", "-");
}
