using CarWorkshop.Application.CQRS.Carts.Commands;
using CarWorkshop.Domain.Contracts;
using MediatR;

namespace CarWorkshop.Application.CQRS.Carts.CommandHandlers;

public class IncreaseCartServiceCommandHandler : IRequestHandler<IncreaseCartServiceCommand>
{
    private readonly ICartRepository _repository;

    public IncreaseCartServiceCommandHandler(ICartRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(IncreaseCartServiceCommand request,
                             CancellationToken cancellationToken)
    {
        var cartService = await _repository
            .GetServiceForCart(request.cartId, request.carWorkshopServiceId);

        cartService.Quantity++;

        await _repository.UpdateServiceQuantity(cartService);
    }
}
