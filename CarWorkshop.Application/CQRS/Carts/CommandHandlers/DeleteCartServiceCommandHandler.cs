using CarWorkshop.Application.CQRS.Carts.Commands;
using CarWorkshop.Application.Services.Contracts;
using CarWorkshop.Domain.Contracts;
using MediatR;

namespace CarWorkshop.Application.CQRS.Carts.CommandHandlers;

public class DeleteCartServiceCommandHandler : IRequestHandler<DeleteCartServiceCommand>
{
    private readonly IUserContextService _userContextService;
    private readonly ICartRepository _repository;

    public DeleteCartServiceCommandHandler(IUserContextService userContextService,
                                           ICartRepository repository)
    {
        _userContextService = userContextService;
        _repository = repository;
    }

    public async Task Handle(DeleteCartServiceCommand request,
                             CancellationToken cancellationToken)
    {
        var userId = _userContextService.UserId;
        var cartService = await _repository.GetServiceById(userId, request.CarWorkshopServiceId);

        await _repository.DeleteServiceFromCart(cartService);
    }
}
