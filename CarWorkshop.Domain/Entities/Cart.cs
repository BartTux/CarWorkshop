using Microsoft.AspNetCore.Identity;

namespace CarWorkshop.Domain.Entities;

public class Cart
{
    public int Id { get; set; }

    public string AddedById { get; set; } = default!;
    public virtual IdentityUser? AddedBy { get; set; }

    public virtual List<CarWorkshopServiceCart> ServiceCarts { get; set; } = new();
}
