namespace CarWorkshop.Domain.Entities;

public class CarWorkshopServiceCart
{
    public int CartId { get; set; }
    public virtual Cart Cart { get; set; } = default!;

    public int CarWorkshopServiceId { get; set; }
    public virtual CarWorkshopService CarWorkshopService { get; set; } = default!;

    public int Quantity { get; set; }
}
