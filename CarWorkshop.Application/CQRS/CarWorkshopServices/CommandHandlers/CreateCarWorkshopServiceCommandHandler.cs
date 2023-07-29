using CarWorkshop.Domain.Contracts;
using CarWorkshop.Domain.Entities;
using CarWorkshop.Application.Models;
using Microsoft.AspNetCore.Authorization;
using CarWorkshop.Application.Authorization.Requirements;
using CarWorkshop.Application.Services.Contracts;
using CarWorkshop.Application.CQRS.CarWorkshopServices.Commands;
using MediatR;

namespace CarWorkshop.Application.CQRS.CarWorkshopServices.CommandHandlers;

public class CreateCarWorkshopServiceCommandHandler : IRequestHandler<CreateCarWorkshopServiceCommand>
{
    private readonly IUserContextService _userContextService;
    private readonly IAuthorizationService _authorizationService;
    private readonly ICarWorkshopRepository _carWorkshopRepository;
    private readonly ICarWorkshopServiceRepository _carWorkshopServiceRepository;

    public CreateCarWorkshopServiceCommandHandler(IUserContextService userContextService,
                                                  IAuthorizationService authorizationService,
                                                  ICarWorkshopRepository carWorkshopRepository,
                                                  ICarWorkshopServiceRepository carWorkshopServiceRepository)
    {
        _userContextService = userContextService;
        _authorizationService = authorizationService;
        _carWorkshopRepository = carWorkshopRepository;
        _carWorkshopServiceRepository = carWorkshopServiceRepository;
    }

    public async Task Handle(CreateCarWorkshopServiceCommand request,
                             CancellationToken cancellationToken)
    {
        var carWorkshop = await _carWorkshopRepository
            .GetByEncodedName(request.CarWorkshopEncodedName);

        var authenticationResult = await _authorizationService.AuthorizeAsync(
            _userContextService.User, 
            carWorkshop,
            new ResourceOperationRequirement(ResourceOperation.Create));

        if (!authenticationResult.Succeeded) throw new Exception();

        var carWorkshopService = new CarWorkshopService
        {
            Description = request.Description,
            Cost = request.Cost,
            CarWorkshopId = carWorkshop.Id
        };

        await _carWorkshopServiceRepository.Create(carWorkshopService);
    }
}
