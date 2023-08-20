using CarWorkshop.Application.CQRS.Carts.Commands;
using CarWorkshop.Application.Services.Contracts;
using CarWorkshop.Domain.Contracts;
using CarWorkshop.Domain.Entities;
using MediatR;

namespace CarWorkshop.Application.CQRS.Carts.CommandHandlers;

public class AddServiceToCartCommandHandler : IRequestHandler<AddServiceToCartCommand>
{
    private readonly IUserContextService _userContextService;
    private readonly ICartRepository _repository;

    public AddServiceToCartCommandHandler(IUserContextService userContextService,
                                          ICartRepository repository)
    {
        _userContextService = userContextService;
        _repository = repository;
    }

    public async Task Handle(AddServiceToCartCommand request,
                             CancellationToken cancellationToken)
    {
        var cartService = new CartService
        {
            AddedById = _userContextService.UserId,
            CarWorkshopServiceId = request.CarWorkshopServiceId,
            Quantity = 1
        };

        await _repository.AddServiceToCart(cartService);
    }
}
