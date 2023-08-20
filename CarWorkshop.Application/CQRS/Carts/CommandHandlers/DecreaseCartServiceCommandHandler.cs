using CarWorkshop.Application.CQRS.Carts.Commands;
using CarWorkshop.Application.Services.Contracts;
using CarWorkshop.Domain.Contracts;
using MediatR;

namespace CarWorkshop.Application.CQRS.Carts.CommandHandlers;

public class DecreaseCartServiceCommandHandler : IRequestHandler<DecreaseCartServiceCommand>
{
    private const int MIN_QUANTITY = 1;

    private readonly IUserContextService _userContextService;
    private readonly ICartRepository _repository;

    public DecreaseCartServiceCommandHandler(IUserContextService userContextService,
                                             ICartRepository repository)
    {
        _userContextService = userContextService;
        _repository = repository;
    }

    public async Task Handle(DecreaseCartServiceCommand request, CancellationToken cancellationToken)
    {
        var userId = _userContextService.UserId;
        var cartService = await _repository.GetServiceById(userId, request.CarWorkshopServiceId);

        if (cartService.Quantity == MIN_QUANTITY) 
            throw new Exception("To perform decrease operation, quantity must be greater than 1");

        cartService.Quantity--;

        await _repository.UpdateServiceQuantity(cartService);
    }
}
