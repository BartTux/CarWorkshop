using CarWorkshop.Application.CQRS.Carts.Commands;
using CarWorkshop.Application.Services.Contracts;
using CarWorkshop.Domain.Contracts;
using MediatR;

namespace CarWorkshop.Application.CQRS.Carts.CommandHandlers;

public class IncreaseCartServiceCommandHandler : IRequestHandler<IncreaseCartServiceCommand>
{
    private readonly IUserContextService _userContextService;
    private readonly ICartRepository _repository;

    public IncreaseCartServiceCommandHandler(IUserContextService userContextService,
                                             ICartRepository repository)
    {
        _userContextService = userContextService;
        _repository = repository;
    }

    public async Task Handle(IncreaseCartServiceCommand request, CancellationToken cancellationToken)
    {
        var userId = _userContextService.UserId;
        var cartService = await _repository.GetServiceById(userId, request.CarWorkshopServiceId);

        cartService.Quantity++;

        await _repository.UpdateServiceQuantity(cartService);
    }
}
