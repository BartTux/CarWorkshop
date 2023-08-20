using Microsoft.AspNetCore.Identity;

namespace CarWorkshop.Domain.Entities;

public class CartService
{
    public string AddedById { get; set; } = default!;
    public virtual IdentityUser? AddedBy { get; set; }

    public int CarWorkshopServiceId { get; set; }
    public virtual CarWorkshopService CarWorkshopService { get; set; } = default!;

    public int Quantity { get; set; }
}
