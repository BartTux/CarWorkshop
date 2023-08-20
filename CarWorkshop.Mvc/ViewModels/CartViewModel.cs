using CarWorkshop.Application.Models;

namespace CarWorkshop.Mvc.ViewModels;

public class CartViewModel
{
    public required List<CartServiceDTO> CartServices { get; set; } = new();

    private decimal _totalServicesCost;
    public decimal TotalServicesCost 
    {
        get 
        {
            CalculateTotalServiceCost();
            return _totalServicesCost;
        }
    }

    private void CalculateTotalServiceCost() => 
        CartServices.ForEach(cs => _totalServicesCost += cs.TotalCost);
}
