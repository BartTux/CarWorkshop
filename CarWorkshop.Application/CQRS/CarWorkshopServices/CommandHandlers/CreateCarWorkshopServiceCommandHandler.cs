using CarWorkshop.Application.Services;
using CarWorkshop.Domain.Contracts;
using CarWorkshop.Domain.Entities;
using MediatR;
using CarWorkshop.Application.CQRS.CarWorkshopServices.Commands;

namespace CarWorkshop.Application.CQRS.CarWorkshopServices.CommandHandlers;

public class CreateCarWorkshopServiceCommandHandler : IRequestHandler<CreateCarWorkshopServiceCommand>
{
    private readonly IUserContextService _userContextService;
    private readonly ICarWorkshopRepository _carWorkshopRepository;
    private readonly ICarWorkshopServiceRepository _carWorkshopServiceRepository;

    public CreateCarWorkshopServiceCommandHandler(IUserContextService userContext,
                                                  ICarWorkshopRepository carWorkshopRepository,
                                                  ICarWorkshopServiceRepository carWorkshopServiceRepository)
    {
        _userContextService = userContext;
        _carWorkshopRepository = carWorkshopRepository;
        _carWorkshopServiceRepository = carWorkshopServiceRepository;
    }

    public async Task Handle(CreateCarWorkshopServiceCommand request,
                             CancellationToken cancellationToken)
    {
        var carWorkshop = await _carWorkshopRepository.GetByEncodedName(request.CarWorkshopEncodedName);
        var user = _userContextService.GetCurrentUser();

        if (user is null
            || carWorkshop.CreatedById != user.Id && !user.IsInRole("Moderator")) return;

        var carWorkshopService = new CarWorkshopService
        {
            Description = request.Description,
            Cost = request.Cost,
            CarWorkshopId = carWorkshop.Id
        };

        await _carWorkshopServiceRepository.Create(carWorkshopService);
    }
}
